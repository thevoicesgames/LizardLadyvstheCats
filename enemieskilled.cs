using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieskilled : MonoBehaviour
{
    public UnityEngine.UI.Text tex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tex.text = baddie.enemieskilled.ToString();
    }
}
