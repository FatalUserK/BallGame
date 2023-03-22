using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_bs : MonoBehaviour
{

    [SerializeField] int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("IncrementCount", 10, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            StartCoroutine(CoroutineExample());
        }
    }


    IEnumerator CoroutineExample()
    {
        Invoke("IncrementCount", 1);

        yield return null;
    }

    void IncrementCount()
    {
        count++;
    }
}
