using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nebula : MonoBehaviour {
    float blue;
    float red;
    float green;
    float reds;
    float greens;
    float blues;
    int count;
	// Use this for initialization
	void Start () {
        blue = 0.0009f;
        red = -0.001f;
        green = 0.0001f;
        reds = 0.2f;
        blues = 0.19f;
        greens = 0.07f;
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(reds, blues,greens);
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.00005f;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        count++;
        if(count>10)
        {
            count = 0;
            if (reds >= 0.2f)
                red = -0.001f;

            else if(reds <=0) red = 0.001f;
            reds += red;

            if (blues >= 0.2f)
                blue = -0.009f;

            else if (blues <= 0) blue = 0.0009f;
            blues += blue;

            if (greens >= 0.2f)
                green = -0.0001f;

            else if (greens <= 0) green = 0.0001f;
            greens += green;

            RenderSettings.fogColor = new Color(reds, blues, greens);

        }
	}
}
