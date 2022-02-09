using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{

    public float speed = 2f;
    public Color startColor;
    public Color endColor;

    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float rand = Random.Range(0.01f, 0.05f);
        float t = Mathf.Sin(speed * Time.time * rand) + 1;
        t /= 2;

        light.color = Color.Lerp(startColor, endColor, t);

        /*
        float intensity = Random.Range(1f, 1.5f);
        light.intensity = intensity * t;
        */
    }
}
