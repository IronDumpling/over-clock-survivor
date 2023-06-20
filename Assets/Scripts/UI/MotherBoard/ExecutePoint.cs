using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Common;
using UnityEditor.PackageManager;
using UnityEngine;
using Color = UnityEngine.Color;

public class ExecutePoint : MonoSingleton<ExecutePoint>
{
    public LayerMask mask;

    private Vector2 previousNormal = Vector2.zero;
    RaycastHit2D[] hitInfoList = new RaycastHit2D[4];

    private void Update()
    {
        GenerateRayCast();
        CheckRayCast();
    }

    private void GenerateRayCast()
    {
        hitInfoList[0] = Physics2D.Raycast(transform.position, -transform.up, 0.6f, mask.value);
        hitInfoList[1] = Physics2D.Raycast(transform.position, -transform.right, 0.6f, mask.value);
        hitInfoList[2] = Physics2D.Raycast(transform.position, transform.up, 0.6f, mask.value);
        hitInfoList[3] = Physics2D.Raycast(transform.position, transform.right, 0.6f, mask.value);
    }

    private void CheckRayCast()
    {
        foreach (var hitInfo in hitInfoList)
        {
            if (hitInfo.collider != null)
            {
                if (previousNormal != hitInfo.normal)
                {
                    CellManager.Instance.ResetCellListRun();
                }
                previousNormal = hitInfo.normal;

                hitInfo.transform.GetComponent<CardCell>().Execute();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position,transform.position +  -transform.up * 0.6f);
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.6f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * 0.6f);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 0.6f);
    }
}
