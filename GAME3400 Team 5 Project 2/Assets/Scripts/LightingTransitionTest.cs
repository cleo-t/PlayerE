using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTransitionTest : MonoBehaviour
{
    private int target = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.target = target == 1 ? 0 : 1;
        }
        LightingTransition.instance.brightness = target;
    }
}
