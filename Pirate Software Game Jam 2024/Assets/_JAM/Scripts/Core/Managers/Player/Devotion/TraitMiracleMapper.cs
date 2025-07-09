using System;

namespace Base.Core.Managers
{
    /// <summary>
    /// Provides mapping logic for trait-miracle and trait-faith relationships.
    /// </summary>
    public static class TraitMiracleMapper
    {
        public static bool IsTraitMatchingMiracle(TraitType citizenTrait, MiracleType miracleType)
        {
            switch (citizenTrait)
            {
                case TraitType.Academic:
                case TraitType.Apologist:
                case TraitType.Spiritual:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior;
                case TraitType.Collector:
                case TraitType.Witch:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior 
                        or MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior;
                case TraitType.Poet:
                case TraitType.Scheduled:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior or MiracleType.BlueBasic;
                case TraitType.Performer:
                case TraitType.Naturalist:
                case TraitType.Soldier:
                    return miracleType is MiracleType.BlueBasic or MiracleType.BlueIntermediate or MiracleType.BlueSuperior;
                case TraitType.Aesthetic:
                case TraitType.Masochist:
                    return miracleType is MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior
                        or MiracleType.BlueBasic or MiracleType.BlueIntermediate or MiracleType.BlueSuperior;
                case TraitType.Fanatic:
                case TraitType.Noble:
                case TraitType.Farmer:
                    return miracleType is MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static FaithType GetFaithTypeForTrait(TraitType trait)
        {
            return trait switch
            {
                TraitType.Academic => FaithType.GodOfLore,
                TraitType.Apologist => FaithType.TricksterGoddess,
                TraitType.Spiritual => FaithType.MotherOfTheGods,
                TraitType.Collector => FaithType.SeekerGod,
                TraitType.Witch => FaithType.ChaosGoddess,
                TraitType.Poet => FaithType.GoddessOfPoetry,
                TraitType.Scheduled => FaithType.FatherOfTheGods,
                TraitType.Performer => FaithType.LoveGod,
                TraitType.Naturalist => FaithType.NatureGoddess,
                TraitType.Soldier => FaithType.WarGod,
                TraitType.Aesthetic => FaithType.BeautyGoddess,
                TraitType.Masochist => FaithType.FuryGoddess,
                TraitType.Fanatic => FaithType.EvilGod,
                TraitType.Noble => FaithType.WealthGod,
                TraitType.Farmer => FaithType.HouseholdGod,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
} 