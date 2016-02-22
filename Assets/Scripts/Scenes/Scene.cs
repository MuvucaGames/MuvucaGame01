using UnityEngine;
using System.Collections;

public abstract class Scene : ActionableElement {

	protected Dialog dialog;
	protected HeroStrong heroStrong;
	protected HeroFast heroFast;

	[SerializeField]private bool sceneActive = false;
	[SerializeField]private bool heroStrongState = false;
	[SerializeField]private bool heroFastState = false;

	private int time_ms = 0;
	private CameraController cameraController;

	void Start () {
		heroStrong = FindObjectOfType<HeroStrong>();
		heroFast = FindObjectOfType<HeroFast> ();
		dialog = GetComponentInChildren<Dialog> ();
		cameraController = FindObjectOfType<CameraController> ();
	}

	public void FixedUpdate()
	{
		if (sceneActive) SceneLoop ();
	}

	public void SaveHeroesControl() {
		heroFastState = heroFast.m_isActive;
		heroStrongState = heroStrong.m_isActive;
		cameraController.m_isOnCutscene = true;
	}

	public void DisableControl()
	{
		heroStrong.GetComponent<HeroControl> ().enabled = false;
		heroFast.GetComponent<HeroControl> ().enabled = false;
	}

	public void RestoreHeroesControl() {
		heroFast.m_isActive = heroFastState;
		heroStrong.m_isActive = heroStrongState;
		heroStrong.GetComponent<HeroControl> ().enabled = true;
		heroFast.GetComponent<HeroControl> ().enabled = true;	
		cameraController.m_isOnCutscene = false;
	}

	public override void Activate() {
		if (!sceneActive) {
			sceneActive = true;
			BeginScene ();
			SceneAwake ();
		}
	}

	// This is for scenes that the heroes are not supposed to move during the scene
	public void LockHeroesMovement() {
		SaveHeroesControl ();
		heroStrong.m_isActive = false;
		heroFast.m_isActive = false;
		heroStrong.GetComponent<HeroControl> ().enabled = false;
		heroFast.GetComponent<HeroControl> ().enabled = false;
	}

	// This is for scene that has free movement from heroes
	public void UnlockHeroesMovement() {
		RestoreHeroesControl ();
	}

	private void BeginScene()
	{
		time_ms = (int)(Time.time * 1000);
		SaveHeroesControl ();
	}

	// User must call EndScene when scene ends
	public void EndScene()
	{
		RestoreHeroesControl();
		sceneActive = false;
	}

	// Track time from beginning of the scene(milliseconds)
	public int TimeElapsed_ms()
	{
		int TotalTimeElapsed = (int)((Time.time*1000) - time_ms);
		return TotalTimeElapsed; 
	}

	// Track time from some time stamp(milliseconds)
	public int TimeElapsed_ms(int timeStamp_ms)
	{
		int TotalTimeElapsed = (int)((Time.time*1000) - timeStamp_ms);
		return TotalTimeElapsed; 
	}

	public override void Deactivate() {	}

	// Function that represents the scene itself. Runs once per frame if scene is Active
	public abstract void SceneLoop ();
	// Runs once at the beginning of scene
	public abstract void SceneAwake();
}
