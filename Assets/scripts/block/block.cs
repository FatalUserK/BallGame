using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{

    string blockName = string.Empty;


    // Start is called before the first frame update
    void Start() //start
    {
        Invoke("BlockCheck", 3);
    }


    #region interactions and damage etc




    void GetHit() //call when ball hits block
    {
        
    }



    int extraLives = 0; //no idea if ill use this lmao

    void BlockBreak() //call when block breaks
    {
        Destroy(gameObject);
    }
    #endregion



    #region settings and attributes


    [SerializeField] int blockHP;
    [SerializeField] int blockMaxHP; 


    bool settingsApplied = false;

    public void ApplySettings(float shape,int _blockHP, float _blockMaxHP = -45.2397642f) //apply settings to the block
    {
        if (_blockMaxHP == -45.2397642) { _blockMaxHP = _blockHP; } //if MaxHP is not set, then set it to be equal to HP

        blockHP = _blockHP;
        blockMaxHP = (int)_blockMaxHP;

        UpdateBlock();
        settingsApplied = true;
    }

    void BlockCheck() //check if the block has settings applied already
    {
        if (!settingsApplied) //if it does not, apply some default settings
        {
            int rand = UnityEngine.Random.Range(1, 3);
            ApplySettings(1, rand);
        }
    }

    bool canBeNegative = false;

    void UpdateBlock()
    {
        gameObject.name = blockName + " " + blockHP + "/" + blockMaxHP;

        if (!canBeNegative && blockHP >= 0)
        {
            BlockBreak();
        }
    }

    #endregion


}
