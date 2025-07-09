using System;
using System.Collections.Generic;
using UnityEngine;
using Base.Core.Managers;

namespace Base.Gameplay
{
    /// <summary>
    /// Represents a building in the city grid, with type, size, and associated GameObject.
    /// </summary>
    [Serializable]
    public class Building
    {
        public string name;
        public Vector2[] gridPositions;
        public BuildingSize buildingSize;
        public GameObject buildingObject;
        public Mesh buildingMesh;
        public BuildingType buildingType;
        public ActionOptions buildingActionOptions;
        public List<Citizen> tempListForDoAction = new();

        public Building(string _name, Vector2[] _gridPositions, BuildingSize _buildingSize,
            GameObject _buildingObject, BuildingType buildingType)
        {
            name = _name;
            buildingSize = _buildingSize;
            gridPositions = _gridPositions;
            buildingObject = _buildingObject;
            this.buildingType = buildingType;
        }

        public void DoBuildingAction()
        {
            switch (buildingActionOptions)
            {
                case ActionOptions.DoAction:
                    switch (buildingType)
                    {
                        case BuildingType.Sanity:
                            foreach (var citizen in tempListForDoAction)
                            {
                                citizen.Sanity += 1;
                            }
                            break;
                        case BuildingType.Temple:
                            //GameManager.Player.Devotion.ChangeDevotionAmount(5); 
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case ActionOptions.AskFavorFromFaction:
                    break;
                case ActionOptions.ReplaceWithTemple:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void TurnBuildingIntoTemple(Color templeColor)
        {
            buildingType = BuildingType.Temple;
            for (int i = 0; i < buildingObject.transform.childCount; i++)
            {
                SpriteRenderer[] spriteRenderers = buildingObject.GetComponentsInChildren<SpriteRenderer>();
                spriteRenderers[i].color = templeColor;
            }
        }
    }
} 