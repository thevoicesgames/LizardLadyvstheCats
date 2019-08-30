using UnityEngine;
using System;

using UnityEngine.UI;



public class startmenu : MonoBehaviour {

    public UnityEngine.EventSystems.EventSystem ev;
    public GameObject newgamebutton;
    public GameObject levelselectbutton;
    public GameObject exitbutton;
    public GameObject continuebutton;
    public GameObject backbutton;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject level6;
    public GameObject level7;
    public GameObject level8;






    public void Update()
    {
        if (ev.currentSelectedGameObject == null)
        {
            if (continuebutton.active)
                ev.SetSelectedGameObject(continuebutton);
            if (backbutton.active)
                ev.SetSelectedGameObject(backbutton);
        }
    }


    //Here we manage the response of the previous request


    public void StartGame()
    {

        baddie.enemynumber = 0;
        playermovement.pushed = false;

            Application.LoadLevel(Application.loadedLevel + 1);
    }
    public void loadlevel1()
    {
        Application.LoadLevel("level1");
    }
    public void loadlevel2()
    {
        Application.LoadLevel("level2");
    }
    public void loadlevel3()
    {
        Application.LoadLevel("level3");
    }
    public void loadlevel4()
    {
        Application.LoadLevel("level4");
    }
    public void loadlevel5()
    {
        Application.LoadLevel("level5");
    }
    public void loadlevel6()
    {
        Application.LoadLevel("level6");
    }
    public void loadlevel7()
    {
        Application.LoadLevel("level7");
    }
    public void loadlevel8()
    {
        Application.LoadLevel("level8");
    }
    public void back()
    {
        newgamebutton.active = true;
        levelselectbutton.active = true;
        //exitbutton.active = true;
        continuebutton.active = true;
        backbutton.active = false;
        level1.active = false;
        level2.active = false;
        level3.active = false;
        level4.active = false;
        level5.active = false;
        level6.active = false;
        level7.active = false;
        level8.active = false;
        ev.SetSelectedGameObject(null);



        ev.SetSelectedGameObject(continuebutton);
    }

    public void levelselect()
    {
        newgamebutton.active = false;
        levelselectbutton.active = false;
        exitbutton.active = false;
        continuebutton.active = false;
        backbutton.active = true;

        if (PlayerPrefs.GetInt("unlockedlevel1") == 1)
            level1.active = true;
        if (PlayerPrefs.GetInt("unlockedlevel2") == 1)
            level2.active = true;
        if (PlayerPrefs.GetInt("unlockedlevel3") == 1)
            level3.active = true;
        if (PlayerPrefs.GetInt("unlockedlevel4") == 1)
            level4.active = true;
        if (PlayerPrefs.GetInt("unlockedlevel5") == 1)
            level5.active = true;
        if (PlayerPrefs.GetInt("unlockedlevel6") == 1)
            level6.active = true;
        if (PlayerPrefs.GetInt("unlockedlevel7") == 1)
            level7.active = true;
        if (PlayerPrefs.GetInt("unlockedlevel8") == 1)
            level8.active = true;







        ev.SetSelectedGameObject(null);

        ev.SetSelectedGameObject(backbutton);
    }

    public void Continue()
    {
       
        int level = PlayerPrefs.GetInt("level");
        if (level > 0)
        {
           
          
            Application.LoadLevel(level);
        }
        else StartGame();
    }
    public void quitgame()
    {

        Application.Quit();
    }
    public void credits()
    {

        Application.LoadLevel("credits");
    }
    void Start()
    {
        

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        ev.SetSelectedGameObject(null);

        ev.SetSelectedGameObject(continuebutton);

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;
    }
}
