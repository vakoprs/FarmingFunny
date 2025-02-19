using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JasmineShow : MonoBehaviour
{
    public GameObject dialogFrame;
    public GameObject JasmineAppear;
    
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            dialogFrame.SetActive(false);
            JasmineAppear.SetActive(false);
        }
        catch(NullReferenceException e)
        {
            Debug.LogWarning("No reference");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dialogFrame.activeSelf == true)
        {
            JasmineAppear.SetActive(true);
        }
        //else JasmineAppear.SetActive(false);
    }

  
}
