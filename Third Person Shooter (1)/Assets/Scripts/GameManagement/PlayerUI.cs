using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour {

    public Text ammoText;
    public Slider healthBar;
    public Text healthText;
    public GameObject damage_react;
    public Text TaskUI;
    public Canvas taskCanvas;
    

    void Start()
    {
        taskCanvas.enabled = false;
    }


    public void TaskCanvasPress()
    {
        if(taskCanvas.enabled == false)
            taskCanvas.enabled = true;
        else
            taskCanvas.enabled = false;
    }

}
