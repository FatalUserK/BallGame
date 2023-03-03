using System.Collections;
using System.Collections.Generic;
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
            
            for (int i = 0; i > -10; i--) // -10
            {
                rand.Next();
                for (int j = 0; j < 11; j++) // 11
                {
                    int rnd = Random.Range(-3, 5);
                    //Debug.Log("GENERATE TUTORIAL BLOCK RND NUMBER IS " + rnd);
                    if (rnd > 0 && rnd <= 3)
                    {
                        block = Instantiate(block, new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i - .5f), Quaternion.identity, transform);
                        block.GetComponent<Block>().GenerateBlock(1, 5, 1);
                    }
                    else if (rnd == 4)
                    {
                        block = Instantiate(block, new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i - .5f), Quaternion.identity, transform);
                        block.GetComponent<Block>().GenerateBlock(1, 5, 2);
                    }
                }
            }
        }
    }



    void GenerateTutorialBlock(int i, int j)
    {

    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
