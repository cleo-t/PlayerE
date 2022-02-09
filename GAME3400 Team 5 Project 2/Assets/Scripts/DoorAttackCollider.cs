using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAttackCollider : MonoBehaviour
{
    [SerializeField]
    private AggressiveDoor ad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.ad.HitPlayer();
        }
    }
}
