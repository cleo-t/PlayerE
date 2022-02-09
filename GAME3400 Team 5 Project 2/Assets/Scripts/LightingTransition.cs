using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTransition : MonoBehaviour
{
    public static LightingTransition instance;

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
    private float brightness;
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
        this.brightness = this.initialBrightness;
        this.targetBrightness = this.initialBrightness;

        this.initialEnvironmentIntensity = RenderSettings.ambientIntensity;
        this.initialReflectionIntensity = RenderSettings.reflectionIntensity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.targetBrightness = this.targetBrightness == 1 ? 0 : 1;
        }

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
        this.brightness = Mathf.Lerp(this.brightness, this.targetBrightness, this.transitionSpeed * Time.deltaTime);
        if (this.brightness - 0 < 0.001)
        {
            this.brightness = 0;
        }
        if (this.brightness - 1 > -0.001)
        {
            this.brightness = 1;
        }
    }

    private void UpdateFog()
    {
        RenderSettings.fogDensity = Mathf.Lerp(this.darkFogDensity, this.brightFogDensity, this.brightness);
    }

    private void UpdateEnvironmentLighting()
    {
        RenderSettings.ambientIntensity = Mathf.Lerp(0, this.initialEnvironmentIntensity, this.brightness);
        RenderSettings.reflectionIntensity = Mathf.Lerp(0, this.initialReflectionIntensity, this.brightness);
    }
}
