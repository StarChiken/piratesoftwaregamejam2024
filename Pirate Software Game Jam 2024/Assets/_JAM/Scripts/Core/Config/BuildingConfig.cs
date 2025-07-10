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
    [SerializeField] private GameObject m_building1x1;
    [SerializeField] private GameObject m_building2x2;
    [SerializeField] private GameObject m_building2x1;
    [SerializeField] private GameObject m_buildingL;
    [SerializeField] private GameObject m_citizenPrefab;
    
    [Header("Building Materials")]
    [SerializeField] private Material m_houseMaterial;
    [SerializeField] private Material m_factionDutyMaterial;
    [SerializeField] private Material m_sanityMaterial;
    [SerializeField] private Material m_healthMaterial;
    
    [Header("Building Colors")]
    [SerializeField] private Color m_houseColor = new Color(0.8301887f, 0.21537916f, 0.21537916f, 1f);
    
    [Header("Building Type Names")]
    [SerializeField] private List<string> m_availableBuildingTypeNames = new()
    {
        "House",
        "Temple",
        "Market",
        "Entertainment"
    };

    // Public properties for backward compatibility
    public GameObject Building1x1 => m_building1x1;
    public GameObject Building2x2 => m_building2x2;
    public GameObject Building2x1 => m_building2x1;
    public GameObject BuildingL => m_buildingL;
    public GameObject CitizenPrefab => m_citizenPrefab;
    public Material HouseMaterial => m_houseMaterial;
    public Material FactionDutyMaterial => m_factionDutyMaterial;
    public Material SanityMaterial => m_sanityMaterial;
    public Material HealthMaterial => m_healthMaterial;
    public Color HouseColor => m_houseColor;
    public List<string> AvailableBuildingTypeNames => m_availableBuildingTypeNames;

    protected override string ConfigFileName => "BuildingConfig";
} 