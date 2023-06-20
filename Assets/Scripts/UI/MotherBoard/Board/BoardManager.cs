using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private Vector3 _midPoint;
    private int _width, _height;
    private int _widthScale, _heightScale;

    private void Awake()
    {
        _midPoint = new Vector3(gameObject.transform.position.x + (float)_width * _widthScale / 2 - 0.5f,
                        gameObject.transform.position.y + (float)_height * _heightScale / 2 - 0.5f);
    }

    private void Start()
    {
        SetCameraFollow();
    }

    private void SetCameraFollow()
    {
        CameraManager.Instance.boardFollowPoint.position = _midPoint;
    }
}
