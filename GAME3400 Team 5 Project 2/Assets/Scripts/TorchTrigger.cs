using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    public event Action torchOn;
    private bool triggered;

    private void Start()
    {
        this.triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.triggered && other.CompareTag("Player"))
        {
            this.triggered = true;
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
