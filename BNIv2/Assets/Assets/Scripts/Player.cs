using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{

	public Rigidbody2D Rigidbody;
	public float movementSpeed;
	public float jumpSpeed;

	public BlockManager manager;

	public Slider powerSlider;
	public GameObject gameOver;
	public float powerConsumption;
	public float cellPowerRegain;
	public float outOfBoundsDrop;
	bool outOfBounds = false;
	float power = 100;

	bool gameOverFlag = false;

	float playerPower {
		get {
			return power;
		}
		set {
			power = value;
			UpdatePower (power);
		}
	}


	// Update is called once per frame
	void Update ()
	{
		if (power > 0) {
#if UNITY_STANDALONE || UNITY_WEBGL

			if (Input.GetButtonDown ("Horizontal")) {
				Rigidbody.AddForce (new Vector3 (movementSpeed * Input.GetAxisRaw ("Horizontal"), 0, 0));
				Rigidbody.AddForce (new Vector3 (0, jumpSpeed, 0));
				playerPower -= powerConsumption;
			}

#elif UNITY_ANDROID
			if (Input.touchCount > 0 && Input.GetTouch (Input.touchCount - 1).phase == TouchPhase.Began) {
				if (Input.GetTouch (Input.touchCount - 1).position.x > (Screen.width / 2)) {
					Rigidbody.AddForce (new Vector3 (movementSpeed * 1, 0, 0));
					Rigidbody.AddForce (new Vector3 (0, jumpSpeed, 0));
					playerPower -= powerConsumption;
				} else {
					Rigidbody.AddForce (new Vector3 (movementSpeed * -1, 0, 0));
					Rigidbody.AddForce (new Vector3 (0, jumpSpeed, 0));
					playerPower -= powerConsumption;
				}
			}
#endif
		} else {
			if (!gameOverFlag) {
				gameOverFlag = true;
				manager.onEnd ();
				gameOver.SetActive (true);
			}
		}
	}

	void FixedUpdate ()
	{
		if (outOfBounds) {
			playerPower -= outOfBoundsDrop;
		}
	}

	void UpdatePower (float value)
	{
		powerSlider.value = value;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8) {
			//Hit a cell
			Destroy (other.gameObject);

			//Also do points and energy and stuff here
			playerPower += cellPowerRegain;
			playerPower = Mathf.Clamp (playerPower, 0, 150);
		}
	}

	void OnBecameInvisible ()
	{
		outOfBounds = true;
	}

	void OnBecameVisible ()
	{
		outOfBounds = false;
	}

}
