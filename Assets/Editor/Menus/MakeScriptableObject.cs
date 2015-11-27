using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject{
	[MenuItem("Assets/Create/Sentence")]
	public static void CreateSentence(){
		Sentence sentence = ScriptableObject.CreateInstance<Sentence> ();

		AssetDatabase.CreateAsset (sentence, AssetDatabase.GenerateUniqueAssetPath ("Assets/Prefabs/Dialogs/sentence.asset"));
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = sentence;

	}

}
