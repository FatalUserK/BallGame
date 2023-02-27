using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool tutorialMode = true;
    bool blockSpawnMode;

    [SerializeField] GameObject block;

    void Start()
    {
        for (int i = 0 ; i < 10; i++)
        {
            for (int j = 0 ; j < 15; j++)
            {
                block = Instantiate(block, new Vector2(j, i * -1), Quaternion.identity);
                block.GetComponent<Block>().GenerateBlock();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
