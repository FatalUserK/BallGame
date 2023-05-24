using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool blockSpawnMode;

    [SerializeField] System.Random rand = new System.Random();

    [SerializeField] GameObject block;


    GlobalEventsManager GEM;

    void Start()
    {
        GEM = GlobalEventsManager.GEM;

        if (GEM.tutorialMode)
        {
            
            for (int i = -1; i > -8; i--) // Y
            {
                rand.Next();
                for (int j = 0; j < 8; j++) // X
                {
                    int rnd = UnityEngine.Random.Range(-15, 5);
                    rnd = rand.Next(-15, 5);

                    //Debug.Log("GENERATE TUTORIAL BLOCK RND NUMBER IS " + rnd);

                    if (rnd > 0 && rnd <= 3)
                    {
                        GameObject newBlock = Instantiate(block, new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i - .5f), Quaternion.identity, transform);
                        newBlock.GetComponent<BlockGenerator>().GenerateBlock(1, 5, 1);
                    }
                    else if (rnd == 4)
                    {
                        GameObject newBlock = Instantiate(block, new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i - .5f), Quaternion.identity, transform);
                        newBlock.GetComponent<BlockGenerator>().GenerateBlock(1, 5, 2);
                    }
                }
            }
        }
    }


    public void GenerateNewBlocks(int _score)
    {

        int ballUp = UnityEngine.Random.Range(0, 8);

        for (int i = 0; i <= 8; i++)
        {
            int r = rand.Next(1,3);
            Debug.Log("R = " + r);
            if (i != ballUp && r == 1)
            {
                Debug.Log("spawning block");
                GameObject newBlock = Instantiate(block, new Vector2(transform.localPosition.x + i + .5f, transform.localPosition.y - .5f), Quaternion.identity, transform);
                newBlock.GetComponent<BlockGenerator>().GenerateBlock( Mathf.Clamp(Mathf.RoundToInt(_score / (float)1.2), 1, 100000) , Mathf.RoundToInt(_score * (float)1.2), 1); 
            }                                                           //minRange = Clamp( Rounded( score/1.2 ), 1, -1 ), maxRange = Rounded( score*1.2 )
        }
    }



}
