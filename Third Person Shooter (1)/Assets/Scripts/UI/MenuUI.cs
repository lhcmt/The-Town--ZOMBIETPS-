using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuUI : MonoBehaviour {


    public Canvas quitMenu;
    public Canvas helpMenu;
    public Button starText;
    public Button helpText;
    public Button exitText;
	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas>();
        helpMenu = helpMenu.GetComponent<Canvas>();
        quitMenu.enabled = false;
        helpMenu.enabled = false;
	}
	
	// Update is called once per frame

    //点击退出，
    public void ExitPress()
    {
        quitMenu.enabled = true;
        starText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
    }
    //返回主菜单
    public void NoPress()
    {
        quitMenu.enabled = false;
        starText.enabled = true;
        helpText.enabled = true;
        exitText.enabled = true;
    }

    public void YesPress()
    {
        Application.Quit();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void HelpPress()
    {
        helpMenu.enabled = true;
        starText.enabled = false;
        helpText.enabled = false;
        exitText.enabled = false;
    }
    public void HelpMenuOK()
    {
        helpMenu.enabled = false;
        starText.enabled = true;
        helpText.enabled = true;
        exitText.enabled = true;
    }

}
