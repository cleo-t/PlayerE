using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    public static event Action KillPlayerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.InvokeKillEvent();
        }
    }

    private void InvokeKillEvent()
    {
        if (KillPlayerEvent != null)
        {
            KillPlayerEvent.Invoke();
        }
    }
}
