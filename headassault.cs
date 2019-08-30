using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headassault : MonoBehaviour {
    bool clear;
    int clearcount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (baddie.enemynumber <= 0)
        {
            clear = true;
        }
        if(clear)
        {
            clearcount++;
        }
        if(clearcount > 200)
        { 
            PlayerPrefs.SetInt("level", Application.loadedLevel + 1);

            PlayerPrefs.Save();



            UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevel + 1);
        }
	}
}
