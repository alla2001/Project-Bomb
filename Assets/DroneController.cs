using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{


    public Camera Camera1;
    public Camera Camera2;

    public static DroneController instance;
    public enum States
    {
        Movement,Arm


    }

    public States currentState { get; private set; }


    public float Speed;
    public float angularSpeed;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else

            Destroy(this);

    }
 
    public void ChangeState(States newstate)
    {

        currentState=newstate;

        switch (newstate)
        {

            case States.Movement:
                Camera1.gameObject.SetActive(false);
                Camera2.gameObject.SetActive(true);
                break;
            case States.Arm:
                Camera1.gameObject.SetActive(true);
                Camera2.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (InputManager.instance.spaceDown)
        {
            if ((int)currentState >= 1)
            {
                ChangeState(0);
            }
            else
            {
                ChangeState(currentState + 1);
            }
         
        }
        if (InputManager.instance.rotaryclick)
        {
            if ((int)currentState >= 1)
            {
                ChangeState(0);
            }
            else
            {
                ChangeState(currentState + 1);
            }

        }
    }

    private void FixedUpdate()
    {
        if (currentState != States.Movement) return;
        transform.position += transform.forward * Speed*0.01f * InputManager.instance.movementInput.y;
        transform.Rotate(Vector3.up * angularSpeed * 0.01f * InputManager.instance.movementInput.x);
    }



}
