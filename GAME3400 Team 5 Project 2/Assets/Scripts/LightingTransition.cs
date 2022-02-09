using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTransition : MonoBehaviour
{
    public static LightingTransition instance;
    public float brightness
    {
        get
        {
            return this.brightnessVal;
        }
        set
        {
            this.SetBrightness(value);
        }
    }

    [SerializeField]
    [Range(0, 1)]
    private float brightFogDensity = 0;
    [SerializeField]
    [Range(0, 1)]
    private float darkFogDensity = 0.05f;
    [SerializeField]
    [Range(0, 1)]
    private float initialBrightness = 1;
    [SerializeField]
    private float transitionSpeed = 1;

    // [0,1]
    private float brightnessVal;
    // [0,1]
    private float targetBrightness;

    private float initialEnvironmentIntensity;
    private float initialReflectionIntensity;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        this.brightnessVal = this.initialBrightness;
        this.targetBrightness = this.initialBrightness;

        this.initialEnvironmentIntensity = RenderSettings.ambientIntensity;
        this.initialReflectionIntensity = RenderSettings.reflectionIntensity;
    }

    void Update()
    {
        this.UpdateBrightness();

        this.UpdateFog();
        this.UpdateEnvironmentLighting();
    }

    public void SetBrightness(float brightness)
    {
        this.targetBrightness = Mathf.Clamp(brightness, 0, 1);
    }

    private void UpdateBrightness()
    {
        this.brightnessVal = Mathf.Lerp(this.brightnessVal, this.targetBrightness, this.transitionSpeed * Time.deltaTime);
        if (this.brightnessVal - 0 < 0.001)
        {
            this.brightnessVal = 0;
        }
        if (this.brightnessVal - 1 > -0.001)
        {
            this.brightnessVal = 1;
        }
    }

    private void UpdateFog()
    {
        RenderSettings.fogDensity = Mathf.Lerp(this.darkFogDensity, this.brightFogDensity, this.brightnessVal);
    }

    private void UpdateEnvironmentLighting()
    {
        RenderSettings.ambientIntensity = Mathf.Lerp(0, this.initialEnvironmentIntensity, this.brightnessVal);
        RenderSettings.reflectionIntensity = Mathf.Lerp(0, this.initialReflectionIntensity, this.brightnessVal);
    }
}
