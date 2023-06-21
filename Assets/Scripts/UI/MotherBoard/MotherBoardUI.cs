using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class MotherBoardUI : MonoSingleton<MotherBoardUI>
{
    private GameObject _boardManager;

    public void Awake()
    {
        _boardManager = transform.Find("BoardManager").gameObject;
    }

    public void HandleMotherBoard()
    {
        _boardManager.SetActive(!_boardManager.activeSelf);
    }
}
