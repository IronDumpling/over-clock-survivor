using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;

public class UIManager : MonoSingleton<UIManager>
{
    public void HandleSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log($"{EventSystem.current.currentSelectedGameObject?.name}");
        }
    }
}
