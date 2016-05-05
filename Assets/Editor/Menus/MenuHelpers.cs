using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

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
		string defaultScenePath = "Assets/Scenes/DefaultScene.unity";
		string clearMessage = "Deseja carregar novos objetos na scene atual? Salve antes de prosseguir.";
		string clearTitle = "Inicializar Scene";

		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));

		// Caso já existam objetos na scene verificar se o usuário realmente quer carregar novos objectos
		if (objects.Length > 0) {
			if (!EditorUtility.DisplayDialog (clearTitle, clearMessage, "Ok", "Cancelar")) {
				return;
			}
		}

		EditorSceneManager.OpenScene (defaultScenePath, OpenSceneMode.Additive);
		Scene defaultScene = EditorSceneManager.GetSceneByName ("DefaultScene");
		EditorSceneManager.MergeScenes (defaultScene, EditorSceneManager.GetActiveScene());
	}

	private static void ClearScene() {
		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));

		foreach (Object o in objects) {
			GameObject.DestroyImmediate (o);
		}
	}
}