using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetStrength = 10f;
    public bool magnetOn = true;
    public float dampingFactor = 0.8f;
    private void OnTriggerStay(Collider other)
    {
        if (magnetOn && other.attachedRigidbody != null)
        {
        
         
            Vector3 forceDirection = (transform.position - other.transform.position).normalized;
            float distance = Vector3.Distance(transform.position, other.transform.position);
            float forceMagnitude = (magnetStrength / (distance+1)) - ( distance)* dampingFactor;
            other.attachedRigidbody.AddForce( forceMagnitude * forceDirection, ForceMode.Acceleration);
            InputManager.instance.buzz();
        }
    }

    public void ToggleMagnet()
    {
        magnetOn = !magnetOn;
    }
}
