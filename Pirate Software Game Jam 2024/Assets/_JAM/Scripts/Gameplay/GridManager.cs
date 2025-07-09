using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Gameplay
{
    /// <summary>
    /// Manages the city grid, building placement, and lookup operations.
    /// </summary>
    public class GridManager
    {
        // Internal dictionary mapping grid positions to buildings
        private readonly Dictionary<Vector2, Building> _buildingGrid = new();

        /// <summary>
        /// Adds a building to the grid at the specified positions.
        /// </summary>
        /// <param name="building">The building to add.</param>
        public void AddBuilding(Building building)
        {
            foreach (var pos in building.gridPositions)
            {
                _buildingGrid[pos] = building;
            }
        }

        /// <summary>
        /// Removes a building from the grid at the specified positions.
        /// </summary>
        /// <param name="building">The building to remove.</param>
        public void RemoveBuilding(Building building)
        {
            foreach (var pos in building.gridPositions)
            {
                _buildingGrid.Remove(pos);
            }
        }

        /// <summary>
        /// Gets a building at a specific grid position.
        /// </summary>
        /// <param name="position">The grid position to query.</param>
        /// <returns>The building at the position, or null if none exists.</returns>
        public Building GetBuildingAt(Vector2 position)
        {
            _buildingGrid.TryGetValue(position, out var building);
            return building;
        }

        /// <summary>
        /// Gets all buildings of a specific type.
        /// </summary>
        /// <param name="type">The type of building to retrieve.</param>
        /// <returns>A list of buildings of the specified type.</returns>
        public List<Building> GetBuildingsByType(BuildingType type)
        {
            var result = new List<Building>();
            var seen = new HashSet<Building>();
            foreach (var building in _buildingGrid.Values)
            {
                if (building.buildingType == type && seen.Add(building))
                {
                    result.Add(building);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets a random building of a specific type.
        /// </summary>
        /// <param name="type">The type of building to retrieve.</param>
        /// <returns>A random building of the specified type, or null if none exist.</returns>
        public Building GetRandomBuildingByType(BuildingType type)
        {
            var buildings = GetBuildingsByType(type);
            if (buildings.Count == 0) return null;
            return buildings[UnityEngine.Random.Range(0, buildings.Count)];
        }

        /// <summary>
        /// Returns all grid positions currently in use.
        /// </summary>
        /// <returns>An enumerable of all grid positions.</returns>
        public IEnumerable<Vector2> GetAllPositions() => _buildingGrid.Keys;
    }
} 