using AzerothReborn.RealmServer.Reference;

namespace AzerothReborn.RealmServer.Extensions;

public static class Functions
{
    public static int GetTime()
    {
        return Environment.TickCount;
    }

    public static string UppercaseFirstLetter(string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            return val;
        }

        var array = val.ToCharArray();
        array[0] = char.ToUpper(array[0]);
        return new string(array);
    }

    public static string LowercaseFirstLetter(string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            return val;
        }

        var array = val.ToCharArray();
        array[0] = char.ToLower(array[0]);
        return new string(array);
    }

    public static bool GuidIsCreature(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_UNIT;
    }

    public static bool GuidIsPet(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_PET;
    }

    public static bool GuidIsItem(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_ITEM;
    }

    public static bool GuidIsGameObject(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_GAMEOBJECT;
    }

    public static bool GuidIsDnyamicObject(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_DYNAMICOBJECT;
    }

    public static bool GuidIsTransport(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_TRANSPORT;
    }

    public static bool GuidIsMoTransport(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_MO_TRANSPORT;
    }

    public static bool GuidIsCorpse(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_CORPSE;
    }

    public static bool GuidIsPlayer(ulong guid)
    {
        return GuidHigh2(guid) == Constants.GUID_PLAYER;
    }

    public static ulong GuidHigh2(ulong guid)
    {
        return guid & Constants.GUID_MASK_HIGH;
    }

    public static uint GuidHigh(ulong guid)
    {
        return (uint)((guid & Constants.GUID_MASK_HIGH) >> 32);
    }

    public static uint GuidLow(ulong guid)
    {
        return (uint)(guid & Constants.GUID_MASK_LOW);
    }

    public static int GetShapeshiftModel(ShapeshiftForm form, Races race, int model)
    {
        switch (form)
        {
            case ShapeshiftForm.FORM_CAT:
                {
                    if (race == Races.RACE_NIGHT_ELF)
                    {
                        return 892;
                    }

                    if (race == Races.RACE_TAUREN)
                    {
                        return 8571;
                    }

                    break;
                }

            case ShapeshiftForm.FORM_BEAR:
            case ShapeshiftForm.FORM_DIREBEAR:
                {
                    if (race == Races.RACE_NIGHT_ELF)
                    {
                        return 2281;
                    }

                    if (race == Races.RACE_TAUREN)
                    {
                        return 2289;
                    }

                    break;
                }

            case ShapeshiftForm.FORM_MOONKIN:
                {
                    if (race == Races.RACE_NIGHT_ELF)
                    {
                        return 15374;
                    }

                    if (race == Races.RACE_TAUREN)
                    {
                        return 15375;
                    }

                    break;
                }

            case ShapeshiftForm.FORM_TRAVEL:
                {
                    return 632;
                }

            case ShapeshiftForm.FORM_AQUA:
                {
                    return 2428;
                }

            case ShapeshiftForm.FORM_FLIGHT:
                {
                    if (race == Races.RACE_NIGHT_ELF)
                    {
                        return 20857;
                    }

                    if (race == Races.RACE_TAUREN)
                    {
                        return 20872;
                    }

                    break;
                }

            case ShapeshiftForm.FORM_SWIFT:
                {
                    if (race == Races.RACE_NIGHT_ELF)
                    {
                        return 21243;
                    }

                    if (race == Races.RACE_TAUREN)
                    {
                        return 21244;
                    }

                    break;
                }

            case ShapeshiftForm.FORM_GHOUL:
                {
                    return race == Races.RACE_NIGHT_ELF ? 10045 : model;
                }

            case ShapeshiftForm.FORM_CREATUREBEAR:
                {
                    return 902;
                }

            case ShapeshiftForm.FORM_GHOSTWOLF:
                {
                    return 4613;
                }

            case ShapeshiftForm.FORM_SPIRITOFREDEMPTION:
                {
                    return 12824;
                }

            default:
                {
                    return model;
                }
                // Case ShapeshiftForm.FORM_CREATURECAT
                // Case ShapeshiftForm.FORM_AMBIENT
                // Case ShapeshiftForm.FORM_SHADOW
        }

        return default;
    }

    public static ManaTypes GetShapeshiftManaType(ShapeshiftForm form, ManaTypes manaType)
    {
        switch (form)
        {
            case ShapeshiftForm.FORM_CAT:
            case ShapeshiftForm.FORM_STEALTH:
                {
                    return ManaTypes.TYPE_ENERGY;
                }

            case ShapeshiftForm.FORM_AQUA:
            case ShapeshiftForm.FORM_TRAVEL:
            case ShapeshiftForm.FORM_MOONKIN:
            case var @case when @case == ShapeshiftForm.FORM_MOONKIN:
            case var case1 when case1 == ShapeshiftForm.FORM_MOONKIN:
            case ShapeshiftForm.FORM_SPIRITOFREDEMPTION:
            case ShapeshiftForm.FORM_FLIGHT:
            case ShapeshiftForm.FORM_SWIFT:
                {
                    return ManaTypes.TYPE_MANA;
                }

            case ShapeshiftForm.FORM_BEAR:
            case ShapeshiftForm.FORM_DIREBEAR:
                {
                    return ManaTypes.TYPE_RAGE;
                }

            default:
                {
                    return manaType;
                }
        }
    }
}
