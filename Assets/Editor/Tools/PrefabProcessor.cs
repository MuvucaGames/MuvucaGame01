using UnityEngine;
using System.Collections;
using UnityEditor;
/// <summary>
/// Prefab processor.
/// Verify that the prefab is created within our norms
/// </summary>
public class PrefabProcessor : UnityEditor.AssetModificationProcessor {

	static string[] OnWillSaveAssets (string[] paths) {
		foreach (string path in paths) {
			if (path.EndsWith (".prefab")) {
				ValidatePrefab (path);
			}
		}
		return paths;
	}

	static void OnWillCreateAsset (string path) {
		if (path.EndsWith (".prefab")) {
			ValidatePrefab (path);
		}
	}

	public static void ValidatePrefab(string path){
		GameObject go = AssetDatabase.LoadAssetAtPath<GameObject> (path);
		if (go == null)
			return;
		Transform transform = go.GetComponent<Transform> ();
		if (transform) {
			if (transform.position != Vector3.zero || transform.rotation.eulerAngles != Vector3.zero || transform.localScale != Vector3.one) {

				if(EditorUtility.DisplayDialog("Prefab fora do Padrao", "O Transform do prefab " + path + "esta fora do padrao. Voce deseja ajusta-lo automaticamente?", "Sim", "Nao, eu sei o que to fazendo")){
					transform.position = Vector3.zero;
					transform.rotation = Quaternion.identity;
					transform.localScale = Vector3.one;
				}
			}

		}
	}

	
}
