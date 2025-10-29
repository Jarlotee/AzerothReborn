using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;

namespace AzerothReborn.AuthServer.Handlers;

internal sealed class RsLogonChallengeHandler : IHandler<Requests.RsLogonChallengeRequest>
{
    private readonly ILogger _logger;
    private readonly Domain.ClientState _clientState;
    private readonly Data.Auth.Context _context;

    public RsLogonChallengeHandler(
        ILogger<RsLogonChallengeHandler> logger,
        Domain.ClientState clientState,
        Data.Auth.Context context)
    {
        _logger = logger;
        _clientState = clientState;
        _context = context;
    }

    public Network.MessageOpcode MessageOpcode => Network.MessageOpcode.CMD_AUTH_LOGON_CHALLENGE;

    public async Task<Responses.IResponseMessage> ExectueAsync(Requests.RsLogonChallengeRequest request)
    {
        if (request.ClientBuild != Domain.WowClientBuildVersions.WOW_1_12_1)
        {
            _logger.LogWarning($"Someone try to login with unsupported client version {request.ClientBuild}");
            return new Responses.AuthLogonProofResponse { AccountState = Domain.AccountStates.LOGIN_BADVERSION };
        }

        var account = await _context.Accounts
            .Where(a => a.UserName == request.AccountName)
            .FirstOrDefaultAsync();

        if (account == null)
        {
            return new Responses.AuthLogonProofResponse { AccountState = Domain.AccountStates.LOGIN_UNKNOWN_ACCOUNT };
        }

        // what about ip address?

        var bannedInfo = await _context.AccountsBanned
            .Where(ab => ab.AccountId == account.AccountId)
            .FirstOrDefaultAsync();

        if (bannedInfo is not null && bannedInfo.Active)
        {
            return new Responses.AuthLogonProofResponse { AccountState = Domain.AccountStates.LOGIN_BANNED };
        }
        if (account.ShaPasswordHash.Length != 40)
        {
            return new Responses.AuthLogonProofResponse { AccountState = Domain.AccountStates.LOGIN_BAD_PASS };
        }

        var hash = GetPasswordHashFromString(account.ShaPasswordHash);
        _clientState.AccountId = account.AccountId;
        _clientState.AccountName = request.AccountName;
        _clientState.AuthEngine.CalculateX(Encoding.UTF8.GetBytes(request.AccountName), hash);

        return new Responses.AuthLogonChallengeResponse
        {
            PublicB = _clientState.AuthEngine.PublicB,
            G = _clientState.AuthEngine.g,
            N = _clientState.AuthEngine.N,
            Salt = _clientState.AuthEngine.Salt,
            CrcSalt = Domain.AuthEngine.CrcSalt
        };
    }

    private byte[] GetPasswordHashFromString(string sha_pass_hash)
    {
        var hash = new byte[20];
        for (var i = 0; i < 40; i += 2)
        {
            hash[i / 2] = byte.Parse(sha_pass_hash.AsSpan(i, 2), NumberStyles.HexNumber);
        }
        return hash;
    }
}
