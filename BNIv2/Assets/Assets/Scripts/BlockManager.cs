using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{

    public GameObject[] blockGroups;

    GameObject initializingBlock;
    BlockScript initialisingBlockScript;

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

    // Use this for initialization
    void Start()
    {
        dropSpeed = initialDropSpeed;
        delayDuration = initialDelayDuration;
        loopCountLevel = initialLoopCountLevel;
        StartCoroutine("blockLoop");
    }

    IEnumerator blockLoop()
    {
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
            Debug.Log("new loop level: " + loopCountLevel);
        }
    }

    void instantiateBlock(float speed)
    {
        initializingBlock = Instantiate(blockRandomiser(), transform.position, Quaternion.identity) as GameObject;
        initialisingBlockScript = initializingBlock.GetComponent<BlockScript>();
        initialisingBlockScript.dropSpeed = speed;
        initialisingBlockScript.beginDrop();
    }

    GameObject blockRandomiser()
    {
        return blockGroups[Random.Range(0, blockGroups.Length)];
    }
}
