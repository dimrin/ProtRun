using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotatingManager : MonoBehaviour
{
    [SerializeField] private float skyboxRotatingSpeed;

    // Update is called once per frame
    void Update()
    {
        RotateSkybox();
    }

    private void RotateSkybox()
    {
        RenderSettings.skybox.SetFloat("_Rotation", skyboxRotatingSpeed * Time.time);
    }
}
