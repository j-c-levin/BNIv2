using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour
{

	public float dropSpeed;

	public void beginDrop ()
	{
		StartCoroutine ("drop");
	}

	IEnumerator drop ()
	{
		yield return new WaitForEndOfFrame ();
		transform.Translate (new Vector3 (0, -dropSpeed * Time.deltaTime, 0));
		StartCoroutine ("drop");
	}

	IEnumerator lateDestroy ()
	{
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}

	void OnBecameInvisible ()
	{
		if (gameObject.layer == 10) {

		} else {
			Destroy (this.gameObject);
		}
	}

}
