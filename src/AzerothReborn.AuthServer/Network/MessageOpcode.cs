namespace AzerothReborn.AuthServer.Network;

public enum MessageOpcode : byte
{
    CMD_AUTH_LOGON_CHALLENGE = 0x0,
    CMD_AUTH_LOGON_PROOF = 0x1,
    CMD_AUTH_RECONNECT_CHALLENGE = 0x2,
    CMD_AUTH_RECONNECT_PROOF = 0x3,
    CMD_AUTH_REALMLIST = 0x10,
    CMD_XFER_INITIATE = 0x30,
    CMD_XFER_DATA = 0x31,
    CMD_XFER_ACCEPT = 0x32,
    CMD_XFER_RESUME = 0x33,
    CMD_XFER_CANCEL = 0x34
}
