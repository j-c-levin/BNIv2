using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

    public float dropSpeed;

    public void beginDrop()
    {
        StartCoroutine("drop");
    }

    IEnumerator drop()
    {
        yield return new WaitForEndOfFrame();
        transform.Translate(new Vector3(0, -dropSpeed * Time.deltaTime, 0));
        StartCoroutine("drop");
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
