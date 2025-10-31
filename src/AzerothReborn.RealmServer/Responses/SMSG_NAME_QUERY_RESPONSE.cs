namespace AzerothReborn.RealmServer.Responses;

internal class SMSG_NAME_QUERY_RESPONSE : IResponse
{
    private readonly ulong _guid;
    private readonly string _name;
    private readonly int _race;
    private readonly int _gender;
    private readonly int _class;

    public SMSG_NAME_QUERY_RESPONSE(ulong guid, string name, int race, int gender, int @class)
    {
        _guid = guid;
        _name = name;
        _race = race;
        _gender = gender;
        _class = @class;
    }

    public Network.Opcodes GetOpcode()
    {
        return Network.Opcodes.SMSG_NAME_QUERY_RESPONSE;
    }

    public void Write(Network.PacketWriter writer)
    {
        writer.AddUInt64(_guid);
        writer.AddString(_name);
        writer.AddInt32(_race);
        writer.AddInt32(_gender);
        writer.AddInt32(_class);
        writer.AddInt8(0);
    }
}
