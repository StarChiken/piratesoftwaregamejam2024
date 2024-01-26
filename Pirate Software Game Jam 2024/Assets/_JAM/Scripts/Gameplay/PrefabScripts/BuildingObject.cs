using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    public Renderer[] roofObjects;

    public void SetRoofMaterial(Material material)
    {
        for (int i = 0; i < roofObjects.Length; i++)
        {
            roofObjects[i].material = material;
        }
    }
}
