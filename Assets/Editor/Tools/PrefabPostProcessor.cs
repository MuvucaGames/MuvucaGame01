using UnityEngine;
using System.Collections;

public class PrefabPostProcessor : UnityEditor.AssetPostprocessor
{
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) 
	{
		foreach (string str in importedAssets)
		{
			if (str.EndsWith (".prefab")) {
				PrefabProcessor.ValidatePrefab (str);
			}
		}
	}
}

