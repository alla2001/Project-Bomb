using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPartIKHand : MonoBehaviour
{
    public Transform target;
    public Transform upperArm;
    public Transform forearm;

    public float upperArmLength;
    public float forearmLength;

    void Update()
    {
        // Calculate the distance between the target and the upper arm
        float distance = Vector3.Distance(target.position, upperArm.position);

        // Calculate the angle between the upper arm and the target
        float angle = Mathf.Acos((upperArmLength * upperArmLength + distance * distance - forearmLength * forearmLength) / (2 * upperArmLength * distance)) * Mathf.Rad2Deg;

        // Rotate the upper arm towards the target
        upperArm.LookAt(target.position, transform.up);
        upperArm.Rotate(angle, 0, 0, Space.Self);

        // Rotate the forearm towards the target
        forearm.LookAt(target.position, transform.up);
    }
}