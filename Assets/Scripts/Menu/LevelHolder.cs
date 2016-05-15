using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class LevelHolder : ScriptableObject {

	public string IntroScene;

	public string MenuScene;

	public List<string> Levels;

}
