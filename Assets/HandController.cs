using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class HandController : MonoBehaviour
{
    public Transform traget;
    public float speed;
    public Magnet magnet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DroneController.instance.currentState != DroneController.States.Arm)
            return;
       
     
        MoveArm( new Vector3(InputManager.instance.movementInput.x, InputManager.instance.movementInput.y, InputManager.instance.zForward*10) * speed*Time.deltaTime);

       
    }

    public void MoveArm(Vector3 delta)
    {
        traget.Translate(delta,Space.World);
    }
}
