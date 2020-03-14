using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class pausesystem : MonoBehaviour
{

    public GameObject resumes;
    public GameObject exits;
    public GameObject retry;
    public UnityEngine.EventSystems.EventSystem ev;
   
    // Use this for initialization
    public void pausegame()
    {
        if (Time.timeScale == 1 && !playermovement.dead)
        {
            Time.timeScale = 0;
            resumes.active = true;
            exits.active = true;
            Cursor.visible = true;
           
            Cursor.lockState = CursorLockMode.None;
            ev.SetSelectedGameObject(null);
            ev.SetSelectedGameObject(resumes);

        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            
            //Cursor.lockState = CursorLockMode.Confined;
            resumes.active = false;
            exits.active = false;
        }
    }

    public void exitgame()
    {
        Time.timeScale = 1;
        
        Application.LoadLevel("titlescreen");


    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false)
            if (resumes.active != true)
            {
                pausegame();
            }
    }
    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
       // Cursor.lockState = CursorLockMode.Confined;
        resumes.active = false;
        exits.active = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(playermovement.dead)
        {
            Cursor.visible = true;

            Cursor.lockState = CursorLockMode.None;
            retry.active = true;
        }
      
        if (Input.GetButtonDown("pause")|| CrossPlatformInputManager.GetButtonDown("Pause"))
        {

            pausegame();
        }
       
        if (Time.timeScale == 0)
        {
            if (ev.currentSelectedGameObject == null)
            {
                ev.SetSelectedGameObject(resumes);
            }
        }
       /* if (UnityEngine.PS4.Utility.isInBackgroundExecution)
            if (resumes.active != true)
            {
                pausegame();
            }*/
    }





}

