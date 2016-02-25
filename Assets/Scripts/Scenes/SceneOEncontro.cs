using UnityEngine;
using System.Collections;

public class SceneOEncontro : Scene {

	private Rigidbody2D heroStrongRigidbody;
	private Rigidbody2D heroFastRigidbody;
	private bool RunsOnce = false;
	private uint action = 0;
	private int timeAct = 0;
	private bool cameraNaturalMovement = true;
	private Vector3 vel = Vector3.zero;

	public void Awake()
	{
		heroStrong = FindObjectOfType<HeroStrong> ();
		heroFast = FindObjectOfType<HeroFast> ();
		dialog = GetComponentInChildren<Dialog> ();
		heroStrongRigidbody = heroStrong.GetComponent<Rigidbody2D>();
		heroFastRigidbody = heroFast.GetComponent<Rigidbody2D>();
	}

	public override void SceneAwake() {
		Camera myCamera = Camera.main;
		DisableControl ();
		LockHeroesMovement ();
	}

	public override void SceneLoop()
	{
		if (cameraNaturalMovement) {
			Camera myCamera = Camera.main;
			float heroesMidPoint = (heroFastRigidbody.position.x + heroStrongRigidbody.position.x) / 2;
			Vector3 positionTarget = new Vector3(heroesMidPoint,
			                                     myCamera.transform.position.y,
			                                     myCamera.transform.position.z);
			myCamera.transform.position = Vector3.SmoothDamp (myCamera.transform.position,
			                                                  positionTarget, 
			                                                  ref vel, 0.7f);
		}

		if (!RunsOnce) {
			RunsOnce = true;
			switch (action) {
			case 0:
				RunsOnce = false;
				if (heroFastRigidbody.position.x < (heroStrongRigidbody.position.x + 3.5f)) {
					heroStrong.Move (-0.42f);
					heroFast.Move (-0.08f);
					break;
				}
				heroFast.StopWalk ();
				AdvanceInScene();
				break;
			case 1:
			    Balloon _ballonInstance = Instantiate (dialog.balloonPrefab);
			    _ballonInstance.Init (dialog.senteces[0], AdvanceInScene);
				AdvanceInScene();
				break;
			case 2:
				RunsOnce = false;
				if (TimeElapsed_ms (timeAct) > 360) {
					heroStrong.StopWalk ();
				}
				break;
			case 3:
				RunsOnce = false;
				if (TimeElapsed_ms(timeAct) > 240)
				{
					heroStrong.Move (0.10f);
					heroStrong.StopWalk ();
					StartCoroutine (WaitSecondsToAction (1));
					RunsOnce = true;
				}

				break;
			case 4:
				Balloon ballonInstance = Instantiate (dialog.balloonPrefab);
				ballonInstance.Init (dialog.senteces[1], AdvanceInScene);
				break;
			case 5:
				ballonInstance = Instantiate (dialog.balloonPrefab);
				ballonInstance.Init (dialog.senteces[2], AdvanceInScene);
				break;
			case 6:
				RunsOnce = false;
				if (heroStrongRigidbody.position.x < (heroFastRigidbody.position.x - 2)) {
					heroFast.Move (-0.20f);
					break;
				}
				heroStrong.StopWalk ();
				heroFast.StopWalk ();
				heroFast.Crouch ();
				AdvanceInScene();
				break;
			case 7:
				RunsOnce = false;
				if (TimeElapsed_ms(timeAct) > 750)
				{
					AdvanceInScene();
				}
				break;
			case 8:
				ballonInstance = Instantiate (dialog.balloonPrefab);
				ballonInstance.Init (dialog.senteces[3], AdvanceInScene);
				break;
			case 9:
				heroStrong.Move (-0.05f);
				heroStrong.StopWalk ();
				ballonInstance = Instantiate (dialog.balloonPrefab);
				ballonInstance.Init (dialog.senteces[4], AdvanceInScene);
				break;
			case 10:
				heroFast.Move (0.05f);
				heroFast.StopWalk ();
				ballonInstance = Instantiate (dialog.balloonPrefab);
				ballonInstance.Init (dialog.senteces[5], AdvanceInScene);
				break;
			case 11:
				heroStrong.Move(0.05f);
				heroStrong.StopWalk();
				ballonInstance = Instantiate (dialog.balloonPrefab);
				ballonInstance.Init (dialog.senteces[6], FixedUpdate);
				StartCoroutine (WaitSecondsToAction (1));
				break;
			case 12:
				heroFast.Move (-0.05f);
				heroFast.StopWalk();
				StartCoroutine (WaitSecondsToAction (1));
				break;
			case 13:
				heroFast.StandUp();
				StartCoroutine(WaitSecondsToAction(1));
				break;
			case 14:
				RunsOnce = false;
				if ((heroFastRigidbody.position.x > heroStrongRigidbody.position.x + 1) ||
				    (heroFastRigidbody.position.x < heroStrongRigidbody.position.x - 1))
				{
					heroFast.Move(-0.08f);
					heroStrong.Move (0.08f);
				}
				else
				{
					heroFast.StopWalk();
					heroStrong.StopWalk();
					// TODO: Hug animation
					AdvanceInScene();
				}
				break;
			case 15:
				RunsOnce = false;
				Camera myCamera = Camera.main;

				if(TimeElapsed_ms(timeAct) < 6500)
				{
					heroFast.Move(-0.2f);
					heroStrong.Move (-0.3f);
				}
				else
				{
					heroFast.StopWalk();
					heroStrong.StopWalk();
					AdvanceInScene	();
				}
				cameraNaturalMovement = false;
				Vector3 positionTarget = new Vector3(myCamera.transform.position.x,
				                                     myCamera.transform.position.y + 2f,
				                                     myCamera.transform.position.z);
				myCamera.transform.position = Vector3.SmoothDamp (myCamera.transform.position,
							                                      positionTarget, 
					  		                                      ref vel, 0.8f);
				break;
			case 16:
				StartCoroutine(WaitSecondsToAction(2));
				break;
			default:
				EndScene();
				break;
			}
		}
	}

	IEnumerator WaitSecondsToAction(uint seconds)
	{
		yield return new WaitForSeconds (seconds);
		AdvanceInScene ();
	}

	private void AdvanceInScene()
	{
		RunsOnce = false;
		action++;
		timeAct = (int)(Time.time * 1000);
	}
}
