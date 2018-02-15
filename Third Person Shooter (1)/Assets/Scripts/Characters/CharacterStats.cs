using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    [Range(0, 100)]
    public float health =100f;
    //队伍
    public int faction; //what time he is on

    //ID
    public int ID;
    //name etc
    public string playerName;


    private PlayerUI playerUI;

    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
    }
    void Update()
    {
        health = Mathf.Clamp(health, 0, 100);
    }

    public void ApplyDamage(float number)
    {
        playerUI.damage_react.GetComponent<CanvasGroup>().alpha = 1;
        health -= number;
        if(health <0)
        {
            health = 0;
            //gameover();
        }
        if(health >100)
        {
            health = 100;
        }
    }

}
