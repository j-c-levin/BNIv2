using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(0, speed * Time.deltaTime));
	}
}
