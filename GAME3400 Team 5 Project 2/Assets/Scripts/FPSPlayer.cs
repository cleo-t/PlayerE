using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSPlayer : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float turnSpeed;

    private CharacterController cc;

    void Start()
    {
        this.cc = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        float turnInput = this.GetTurnInput();
        this.Turn(turnInput);
    }

    void FixedUpdate()
    {
        Vector3 moveInput = this.GetMoveInput();
        this.Move(moveInput);
        this.Gravity();
    }

    private Vector3 GetMoveInput()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 inputVec = new Vector3(horInput, 0, verInput);
        if (inputVec.magnitude > 1)
        {
            inputVec.Normalize();
        }

        return inputVec;
    }

    private float GetTurnInput()
    {
        return Input.GetAxis("Mouse X");
    }
    
    private void Turn(float turnInput)
    {
        this.transform.rotation *= Quaternion.AngleAxis(turnInput * this.turnSpeed * Time.deltaTime, Vector3.up);
    }
    
    private void Move(Vector3 moveInput)
    {
        Vector3 movement = (this.transform.right * moveInput.x
            + this.transform.forward * moveInput.z) * this.moveSpeed;
        this.cc.SimpleMove(movement * Time.fixedDeltaTime);
    }

    private void Gravity()
    {

    }
}
