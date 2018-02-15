using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//单例
[RequireComponent(typeof(Timer))]

public class GameController : MonoBehaviour {
    //单例
    public static GameController gc;

    private UserInput player { get { return FindObjectOfType<UserInput>();  }set{player = value;}}
    private PlayerUI playerUI { get { return FindObjectOfType<PlayerUI>(); } set { playerUI = value; } }
    private WeaponHandler wp { get { return player.GetComponent<WeaponHandler>(); } set { wp = value; } }
    private CharacterStats characterStats { get { return player.GetComponent<CharacterStats>(); } set { characterStats = value; } }


    private Timer m_Timer;
    public Timer timer{
        get{
            if (m_Timer == null)
                m_Timer = gameObject.GetComponent<Timer>();
            return m_Timer;
        }
    }

    private Container m_Container;
    public Container container
    {
        get
        {
            if (m_Container == null)
                m_Container = player.GetComponentInChildren<Container>();
            return m_Container;
        }
    }
    /*
    private EventBus m_EventBus;
    public EventBus EventBus
    {
        get
        {
            if (m_EventBus == null)
                m_EventBus = new EventBus();
            return m_EventBus;
        }
    }*/


    void Awake()
    {
        if (gc == null)
        {
            gc = this;
        }
        else
        {
            if (gc != this)
            {
                Destroy(this);
            }
        }
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if(player)
        {
            if (playerUI)
            {
                if(wp)
                {
                    if (playerUI.ammoText)
                    {
                        //手里没有武器
                        if (wp.currentWeapon == null)
                        {
                            playerUI.ammoText.text = "Unarmed";
                        }
                        else
                        {
                            playerUI.ammoText.text = wp.currentWeapon.ammo.clipAmmo +
                                "//" +
                                GameController.gc.container.GetAmountRemaining(wp.currentWeapon.ammo.AmmoID);

                        }
                    }

                }
                if (playerUI.healthBar && playerUI.healthText)
                {
                    playerUI.healthBar.value = characterStats.health;
                    playerUI.healthText.text = Mathf.Round(playerUI.healthBar.value).ToString();
                }
                //taskMenu
                if (Input.GetButtonDown(player.inputs.TaskButtun))
                {
                    playerUI.TaskCanvasPress();
                }
            }


        }


    }


}
