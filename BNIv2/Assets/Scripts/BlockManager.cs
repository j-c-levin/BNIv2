using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{

	public CameraMovement mainCamera;
	public float initialCameraSpeed;
	public float cameraSpeedModifier;
	public float cameraSpeedCeiling;
	float cameraSpeed;

	public Text scoreText;
	public int initialScorePerSecond;
	public float scoreUpdateRate;
	public float scoreModifierRate;
	float scoreModifier;
	float playerScore;

	public Text gameOverText;

	public GameObject[] blockGroups;

	public GameObject[] cellGroups;

	public int initialLoopCountLevel;
	public float loopCountModifier;
	int loopCountLevel;
	int loopCount;

	public float initialDelayDuration;
	public float delayDurtionModifier;
	public float delayDurationFloor;
	float delayDuration;

	bool begin = true;

	float endTime;

	// Use this for initialization
	void Start ()
	{
		cameraSpeed = initialCameraSpeed;
		delayDuration = initialDelayDuration;
		loopCountLevel = initialLoopCountLevel;
		scoreModifier = 1;
		playerScore = 0;
		scoreText.text = playerScore.ToString ();
	}

	void Update ()
	{
		if (begin) {
			#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
			if (Input.GetButtonDown ("Horizontal")) {
				begin = false;
				mainCamera.speed = cameraSpeed;
				StartCoroutine ("blockLoop");
				StartCoroutine ("score");
			}

#elif UNITY_ANDROID
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
				begin = false;
				mainCamera.speed = cameraSpeed;
				StartCoroutine ("blockLoop");
				StartCoroutine ("score");
			}

#endif
		}
	}

	IEnumerator start ()
	{
		yield return new WaitForSeconds (1);
		mainCamera.speed = cameraSpeed;
		StartCoroutine ("blockLoop");
	}

	IEnumerator score ()
	{
		playerScore += (scoreModifier * (initialScorePerSecond * scoreUpdateRate));
		scoreText.text = playerScore.ToString () + " x" + scoreModifier;
		yield return new WaitForSeconds (scoreUpdateRate);
		StartCoroutine ("score");
	}

	IEnumerator blockLoop ()
	{
		instantiateCells ();
		yield return new WaitForSeconds (delayDuration);
		instantiateBlock ();
		yield return new WaitForSeconds (delayDuration);
		loop ();
		StartCoroutine ("blockLoop");
	}

	void loop ()
	{
		loopCount += 1;
		loopCount %= loopCountLevel;
		if (loopCount == 0) {
			//loopCountLevel *= (int)loopCountModifier;
			if (delayDuration > delayDurationFloor) {
				delayDuration -= delayDurtionModifier;
			}
			if (cameraSpeed < cameraSpeedCeiling) {
				cameraSpeed += cameraSpeedModifier;
				mainCamera.speed = cameraSpeed;
			}
			scoreModifier += scoreModifierRate;
		}
	}

	void instantiateCells ()
	{
		Instantiate (cellRandomiser (), transform.position, transform.rotation);
	}

	void instantiateBlock ()
	{
		Instantiate (blockRandomiser (), transform.position, Quaternion.identity);
	}

	GameObject blockRandomiser ()
	{
		return blockGroups [Random.Range (0, blockGroups.Length)];
	}

	GameObject cellRandomiser ()
	{
		return cellGroups [Random.Range (0, cellGroups.Length)];
	}

	public void onEnd ()
	{
		endTime = Time.time;
		StopCoroutine ("score");
		loopCountLevel = 1000000000;
		gameOverText.text = "Energy Depleated\nFinal Score: " + playerScore + "\nPlay again?";
	}

	public void restart ()
	{
		if (Time.time - endTime > 0.5f) {
			SceneManager.LoadScene ("Game");
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = new Color (1, 0, 0, 0.5F);
		Gizmos.DrawCube (transform.position, new Vector3 (1, 1, 1));
	}
}
