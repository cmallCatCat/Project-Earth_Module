using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FindUICamera : MonoBehaviour
{
    private Camera uiCamera;

    private void Start()
    {
        uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
                    GetComponent<Camera>().GetUniversalAdditionalCameraData().cameraStack
                        .Add(uiCamera);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}