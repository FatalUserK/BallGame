using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update


    public bool mysteryHealth = false;
    public bool invulnerable = false;
    public int bossHealth;
    public int damageMultiplier = 1;


    void Awake()
    {
        if (GlobalEventsManager.GEM.level < 0)
        {
            transform.localScale = new Vector2(1.4f, 1.4f);
        }
    }



    public TextMeshProUGUI txt;
    private void FixedUpdate()
    {
        txt.text = "" + bossHealth;
    }



    public void OnHit(List<int> data)
    {
        if (!invulnerable)
        {
            if (bossHealth <= data[0])
            {
                BossDeath();
            }
            else
            {
                Debug.Log("BOSS HIT!");
                bossHealth = bossHealth - data[0] * data[1] * damageMultiplier;

                //audioData.Play(0);
                gameObject.name = gameObject.name + " " + bossHealth;
            }
        }

    }

    void BossDeath()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
