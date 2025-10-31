namespace AzerothReborn.RealmServer.Reference;

public enum LoginResponse : byte
{
    LOGIN_OK = 0xC,
    AUTH_FAILED = 0x0D,
    LOGIN_VERSION_MISMATCH = 0x14,
    LOGIN_UNKNOWN_ACCOUNT = 0x15,
    LOGIN_WAIT_QUEUE = 0x1B
}
