using System;
using UnityEngine;

/// <summary>
/// Utility class for matching citizen traits to miracle types.
/// </summary>
public static class TraitMiracleMatcher
{
    #region Constants
    private const string c_unknownTraitErrorMessage = "Unknown trait type encountered.";
    private const string c_unknownMiracleErrorMessage = "Unknown miracle type encountered.";
    #endregion

    #region Public API
    /// <summary>
    /// Determines if a given trait matches a miracle type.
    /// </summary>
    /// <param name="citizenTrait">The trait of the citizen.</param>
    /// <param name="miracleType">The type of miracle.</param>
    /// <returns>True if the trait matches the miracle type, otherwise false.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when trait or miracle type is unknown.</exception>
    public static bool IsTraitMatchingMiracle(TraitType citizenTrait, MiracleType miracleType)
    {
        try
        {
            if (!System.Enum.IsDefined(typeof(TraitType), citizenTrait))
            {
                throw new ArgumentOutOfRangeException(nameof(citizenTrait), citizenTrait, c_unknownTraitErrorMessage);
            }

            if (!System.Enum.IsDefined(typeof(MiracleType), miracleType))
            {
                throw new ArgumentOutOfRangeException(nameof(miracleType), miracleType, c_unknownMiracleErrorMessage);
            }

            return citizenTrait switch
            {
                TraitType.Academic or TraitType.Apologist or TraitType.Spiritual
                    => miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior,
                
                TraitType.Collector or TraitType.Witch
                    => miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior 
                        or MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior,
                
                TraitType.Poet or TraitType.Scheduled
                    => miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior or MiracleType.BlueBasic,
                
                TraitType.Performer or TraitType.Naturalist or TraitType.Soldier
                    => miracleType is MiracleType.BlueBasic or MiracleType.BlueIntermediate or MiracleType.BlueSuperior,
                
                TraitType.Aesthetic or TraitType.Masochist
                    => miracleType is MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior
                        or MiracleType.BlueBasic or MiracleType.BlueIntermediate or MiracleType.BlueSuperior,
                
                TraitType.Fanatic or TraitType.Noble or TraitType.Farmer
                    => miracleType is MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior,
                
                _ => throw new ArgumentOutOfRangeException(nameof(citizenTrait), citizenTrait, c_unknownTraitErrorMessage)
            };
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error matching trait {citizenTrait} to miracle {miracleType}: {ex.Message}");
            return false;
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Validates that a trait type is defined in the enum.
    /// </summary>
    /// <param name="traitType">The trait type to validate.</param>
    /// <param name="paramName">The name of the parameter for error messages.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when trait type is unknown.</exception>
    private static void ValidateTraitType(TraitType traitType, string paramName)
    {
        if (!System.Enum.IsDefined(typeof(TraitType), traitType))
        {
            throw new ArgumentOutOfRangeException(paramName, traitType, c_unknownTraitErrorMessage);
        }
    }

    /// <summary>
    /// Validates that a miracle type is defined in the enum.
    /// </summary>
    /// <param name="miracleType">The miracle type to validate.</param>
    /// <param name="paramName">The name of the parameter for error messages.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when miracle type is unknown.</exception>
    private static void ValidateMiracleType(MiracleType miracleType, string paramName)
    {
        if (!System.Enum.IsDefined(typeof(MiracleType), miracleType))
        {
            throw new ArgumentOutOfRangeException(paramName, miracleType, c_unknownMiracleErrorMessage);
        }
    }
    #endregion
} 