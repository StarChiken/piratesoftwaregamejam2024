using System;
using System.Collections.Generic;
using Base.Core.Config;

/// <summary>
/// Maps CommandmentType to MiracleType for devotion actions.
/// </summary>
public static class CommandmentMiracleMapper
{
    private static readonly Dictionary<CommandmentType, MiracleType> _mapping = new()
    {
        // Red Commandments
        { CommandmentType.Prayer, MiracleType.RedBasic },
        { CommandmentType.ReadingScripture, MiracleType.RedBasic },
        { CommandmentType.CopyingText, MiracleType.RedIntermediate },
        { CommandmentType.Research, MiracleType.RedIntermediate },
        { CommandmentType.Confessions, MiracleType.RedSuperior },
        { CommandmentType.Exorcism, MiracleType.RedSuperior },
        { CommandmentType.Alchemy, MiracleType.RedSuperior },
        // Blue Commandments
        { CommandmentType.Feast, MiracleType.BlueBasic },
        { CommandmentType.Creation, MiracleType.BlueBasic },
        { CommandmentType.Dance, MiracleType.BlueIntermediate },
        { CommandmentType.Song, MiracleType.BlueIntermediate },
        { CommandmentType.RitualisticAction, MiracleType.BlueSuperior },
        { CommandmentType.RitualisticPunishment, MiracleType.BlueSuperior },
        // Green Commandments
        { CommandmentType.MaterialOfferings, MiracleType.GreenBasic },
        { CommandmentType.ReligiousCultivation, MiracleType.GreenBasic },
        { CommandmentType.Relics, MiracleType.GreenIntermediate },
        { CommandmentType.Shrines, MiracleType.GreenIntermediate },
        { CommandmentType.GatheringBlood, MiracleType.GreenSuperior },
        { CommandmentType.Donations, MiracleType.GreenSuperior },
        { CommandmentType.RitualSacrifice, MiracleType.GreenSuperior }
    };

    public static MiracleType GetMiracleType(CommandmentType commandment)
    {
        return _mapping.TryGetValue(commandment, out var miracleType) 
            ? miracleType 
            : throw new ArgumentException($"No miracle type mapped for commandment: {commandment}");
    }
}

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
        var miracleType = CommandmentMiracleMapper.GetMiracleType(commandment);
        _devotionActionsList[miracleType]++;
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
        if (targetCitizen == null) return;

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