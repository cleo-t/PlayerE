using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpikeBehaviour : MonoBehaviour
{
    public static event Action KillPlayerEvent;

    private BoxCollider sa;
    [SerializeField]
    // Meters
    private float distance = 0.5f;
    [SerializeField]
    // Seconds
    private float attackDuration = 0.125f;
    [SerializeField]
    // Seconds
    private float extendedDuration = 1.5f;
    [SerializeField]
    // Seconds
    private float resetDuration = 0.5f;
    [SerializeField]
    // Seconds
    private float waitDuration = 1.5f;
    [SerializeField]
    private State initialState = State.Waiting;

    [SerializeField]
    private AudioClip impactClip;
    [SerializeField]
    private AudioClip slideClip;

    private float timer;
    private State state;

    private Vector3 closedPosition;

    private enum State
    {
        Waiting = 0,
        Attacking,
        Extended,
        Resetting
    }

    void Start()
    {
        this.state = this.initialState;
        this.timer = 0;
        this.closedPosition = this.transform.position;
        this.sa = this.gameObject.GetComponentInChildren<BoxCollider>();
    }

    void FixedUpdate()
    {
        this.timer += Time.fixedDeltaTime;
        float stateDuration = this.GetStateTimeLimit(this.state);
        if (this.timer >= stateDuration)
        {
            this.timer -= stateDuration;
            this.state++;
            if (this.state > State.Resetting)
            {
                this.state = State.Waiting;
            }
            switch (this.state)
            {
                case State.Waiting:
                    this.EnterWaiting();
                    break;
                case State.Attacking:
                    this.EnterAttacking();
                    break;
                case State.Extended:
                    this.EnterExtended();
                    break;
                case State.Resetting:
                    this.EnterResetting();
                    break;
            }
        }
        switch (this.state)
        {
            case State.Waiting:
                this.WaitingUpdate();
                break;
            case State.Attacking:
                this.AttackingUpdate();
                break;
            case State.Extended:
                this.ExtendedUpdate();
                break;
            case State.Resetting:
                this.ResettingUpdate();
                break;
        }
    }

    private float GetStateTimeLimit(State state)
    {
        switch (this.state)
        {
            case State.Waiting:
                return this.waitDuration;
            case State.Attacking:
                return this.attackDuration;
            case State.Extended:
                return this.extendedDuration;
            case State.Resetting:
                return this.resetDuration;
            default:
                return 0;
        }
    }

    private void WaitingUpdate()
    {
        this.transform.position = this.GetWaitingPosition();
    }

    private void AttackingUpdate()
    {
        float lerpVal = Mathf.Min(this.timer / this.attackDuration, 1);
        this.transform.position = Vector3.Lerp(this.GetWaitingPosition(), this.closedPosition, lerpVal);
    }

    private void ExtendedUpdate()
    {
        this.transform.position = this.closedPosition;
    }

    private void ResettingUpdate()
    {
        float lerpVal = Mathf.Min(Mathf.Pow(this.timer / this.resetDuration, 2), 1);
        this.transform.position = Vector3.Lerp(this.closedPosition, this.GetWaitingPosition(), lerpVal);
    }

    private Vector3 GetWaitingPosition()
    {
        return this.closedPosition - (this.transform.forward * this.distance);
    }

    public void HitPlayer()
    {
        if (this.state == State.Attacking)
        {
            this.InvokeKillPlayer();
        }
    }

    private void InvokeKillPlayer()
    {
        if (KillPlayerEvent != null)
        {
            KillPlayerEvent.Invoke();
        }
    }

    private void EnterWaiting()
    {
        AudioSource.PlayClipAtPoint(this.impactClip, this.transform.position, 0.5f);
    }

    private void EnterAttacking()
    {
        // this.sa.enabled = true;
        AudioSource.PlayClipAtPoint(this.slideClip, this.transform.position, 0.25f);
    }

    private void EnterExtended()
    {
        AudioSource.PlayClipAtPoint(this.impactClip, this.transform.position, 2);
    }

    private void EnterResetting()
    {
        // this.sa.enabled = false;
        AudioSource.PlayClipAtPoint(this.slideClip, this.transform.position, 0.1f);
    }
}
