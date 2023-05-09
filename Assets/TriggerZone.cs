using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public string targetTag = "Player"; // The tag to detect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(other.gameObject);
            Debug.Log("Object with tag " + targetTag + " entered trigger zone!");
            GameManager.instance.AddScore(1);
            // Do something here when the object with the specified tag enters the trigger zone
        }
    }
}
