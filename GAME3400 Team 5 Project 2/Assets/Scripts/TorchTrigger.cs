using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    public event Action torchOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.InvokeOnEvent();
        }
    }

    private void InvokeOnEvent()
    {
        if (torchOn != null)
        {
            torchOn.Invoke();
        }
    }
}
