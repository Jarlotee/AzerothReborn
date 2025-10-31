using System.Numerics;
using System.Security.Cryptography;
using AzerothReborn.RealmServer.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzerothReborn.RealmServer.Handlers;

[Opcode(Network.Opcodes.CMSG_AUTH_SESSION)]
internal class CMSG_AUTH_SESSION : IHandler
{
    private const int REQUIRED_BUILD_LOW = 5875; // 1.12.1
    private const int REQUIRED_BUILD_HIGH = 6141;
    private readonly ILogger _logger;
    private readonly Server.Realm _realm;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly SHA1 _sha = SHA1.Create();


    public CMSG_AUTH_SESSION(
        ILogger<CMSG_AUTH_SESSION> logger,
        IServiceScopeFactory scopeFactory,
        Server.Realm realm)
    {
        _logger = logger;
        _realm = realm;
        _scopeFactory = scopeFactory;
    }

    public Task<List<Responses.IResponse>> HandleAsync(Network.PacketReader reader, Domain.Client client)
    {
        var request = new Requests.CMSG_AUTH_SESSION(reader);

        if (request.ClientVersion is < REQUIRED_BUILD_LOW or > REQUIRED_BUILD_HIGH)
        {
            var response = new Responses.SMSG_AUTH_RESPONSE(Reference.LoginResponse.LOGIN_VERSION_MISMATCH);
            return Task.FromResult(new List<Responses.IResponse> { response });
        }

        using var scope = _scopeFactory.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<Data.Auth.Context>();
        var account = context.Accounts
            .Where(a => a.UserName.Equals(request.ClientAccount, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();

        if (account is null)
        {
            var response = new Responses.SMSG_AUTH_RESPONSE(Reference.LoginResponse.LOGIN_UNKNOWN_ACCOUNT);
            return Task.FromResult(new List<Responses.IResponse> { response });
        }

        if (string.IsNullOrWhiteSpace(account.SessionKey))
        {
            throw new ApplicationException("Account Session should not be null or empty");
        }

        if (!IsClientValid(request, client, account))
        {
            var response = new Responses.SMSG_AUTH_RESPONSE(Reference.LoginResponse.AUTH_FAILED);
            return Task.FromResult(new List<Responses.IResponse> { response });
        }

        client.Account = account.UserName;
        client.SetEncryptionHash(BigInteger.Parse(account.SessionKey));

        // TODO check realm security level

        // TODO check full (clientTs count and config value) => Reference.LoginResponse.LOGIN_WAIT_QUEUE

        // https://github.com/mangoszero/server/blob/master/src/game/WorldHandlers/AddonHandler.cpp (if you want to deal with this is in the future)

        PurgeHangingCharactersForAccount(account);

        return Task.FromResult(new List<Responses.IResponse> { new Responses.SMSG_AUTH_RESPONSE(Reference.LoginResponse.LOGIN_OK) });
    }

    private bool IsClientValid(Requests.CMSG_AUTH_SESSION request, Domain.Client client, Data.Auth.Models.Account account)
    {
        var clientHash = new List<byte>();

        if (string.IsNullOrWhiteSpace(account.SessionKey))
        {
            throw new ApplicationException("Account Session should not be null or empty");
        }

        var k = BigInteger.Parse(account.SessionKey);

        clientHash.AddRange(System.Text.Encoding.ASCII.GetBytes(request.ClientAccount));
        clientHash.AddRange(BitConverter.GetBytes(0));
        clientHash.AddRange(BitConverter.GetBytes(request.ClientSeed));
        clientHash.AddRange(BitConverter.GetBytes(client.M_Seed));
        clientHash.AddRange(k.ToByteArray());

        var digest = _sha.ComputeHash(clientHash.ToArray());

        if (!digest.SequenceEqual(request.ClientHash))
        {
            return false;
        }

        return true;
    }

    private void PurgeHangingCharactersForAccount(Data.Auth.Models.Account account)
    {
        foreach (var keypair in _realm.CharacteRs)
        {
            if (keypair.Value is not null
                && keypair.Value.Client is not null
                && string.Equals(keypair.Value.Client.Account, account.UserName))
            {
                Domain.Character? character;
                _realm.CharacteRsLock.AcquireReaderLock(Reference.Constants.DEFAULT_LOCK_TIMEOUT);
                _realm.CharacteRs.Remove(keypair.Key, out character);
                _realm.CharacteRsLock.ReleaseLock();
                character?.Dispose();
            }
        }
    }
}
