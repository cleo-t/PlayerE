using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField]
    public float turnSpeed;
    [SerializeField]
    public float minAngle;
    [SerializeField]
    public float maxAngle;

    public float angle;

    private void Start()
    {
        this.angle = 0;
    }

    void Update()
    {
        float turnInput = this.GetTurnInput();
        this.Turn(turnInput);
    }

    private float GetTurnInput()
    {
        return Input.GetAxis("Mouse Y");
    }

    private void Turn(float turnInput)
    {
        this.angle += -turnInput * this.turnSpeed * Time.deltaTime;
        this.angle = Mathf.Clamp(this.angle, -this.maxAngle, -this.minAngle);
        this.transform.localRotation = Quaternion.AngleAxis(this.angle, Vector3.right);
    }
}
