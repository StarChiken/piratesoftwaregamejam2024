using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Configuration data for initializing Devotion.
    /// </summary>
    [Serializable]
    public class DevotionConfig
    {
        public int StartingDevotionPoints = 2;
        public List<MiracleType> MiracleTypes = new()
        {
            MiracleType.RedBasic, MiracleType.RedIntermediate, MiracleType.RedSuperior,
            MiracleType.BlueBasic, MiracleType.BlueIntermediate, MiracleType.BlueSuperior,
            MiracleType.GreenBasic, MiracleType.GreenIntermediate, MiracleType.GreenSuperior
        };
        public List<CommandmentType> CommandmentTypes = new()
        {
            CommandmentType.ReadingScripture, CommandmentType.CopyingText, CommandmentType.Research, CommandmentType.Prayer,
            CommandmentType.Confessions, CommandmentType.Exorcism, CommandmentType.Alchemy, CommandmentType.Dance, CommandmentType.Song,
            CommandmentType.Feast, CommandmentType.Creation, CommandmentType.RitualisticAction, CommandmentType.RitualisticPunishment,
            CommandmentType.RitualSacrifice, CommandmentType.MaterialOfferings, CommandmentType.Relics, CommandmentType.Shrines,
            CommandmentType.GatheringBlood, CommandmentType.Donations, CommandmentType.ReligiousCultivation
        };
    }
} 