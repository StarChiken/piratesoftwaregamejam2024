using System;
using System.Collections.Generic;
using UnityEngine;
using Base.Core.Managers;

namespace Base.Gameplay
{
    /// <summary>
    /// Manages the city grid, building placement, and lookup operations.
    /// </summary>
    public interface IGridManager
    {
        Building GetBuildingAt(Vector2 position);
        Building GetRandomBuildingByType(BuildingType type);
    }

    public class GridManager : IGridManager
    {
        // Internal dictionary mapping grid positions to buildings
        private readonly Dictionary<Vector2, Building> _buildingGrid = new();

        /// <summary>
        /// Adds a building to the grid at its specified positions.
        /// </summary>
        /// <param name="building">The building to add.</param>
        public void AddBuilding(Building building)
        {
            foreach (var position in building.gridPositions)
            {
                _buildingGrid[position] = building;
            }
        }

        /// <summary>
        /// Gets the building at the specified grid position.
        /// </summary>
        /// <param name="position">The grid position to check.</param>
        /// <returns>The building at the position, or null if none exists.</returns>
        public Building GetBuildingAt(Vector2 position)
        {
            _buildingGrid.TryGetValue(position, out Building building);
            return building;
        }

        /// <summary>
        /// Gets all buildings of a specific type.
        /// </summary>
        /// <param name="type">The type of buildings to retrieve.</param>
        /// <returns>A list of buildings of the specified type.</returns>
        public List<Building> GetBuildingsByType(BuildingType type)
        {
            var result = new List<Building>();
            var seen = new HashSet<Building>();

            foreach (var building in _buildingGrid.Values)
            {
                if (building.buildingType == type && !seen.Contains(building))
                {
                    result.Add(building);
                    seen.Add(building);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a random building of the specified type using the GridManager.
        /// </summary>
        /// <param name="buildingType">The type of building to retrieve.</param>
        /// <returns>A random Building of the specified type, or null if none exist.</returns>
        public Building GetRandomBuildingByType(BuildingType type)
        {
            var buildings = GetBuildingsByType(type);
            if (buildings.Count == 0) return null;
            return RandomUtil.GetRandom(buildings);
        }
    }
} 