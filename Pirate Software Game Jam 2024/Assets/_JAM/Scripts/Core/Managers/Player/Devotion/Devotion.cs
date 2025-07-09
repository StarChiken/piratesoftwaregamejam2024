using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Manages devotion points, miracles, and commandments for a player.
    /// </summary>
    [Serializable]
    public class Devotion
    {
        private readonly DevotionConfig _config;
        /// <summary>
        /// The current devotion points.
        /// </summary>
        public int DevotionPoints { get; private set; }
        /// <summary>
        /// The list of devotion actions for each miracle type.
        /// </summary>
        public IReadOnlyDictionary<MiracleType, int> DevotionActionsList => _devotionActionsList;
        private readonly Dictionary<MiracleType, int> _devotionActionsList;
        /// <summary>
        /// The list of commandments and their status.
        /// </summary>
        public IReadOnlyDictionary<CommandmentType, bool> CommandmentsList => _commandmentsList;
        private readonly Dictionary<CommandmentType, bool> _commandmentsList;

        /// <summary>
        /// Initializes a new Devotion system using the provided configuration.
        /// </summary>
        /// <param name="config">Configuration for devotion.</param>
        public Devotion(DevotionConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            DevotionPoints = _config.StartingDevotionPoints;
            _devotionActionsList = new Dictionary<MiracleType, int>();
            foreach (var miracle in _config.MiracleTypes)
                _devotionActionsList[miracle] = 0;
            _commandmentsList = new Dictionary<CommandmentType, bool>();
            foreach (var cmd in _config.CommandmentTypes)
                _commandmentsList[cmd] = false;
        }

        /// <summary>
        /// Adds a commandment and updates devotion actions.
        /// </summary>
        public void AddCommandment(CommandmentType commandment)
        {
            // (Switch logic unchanged, but could be moved to a CommandmentMapper if desired)
            switch (commandment)
            {
                // Red Commandments
                case CommandmentType.Prayer:
                case CommandmentType.ReadingScripture:
                    _devotionActionsList[MiracleType.RedBasic]++;
                    break;
                case CommandmentType.CopyingText:
                case CommandmentType.Research:
                    _devotionActionsList[MiracleType.RedIntermediate]++;
                    break;
                case CommandmentType.Confessions:
                case CommandmentType.Exorcism:
                case CommandmentType.Alchemy:
                    _devotionActionsList[MiracleType.RedSuperior]++;
                    break;
                // Blue Commandments
                case CommandmentType.Feast:
                case CommandmentType.Creation:
                    _devotionActionsList[MiracleType.BlueBasic]++;
                    break;
                case CommandmentType.Dance:
                case CommandmentType.Song:
                    _devotionActionsList[MiracleType.BlueIntermediate]++;
                    break;
                case CommandmentType.RitualisticAction:
                case CommandmentType.RitualisticPunishment:
                    _devotionActionsList[MiracleType.BlueSuperior]++;
                    break;
                // Green Commandments
                case CommandmentType.MaterialOfferings:
                case CommandmentType.ReligiousCultivation:
                    _devotionActionsList[MiracleType.GreenBasic]++;
                    break;
                case CommandmentType.Relics:
                case CommandmentType.Shrines:
                    _devotionActionsList[MiracleType.GreenIntermediate]++;
                    break;
                case CommandmentType.GatheringBlood:
                case CommandmentType.Donations:
                case CommandmentType.RitualSacrifice:
                    _devotionActionsList[MiracleType.GreenSuperior]++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(commandment), commandment, null);
            }
            _commandmentsList[commandment] = true;
        }

        /// <summary>
        /// Changes the devotion point amount by the specified value.
        /// </summary>
        public void ChangeDevotionAmount(int amount)
        {
            DevotionPoints += amount;
        }

        /// <summary>
        /// Performs a miracle on a target citizen.
        /// </summary>
        public void DoMiracle(MiracleType type, Citizen targetCitizen)
        {
            switch (type)
            {
                case MiracleType.RedBasic:
                    targetCitizen.Health += 1;
                    break;
                case MiracleType.RedIntermediate:
                    targetCitizen.Health += 2;
                    break;
                case MiracleType.RedSuperior:
                    targetCitizen.Health += 5;
                    break;
                case MiracleType.BlueBasic:
                    targetCitizen.Sanity += 1;
                    break;
                case MiracleType.BlueIntermediate:
                    targetCitizen.Sanity += 2;
                    break;
                case MiracleType.BlueSuperior:
                    targetCitizen.Sanity += 5;
                    break;
                case MiracleType.GreenBasic:
                    targetCitizen.Happiness += 1;
                    break;
                case MiracleType.GreenIntermediate:
                    targetCitizen.Happiness += 2;
                    break;
                case MiracleType.GreenSuperior:
                    targetCitizen.Happiness += 5;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        /// Gets the faith attraction amount for a specific miracle type.
        /// </summary>
        public int MiracleFaithAttractionByType(MiracleType type)
        {
            _devotionActionsList.TryGetValue(type, out int amount);
            return amount;
        }

        /// <summary>
        /// Checks citizens for their likeness to a miracle type and updates their faith.
        /// </summary>
        private void CheckCitizensForMiracleLikeness(MiracleType miracleType, List<Citizen> targetCitizens)
        {
            foreach (Citizen citizen in targetCitizens)
            {
                var trait = citizen.FaithAttractionTrait;
                if (TraitMiracleMapper.IsTraitMatchingMiracle(trait, miracleType))
                {
                    var dic = citizen.CitizenFaith.FaithTypeDictionary;
                    var faithType = TraitMiracleMapper.GetFaithTypeForTrait(trait);
                    dic[faithType]++;
                }
            }
        }
    }
}