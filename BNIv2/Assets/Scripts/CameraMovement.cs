using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

	public float adaptiveSize;
	public float adaptiveTranslation;
	public GameObject manager;
	public float speed;

	void Start ()
	{
		float width = Screen.width;
		float height = Screen.height;
		float aspectRatio = width / height;
		float round2 = 1196f / 720f;
		float ratio = round2 / aspectRatio;
		Camera.main.orthographicSize = adaptiveSize * ratio;
		float managery = 0;
		if (ratio < 0.6f) {
			managery = 6f;
		} else if (ratio < 0.8f) {
			managery = 7f;
		} else if (ratio < 1f) {
			managery = 8f;
		} else if (ratio < 1.1f) {
			managery = 8.2f;
		} else if (ratio > 1.2f) {
			managery = 9f;
		} else if (ratio > 1.5f) {
			managery = 10f;
		}
		transform.position = new Vector3 (0, managery, -10); 
		manager.transform.position = new Vector3 (0, managery * 2, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (new Vector2 (0, speed * Time.deltaTime));
	}
}
