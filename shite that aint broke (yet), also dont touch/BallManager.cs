using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;
    // Start is called before the first frame update

    public bool isReloading;
    int ballsOnScreen;
    public GameObject Cannon;


    public void CreateCannon(Transform position, GameObject victim)
    {
        Cannon = Instantiate(Cannon, position);
        Destroy(victim);
    }


    public void ReadyTheCannons()
    {
        if (!isReloading)
        {
            isReloading = true;

        }
        else
        {
            Debug.Log("What the Fuck");
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
