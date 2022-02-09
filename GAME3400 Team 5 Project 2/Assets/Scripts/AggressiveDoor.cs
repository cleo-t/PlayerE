using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveDoor : MonoBehaviour
{
    [SerializeField]
    // Meters
    private float distance = 10;
    [SerializeField]
    // Seconds
    private float attackDuration = 1.5f;
    [SerializeField]
    // Seconds
    private float closedDuration = 1.5f;
    [SerializeField]
    // Seconds
    private float resetDuration = 3.5f;
    [SerializeField]
    // Seconds
    private float waitDuration = 0;
    [SerializeField]
    private State initialState = State.Waiting;

    private float timer;
    private State state;

    private Vector3 closedPosition;

    private enum State
    {
        Waiting = 0,
        Attacking,
        Closed,
        Resetting
    }

    void Start()
    {
        this.state = this.initialState;
        this.timer = 0;
        this.closedPosition = this.transform.position;
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
        }
        switch(this.state)
        {
            case State.Waiting:
                this.WaitingUpdate();
                break;
            case State.Attacking:
                this.AttackingUpdate();
                break;
            case State.Closed:
                this.ClosedUpdate();
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
            case State.Closed:
                return this.closedDuration;
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
        float lerpVal = Mathf.Min(Mathf.Pow(this.timer / this.attackDuration, 2), 1);
        this.transform.position = Vector3.Lerp(this.GetWaitingPosition(), this.closedPosition, lerpVal);
    }

    private void ClosedUpdate()
    {
        this.transform.position = this.closedPosition;
    }

    private void ResettingUpdate()
    {
        float lerpVal = Mathf.Min(Mathf.Pow(this.timer / this.resetDuration, 1.5f), 1);
        this.transform.position = Vector3.Lerp(this.closedPosition, this.GetWaitingPosition(), lerpVal);
    }

    private Vector3 GetWaitingPosition()
    {
        return this.closedPosition - (this.transform.forward * this.distance);
    }
}
