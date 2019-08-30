using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class framerate : MonoBehaviour {

    public UnityEngine.UI.Text tex;
    int count;
    float deltaTime = 0.0f;

    void Update()
    {
        count++;

            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
        if (count > 100)
        {
            tex.text = fps.ToString();
            count = 0;
        }
    }
}
