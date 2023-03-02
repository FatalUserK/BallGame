using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool tutorialMode = true;
    bool blockSpawnMode;

    [SerializeField] GameObject block;

    void Awake()
    {
        if (tutorialMode)
        {
            for (int i = 0; i > -10; i--)
            {
                for (int j = 0; j < 11; j++)
                {
                    block = Instantiate(block, new Vector2(transform.localPosition.x + j + .5f, transform.localPosition.y + i + .5f), Quaternion.identity, transform);
                    block.GetComponent<Block>().GenerateBlock(1, 2, 1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
