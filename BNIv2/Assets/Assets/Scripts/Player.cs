using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{

    public Rigidbody2D Rigidbody;
    public float movementSpeed;
    public float jumpSpeed;


    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBGL

        if (Input.GetButtonDown("Horizontal"))
        {
            Rigidbody.AddForce(new Vector3(movementSpeed * Input.GetAxisRaw("Horizontal"), 0, 0));
            Rigidbody.AddForce(new Vector3(0, jumpSpeed, 0));
        }

#elif UNITY_ANDROID
         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(0).position.x > (Screen.width / 2))
                {
                    Rigidbody.AddForce(new Vector3(androidMovementSpeed * 1, 0, 0));
                    Rigidbody.AddForce(new Vector3(0, jumpSpeed, 0));
                }
                else
                {
                    Rigidbody.AddForce(new Vector3(androidMovementSpeed * -1, 0, 0));
                    Rigidbody.AddForce(new Vector3(0, jumpSpeed, 0));
                }
            }
#endif
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            //Hit a cell
            Destroy(other.gameObject);
            //Also do points and energy and stuff here
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            //Player is out of bounds, do punishment stuff
        }
    }
}
