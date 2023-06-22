using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;
using Color = UnityEngine.Color;

public class ExecutePoint : MonoSingleton<ExecutePoint>
{
    private readonly float raycastLength = 40f;

    private void Update()
    {
        GenerateRayCast();
    }

    private void GenerateRayCast()
    {
        RaycastFromPosition(transform.position + Vector3.up * raycastLength);
        RaycastFromPosition(transform.position + Vector3.down * raycastLength);
        RaycastFromPosition(transform.position + Vector3.left * raycastLength);
        RaycastFromPosition(transform.position + Vector3.right * raycastLength);
    }

    void RaycastFromPosition(Vector3 position)
    {
        // Create a pointer event data and set its position
        PointerEventData eventData = new(EventSystem.current)
        {
            position = position
        };

        // Perform the raycast
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // Check if specific game objects are hit
        GameObject prevObj = null;
        foreach (var result in results)
        {
            if(prevObj != result.gameObject)
            {
                CellManager.Instance.ResetCellListRun();
            }

            prevObj = result.gameObject;

            result.gameObject.transform.GetComponent<CardCell>()?.Execute();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position,transform.position +  -transform.up * raycastLength);
        Gizmos.DrawLine(transform.position, transform.position + transform.up * raycastLength);
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * raycastLength);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * raycastLength);
    }

    //public LayerMask mask;
    //private Vector2 previousNormal = Vector2.zero;
    //private RaycastHit2D[] hitInfoList = new RaycastHit2D[4];
    //private void CheckRayCast()
    //{
    //    foreach (var hitInfo in hitInfoList)
    //    {
    //        if (hitInfo.collider != null)
    //        {
    //            if (previousNormal != hitInfo.normal)
    //            {
    //                CellManager.Instance.ResetCellListRun();
    //            }
    //            previousNormal = hitInfo.normal;

    //            hitInfo.transform.GetComponent<CardCell>()?.Execute();
    //        }
    //    }
    //}
}
