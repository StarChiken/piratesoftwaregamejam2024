using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Configuration data for buildings, prefabs, and materials.
/// </summary>
[CreateAssetMenu(fileName = "BuildingConfig", menuName = "Game/Config/Building Config")]
public class BuildingConfig : BaseConfig
{
    [Header("Building Prefabs")]
    [SerializeField] private GameObject building1x1;
    [SerializeField] private GameObject building2x2;
    [SerializeField] private GameObject building2x1;
    [SerializeField] private GameObject buildingL;
    [SerializeField] private GameObject citizenPrefab;
    
    [Header("Building Materials")]
    [SerializeField] private Material houseMaterial;
    [SerializeField] private Material factionDutyMaterial;
    [SerializeField] private Material sanityMaterial;
    [SerializeField] private Material healthMaterial;
    
    [Header("Building Colors")]
    [SerializeField] private Color houseColor = new Color(0.8301887f, 0.21537916f, 0.21537916f, 1f);
    
    [Header("Building Type Names")]
    [SerializeField] private List<string> availableBuildingTypeNames = new()
    {
        "House",
        "Temple",
        "Market",
        "Entertainment"
    };

    // Public properties for backward compatibility
    public GameObject Building1x1 => building1x1;
    public GameObject Building2x2 => building2x2;
    public GameObject Building2x1 => building2x1;
    public GameObject BuildingL => buildingL;
    public GameObject CitizenPrefab => citizenPrefab;
    public Material HouseMaterial => houseMaterial;
    public Material FactionDutyMaterial => factionDutyMaterial;
    public Material SanityMaterial => sanityMaterial;
    public Material HealthMaterial => healthMaterial;
    public Color HouseColor => houseColor;
    public List<string> AvailableBuildingTypeNames => availableBuildingTypeNames;

    protected override string ConfigFileName => "BuildingConfig";
} 