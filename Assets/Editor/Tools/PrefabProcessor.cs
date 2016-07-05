using UnityEngine;
using System.Collections;
using UnityEditor;
/// <summary>
/// Prefab processor.
/// Verify that the prefab is created within our norms
/// </summary>
public class PrefabProcessor : UnityEditor.AssetModificationProcessor {

	static string[] OnWillSaveAssets (string[] paths) {
		Debug.Log("OnWillSaveAssets");
		foreach(string path in paths)
			Debug.Log(path);
		return paths;
	}

	static string OnWillCreateAsset (string path) {
		Debug.Log("OnWillCreateAssets");
		Debug.Log(path);
		return path;
	}
}
