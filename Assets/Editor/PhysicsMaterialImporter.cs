using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

/// <summary>
/// Physics Material Importer.
/// Class that extends the ICustomTiledImporter from tiled2unity to import the physicsMaterial2D from Tiled.
/// </summary>
[Tiled2Unity.CustomTiledImporter]
class PhysicsMaterialImporter : Tiled2Unity.ICustomTiledImporter
{
    /// <summary>
    /// In the Tiled layer add a custom property called 'physicsMaterial2D' to be used in the handler
    /// The value of the property must be a valid PhysicsMaterial2D located in the Assets/Materials/PhysicsMaterial/
    /// </summary>
    public void HandleCustomProperties(GameObject gameObject,
        IDictionary<string, string> props)
    {
        //Se não existir o custom property já saimos
        if (!props.ContainsKey("physicsMaterial2D"))
        {
            return;
        }

        string materialName = props["physicsMaterial2D"] + ".physicsMaterial2D";
        string materialPath = "Assets/Materials/PhysicsMaterial/" + materialName;


        // Verificamos se o material existe, se não existir disparamos o erro e retornamos
        PhysicsMaterial2D material = AssetDatabase.LoadAssetAtPath(materialPath, typeof(PhysicsMaterial2D)) as PhysicsMaterial2D;
        if (material == null)
        {
            Debug.LogError(String.Format("Could not find material: {0}", materialName));
            return;
        }
        
        //O tiled2unity sempre exporta o collider como PolygonCollider, se mudar temos que refazer essa parte
        if (gameObject.GetComponentInChildren<PolygonCollider2D>() != null)
        {
            gameObject.GetComponentInChildren<PolygonCollider2D>().sharedMaterial = material;
        }

        
    }

    /// <summary>
    /// Method not used
    /// </summary>
    public void CustomizePrefab(GameObject prefab)
    {
        //NONE
    }
}