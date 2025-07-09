using System.Collections;
using System.Collections.Generic;
using Base.Core.Components;
using UnityEngine;

/// <summary>
/// Represents a building object with roof materials that can be changed.
/// </summary>
public class BuildingObject : MyMonoBehaviour
{
    public Renderer[] roofObjects;

    /// <summary>
    /// Sets the material for all roof objects.
    /// </summary>
    /// <param name="material">The material to apply to the roofs.</param>
    public void SetRoofMaterial(Material material)
    {
        // Loop through each roof object and set its material
        for (int i = 0; i < roofObjects.Length; i++)
        {
            roofObjects[i].material = material;
        }
    }
}
