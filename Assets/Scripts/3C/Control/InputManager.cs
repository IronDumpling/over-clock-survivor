using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class InputManager : MonoSingleton<InputManager>
{
    private void FixedUpdate()
    {
        // Player Move 
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Player.Instance.HandleMovement(horizontal, vertical);

        // Call MotherBroad
        if (Input.GetKeyDown(KeyCode.Space)) MotherBoardUI.Instance.HandleMotherBoard();

        // Select UI Game Objects
        if (Input.GetMouseButtonDown(0)) UIManager.Instance.HandleSelect();
    }
}
