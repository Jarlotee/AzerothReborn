using AzerothReborn.RealmServer.Reference;

namespace AzerothReborn.RealmServer.Responses;

internal class SMSG_AUTH_RESPONSE : IResponse
{
    private readonly LoginResponse _response;

    public SMSG_AUTH_RESPONSE(LoginResponse response)
    {
        _response = response;
    }

    public Network.Opcodes GetOpcode()
    {
        return Network.Opcodes.SMSG_AUTH_RESPONSE;
    }

    public void Write(Network.PacketWriter writer)
    {
        writer.AddInt8((byte)_response);
        if (_response == LoginResponse.LOGIN_OK)
        {
            writer.AddInt32(0);
            writer.AddInt8(2); // BillingPlanFlags
            writer.AddUInt32(0); // BillingTimeRested    
        }
    }
}
