using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	
	private static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

	private LevelHolder levelHolder;

	public GameManager Instance {
		get {
			
			return instance;
		}
	}


	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			//if not, set instance to this
			instance = this;
		//If instance already exists and it's not this:
		else if (instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}


	/// <summary>
	/// Returns to menu.
	/// </summary>
	public void ReturnToMenu(){
		SceneManager.LoadScene (levelHolder.MenuScene, LoadSceneMode.Single);
	}


	public void Load(Level lvl){
		
	}

	public void LoadNext(){

	}

	public enum Level{
		Level_0,
		Level_1,
		Level_2,
		Level_3,
		Level_4
	}
}
