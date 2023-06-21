using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class MotherBoardUI : MonoSingleton<MotherBoardUI>
{
    private GameObject _boardManager;
    private GameObject _cards;
    private GameObject _circuits;

    public void Awake()
    {
        _boardManager = transform.Find("BoardManager")?.gameObject;
        _cards = transform.Find("Cards")?.gameObject;
        _circuits = transform.Find("Circuits")?.gameObject;
    }

    public void HandleMotherBoard()
    {
        _boardManager.SetActive(!_boardManager.activeSelf);
        _cards.SetActive(!_cards.activeSelf);
        _circuits.SetActive(!_circuits.activeSelf);
    }
}
