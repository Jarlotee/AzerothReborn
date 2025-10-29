using Microsoft.EntityFrameworkCore;

namespace AzerothReborn.AuthServer.Handlers;

// todo, this data should be cached and refreshed on a timer for realms, not characters
internal sealed class AuthRealmlistHandler : IHandler<Requests.AuthRealmlistRequest>
{
    private readonly Domain.ClientState _clientState;
    private readonly Data.Auth.Context _context;

    public AuthRealmlistHandler(
        Domain.ClientState clientState,
        Data.Auth.Context context)
    {
        _clientState = clientState;
        _context = context;
    }

    public Network.MessageOpcode MessageOpcode => Network.MessageOpcode.CMD_AUTH_REALMLIST;

    public async Task<Responses.IResponseMessage> ExectueAsync(Requests.AuthRealmlistRequest request)
    {

        var realms = await _context.Realms.ToListAsync();
        var realmCharacters = await _context.RealmCharacters
            .Where(rc => rc.AccountId == _clientState.AccountId)
            .ToListAsync();

        return new Responses.AuthRealmlistResponse
        {
            Unk = request.Unk,
            Realms = realms.Select(r => new Responses.AuthRealmlistResponse.Realm
            {
                Name = r.Name,
                Address = r.Address,
                Port = r.Port.ToString(),
                Population = r.Population,
                Icon = r.Icon,
                Realmflags = r.RealmFlags,
                Timezone = r.TimeZone,
                CharacterCount = realmCharacters
                    .Where(rc => rc.RealmId == r.RealmId)
                    .FirstOrDefault()?.CharacterCount ?? 0
            }).ToList()
        };
    }
}

