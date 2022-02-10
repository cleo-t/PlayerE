using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private float distance;
    [SerializeField]
    private float rideDuration;

    private bool started;
    private float timer;

    private Vector3 startPosition;

    void Start()
    {
        this.started = false;
        this.timer = 0;
        this.startPosition = this.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.started = true;
        }
        if (this.started)
        {
            this.timer += Time.deltaTime;
            float t = Mathf.Clamp(this.timer / this.rideDuration, 0, 1);
            float lerpVal = 1.0f / (1.0f + Mathf.Exp(-5 * (t - 1)));
            this.transform.position = Vector3.Lerp(this.startPosition, this.GetEndPosition(), lerpVal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.started = true;
        }
    }

    private Vector3 GetEndPosition()
    {
        return this.startPosition + (this.transform.up * this.distance);
    }
}
