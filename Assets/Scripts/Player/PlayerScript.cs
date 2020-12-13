using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private MovementController movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementController>();
        movement.instance = this;
        movement.directionCallback = delegate() { return Input.GetAxisRaw("Horizontal"); };
        movement.jumpKeyDownCallback = delegate () { return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow); };
        movement.jumpKeyCallback = delegate () { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); };
        movement.jumpKeyUpCallback = delegate () { return Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow); };
    }

    // Update is called once per frame
    void Update()
    {
        movement.MovementFSM();
    }

}