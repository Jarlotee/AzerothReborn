using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.AuthServer.Handlers;

internal sealed class RsLogonProofHandler : IHandler<Requests.RsLogonProofRequest>
{
    private readonly ILogger _logger;
    private readonly Domain.ClientState _clientState;
    private readonly Data.Auth.Context _context;

    public RsLogonProofHandler(
        ILogger<RsLogonProofHandler> logger,
        Domain.ClientState clientState,
        Data.Auth.Context context)
    {
        _logger = logger;
        _clientState = clientState;
        _context = context;
    }

    public Network.MessageOpcode MessageOpcode => Network.MessageOpcode.CMD_AUTH_LOGON_PROOF;

    public async Task<Responses.IResponseMessage> ExectueAsync(Requests.RsLogonProofRequest request)
    {
        if (_clientState.AccountName is null) throw new Exception("Account name missing from client state.");
        if (_clientState.IPAddress is null) throw new Exception("Ip address missing from client state.");

        _clientState.AuthEngine.CalculateU(request.A);
        _clientState.AuthEngine.CalculateM1();

        if (!_clientState.AuthEngine.M1.SequenceEqual(request.M1))
        {
            _logger.LogInformation($"Wrong password for user {_clientState.AccountName}");
            return new Responses.AuthLogonProofResponse { AccountState = Domain.AccountStates.LOGIN_BAD_PASS };
        }

        _clientState.AuthEngine.CalculateM2(request.M1);

        var sshash = string.Concat(_clientState.AuthEngine.SsHash.Select(x => x.ToString("X2")));



        var account = await _context.Accounts
            .Where(a => a.UserName == _clientState.AccountName)
            .FirstOrDefaultAsync();

        if (account is null) throw new Exception("Cant find account in the database");

        account.SessionKey = sshash;
        account.LastIpAddress = _clientState.IPAddress.ToString();
        account.LastLoginUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new Responses.AuthLogonProofResponse
        {
            AccountState = Domain.AccountStates.LOGIN_OK,
            M2 = _clientState.AuthEngine.M2
        };
    }
}
