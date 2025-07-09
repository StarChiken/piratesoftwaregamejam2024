using System;
using System.Collections.Generic;
using UnityEngine;
using Base.Core.Managers;

namespace Base.Core.Config
{
    /// <summary>
    /// Configuration data for initializing Devotion.
    /// </summary>
    [CreateAssetMenu(fileName = "DevotionConfig", menuName = "Game/Config/Devotion Config")]
    public class DevotionConfig : BaseConfig
    {
        [Header("Starting Values")]
        [SerializeField] private int startingDevotionPoints = 2;
        
        [Header("Miracle Types")]
        [SerializeField] private List<MiracleType> miracleTypes = new()
        {
            MiracleType.RedBasic, MiracleType.RedIntermediate, MiracleType.RedSuperior,
            MiracleType.BlueBasic, MiracleType.BlueIntermediate, MiracleType.BlueSuperior,
            MiracleType.GreenBasic, MiracleType.GreenIntermediate, MiracleType.GreenSuperior
        };
        
        [Header("Commandment Types")]
        [SerializeField] private List<CommandmentType> commandmentTypes = new()
        {
            CommandmentType.ReadingScripture, CommandmentType.CopyingText, CommandmentType.Research, CommandmentType.Prayer,
            CommandmentType.Confessions, CommandmentType.Exorcism, CommandmentType.Alchemy, CommandmentType.Dance, CommandmentType.Song,
            CommandmentType.Feast, CommandmentType.Creation, CommandmentType.RitualisticAction, CommandmentType.RitualisticPunishment,
            CommandmentType.RitualSacrifice, CommandmentType.MaterialOfferings, CommandmentType.Relics, CommandmentType.Shrines,
            CommandmentType.GatheringBlood, CommandmentType.Donations, CommandmentType.ReligiousCultivation
        };

        // Public properties for backward compatibility
        public int StartingDevotionPoints => startingDevotionPoints;
        public List<MiracleType> MiracleTypes => miracleTypes;
        public List<CommandmentType> CommandmentTypes => commandmentTypes;

        protected override string ConfigFileName => "DevotionConfig";
    }
} 