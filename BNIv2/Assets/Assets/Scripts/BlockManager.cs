using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{

    public CameraMovement mainCamera;
    public float initialCameraSpeed;
    public float cameraSpeedModifier;
    float cameraSpeed;

    public GameObject[] blockGroups;

    public GameObject[] cellGroups;

    public int initialLoopCountLevel;
    public int loopCountModifier;
    int loopCountLevel;
    int loopCount;

    public float initialDropSpeed;
    public float dropSpeedModifier;
    float dropSpeed;

    public float initialDelayDuration;
    public float delayDurtionModifier;
    float delayDuration;

    bool begin = true;

    // Use this for initialization
    void Start()
    {
        cameraSpeed = initialCameraSpeed;
        dropSpeed = initialDropSpeed;
        delayDuration = initialDelayDuration;
        loopCountLevel = initialLoopCountLevel;
    }

    void Update()
    {
        if (begin)
        {
#if UNITY_STANDALONE || UNITY_WEBGL
            if (Input.GetButtonDown("Horizontal"))
            {
                begin = false;
                mainCamera.speed = cameraSpeed;
                StartCoroutine("blockLoop");
            }

#elif UNITY_ANDROID

         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
         {
            begin = false;
        mainCamera.speed = cameraSpeed;
        StartCoroutine("blockLoop");
         }

#endif
        }
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(1);
        mainCamera.speed = cameraSpeed;
        StartCoroutine("blockLoop");
    }

    IEnumerator blockLoop()
    {
        instantiateCells(dropSpeed);
        yield return new WaitForSeconds(delayDuration);
        instantiateBlock(dropSpeed);
        yield return new WaitForSeconds(delayDuration);
        loop();
        StartCoroutine("blockLoop");
    }

    void loop()
    {
        loopCount += 1;
        loopCount %= loopCountLevel;
        if (loopCount == 0)
        {
            loopCountLevel += loopCountModifier;
            dropSpeed += dropSpeedModifier;
            delayDuration -= delayDurtionModifier;
            cameraSpeed += cameraSpeedModifier;
            mainCamera.speed = cameraSpeed;
        }
    }

    void instantiateCells(float speed)
    {
        Instantiate(cellRandomiser(), transform.position, transform.rotation);
    }

    void instantiateBlock(float speed)
    {
        Instantiate(blockRandomiser(), transform.position, Quaternion.identity);
    }

    GameObject blockRandomiser()
    {
        return blockGroups[Random.Range(0, blockGroups.Length)];
    }

    GameObject cellRandomiser()
    {
        return cellGroups[Random.Range(0, cellGroups.Length)];
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}
