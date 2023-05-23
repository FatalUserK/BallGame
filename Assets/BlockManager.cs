using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool tutorialMode = true;
    bool blockSpawnMode;

    [SerializeField] System.Random rand = new System.Random();

    [SerializeField] GameObject block;

    void Awake()
    {
        if (tutorialMode)
        {
            
            for (int i = 0; i > -8; i--) // Y
            {
                rand.Next();
                for (int j = 0; j < 8; j++) // X
                {
                    int rnd = UnityEngine.Random.Range(-10, 5);

                    //Debug.Log("GENERATE TUTORIAL BLOCK RND NUMBER IS " + rnd);

                    if (rnd > 0 && rnd <= 3)
                    {
                        block = Instantiate(block, new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i - .5f), Quaternion.identity, transform);
                        block.GetComponent<BlockGenerator>().GenerateBlock(1, 5, 1);
                    }
                    else if (rnd == 4)
                    {
                        block = Instantiate(block, new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i - .5f), Quaternion.identity, transform);
                        block.GetComponent<BlockGenerator>().GenerateBlock(1, 5, 2);
                    }
                }
            }
        }
    }


    void GenerateNewBlocks(int _score)
    {

        int ballUp = UnityEngine.Random.Range(0, 8);

        for (int i = 0; i <= 8; i++)
        {
            if (i != ballUp)
            {
                block = Instantiate(block, new Vector2(transform.localPosition.x + i + .5f, transform.localPosition.y - .5f), Quaternion.identity, transform);
                block.GetComponent<BlockGenerator>().GenerateBlock(_score - 5, _score + 10, 1);
            }
        }
    }

    public void CallBlockDescendbcUnitySucks()
    {
        StartCoroutine("BlocksDescend", 1);
        
    }

    public IEnumerator BlocksDescend(int amount = 1)
    {
        Debug.Log("oh boy we better start moving, and by precisely " + amount + " units too!");
        Vector3 destination = new Vector3(transform.position.x, transform.position.y - amount);
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, .1f);
            Debug.Log("moving, chugga chugga chugga...");
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
