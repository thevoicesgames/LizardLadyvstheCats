using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEVELEXIT1 : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
                PlayerPrefs.SetInt("level", Application.loadedLevel + 1);

                PlayerPrefs.Save();


                
                UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevel + 1);
            
        }
    }

        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
