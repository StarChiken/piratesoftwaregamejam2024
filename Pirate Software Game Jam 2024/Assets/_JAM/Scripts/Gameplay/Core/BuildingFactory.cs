using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Factory for creating and initializing Building instances.
/// </summary>
public static class BuildingFactory
{
    /// <summary>
    /// Creates a new Building instance with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the building.</param>
    /// <param name="gridPositions">The grid positions occupied by the building.</param>
    /// <param name="size">The size/shape of the building.</param>
    /// <param name="buildingObject">The associated GameObject.</param>
    /// <param name="type">The type of the building.</param>
    /// <returns>A new Building instance.</returns>
    public static Building CreateBuilding(string name, Vector2[] gridPositions, BuildingSize size, GameObject buildingObject, BuildingType type)
    {
        // Create and return a new Building
        return new Building(name, gridPositions, size, buildingObject, type);
    }

    /// <summary>
    /// Turns a building into a temple, changing its type and color.
    /// </summary>
    /// <param name="building">The building to convert.</param>
    /// <param name="templeColor">The color to apply to the temple.</param>
    public static void ConvertToTemple(Building building, Color templeColor)
    {
        // Use the Building's method to convert to a temple
        building.TurnBuildingIntoTemple(templeColor);
    }
} 