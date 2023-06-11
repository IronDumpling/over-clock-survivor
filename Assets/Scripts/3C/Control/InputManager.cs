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
        PlayerController.Instance.HandleMovement(horizontal, vertical);

        // Call MotherBroad
        if (Input.GetKeyDown(KeyCode.Space)) HandleMotherBoard();
    }

    private void HandleMotherBoard()
    {
        int idx = CameraManager.Instance.GetCurCameraIdx();
        if (idx == 0)
        {
            CameraManager.Instance.SwitchCameraByIdx(idx + 1);
            GameManager.Instance.PauseGame();
        }
        else
        {
            CameraManager.Instance.SwitchCameraByIdx(idx - 1);
            GameManager.Instance.ResumeGame();
        }
            
    }
}
