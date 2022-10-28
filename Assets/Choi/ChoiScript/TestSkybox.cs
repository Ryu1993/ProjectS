using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkybox : MonoBehaviour
{
    [SerializeField]
    Material[] skyboxs;
    int dayCount = 0;
    [SerializeField]
    float degree;
    private readonly int skyRotation = Shader.PropertyToID("_Rotation");

    public float Degree { get { return degree; }
        set
        {
            degree = value;
            RenderSettings.skybox.SetFloat(skyRotation, degree);
        }
    }
    private void Start()
    {
        RenderSettings.skybox = skyboxs[dayCount];
        TimeManager.Instance.halfhalfDayProgressAction += SkyboxChange;
    }

    private void FixedUpdate()
    {
        Degree += Time.fixedDeltaTime;
    }

    private void SkyboxChange()
    {
        dayCount++;
        if (dayCount >= skyboxs.Length)
        {
            dayCount = 0;
        }
        RenderSettings.skybox = skyboxs[dayCount];
    }



}
