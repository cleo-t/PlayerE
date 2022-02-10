using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public TorchTrigger Trigger;

    public float speed = 2f;
    public Color startColor;
    public Color endColor;

    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
        Trigger.torchOn += this.turningOnTorch;
    }

    // Update is called once per frame
    void Update()
    {
        //if (light.enabled == true) {
            float rand = Random.Range(0.01f, 0.05f);
            float t = Mathf.Sin(speed * (Time.time % (2 * Mathf.PI))) + 1;
            t /= 2;

            light.color = Color.Lerp(startColor, endColor, t);
        //}

        /*
        float intensity = Random.Range(1f, 1.5f);
        light.intensity = intensity * t;
        */
    }

    void turningOnTorch() {
        light.enabled = true;
    }
}
