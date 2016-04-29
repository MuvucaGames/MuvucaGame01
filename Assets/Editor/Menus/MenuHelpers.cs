using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MenuHelpers{
	[MenuItem("MuvucaGames/GDD")]
	public static void OpenGDD(){
		Help.BrowseURL ("https://github.com/MuvucaGames/MuvucaGame01/wiki/GDD");
	}

	[MenuItem("MuvucaGames/Trello")]
	public static void OpenTrello(){
		Help.BrowseURL ("https://trello.com/muvucagames");
	}

	[MenuItem("MuvucaGames/Github")]
	public static void OpenGithub(){
		Help.BrowseURL ("https://github.com/MuvucaGames/MuvucaGame01/");
	}

	[MenuItem("MuvucaGames/Inicializar Scene")]
	public static void InitializeScene(){
		string clearMessage = "Deseja realmente deletar TODOS os objetos dessa scene permanentemente?";
		string clearTitle = "Inicializar Scene";

		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));

		// Caso já existam objetos na scene verificar se o usuário realmente os quer deletar
		if (objects.Length > 0) {
			if (EditorUtility.DisplayDialog (clearTitle, clearMessage, "Ok", "Cancelar")) {
				ClearScene ();
			} else {
				return;
			}
		}

		// Lista de prefabs a serem instanciados
		List<string> prefabList = new List<string> 
		{ 
			"Managers",
			"Camera",
			"Hero/HeroFast",
			"Hero/HeroStrong"
		};

		foreach (string prefabPath in prefabList) {
			GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Prefabs/" + prefabPath + ".prefab");

			if (prefab != null) {
				PrefabUtility.InstantiatePrefab (prefab);
			} else {
				Debug.Log ("Prefab '" + prefabPath + "' not found");
			}
		}
	}

	private static void ClearScene() {
		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));

		foreach (Object o in objects) {
			GameObject.DestroyImmediate (o);
		}
	}
}
