namespace AzerothReborn.RealmServer.Reference;

public enum PetitionTurnInError
{
    PETITIONTURNIN_OK = 0,                   // :Closes the window
    PETITIONTURNIN_ALREADY_IN_GUILD = 2,     // You are already in a guild
    PETITIONTURNIN_NEED_MORE_SIGNATURES = 4 // You need more signatures
}
