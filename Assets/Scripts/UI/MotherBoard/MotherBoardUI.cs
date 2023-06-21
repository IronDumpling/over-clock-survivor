using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class MotherBoardUI : MonoSingleton<MotherBoardUI>
{
    private GameObject _boardManager;
    private GameObject _cards;
    private GameObject _circuits;
    private Animator _ani;

    public void Awake()
    {
        _boardManager = transform.Find("BoardManager")?.gameObject;
        _cards = transform.Find("Cards")?.gameObject;
        _circuits = transform.Find("Circuits")?.gameObject;
        _ani = gameObject.GetComponent<Animator>();
    }

    public void HandleMotherBoard()
    {
        _ani.SetBool("HideBoard", !_ani.GetBool("HideBoard"));
    }
}
