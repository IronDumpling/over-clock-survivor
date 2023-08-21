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

    private RectTransform _rectTrans;
    private Vector2 _startPos;
    private readonly float _threshold = 0.05f;

    private HashSet<GameObject> hitObjs = new HashSet<GameObject>();

    private void Awake()
    {
        _rectTrans = GetComponent<RectTransform>();
        _startPos = _rectTrans.position;
    }

    private void Update()
    {
        if (IsNewLoop()) StartNewLoop();
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

        //Ray2D ray = new Ray2D(eventData.pointerCurrentRaycast.worldPosition, eventData.pointerCurrentRaycast.worldNormal);
        //if(Physics2D.Raycast(ray, out RaycastHit2D hit))
        //{
        //    Vector3 surfNorm = hit.normal;
        //}

        // Check if specific game objects are hit
        foreach (var result in results)
        {
            GameObject hitObj = result.gameObject;

            if (!hitObjs.Contains(hitObj))
            {
                hitObjs.Add(hitObj);
                hitObj.transform.GetComponent<CardCell>()?.Execute();
            }
        }
    }

    private bool IsNewLoop()
    {
        return Mathf.Abs(_rectTrans.position.x - _startPos.x) < _threshold &&
               Mathf.Abs(_rectTrans.position.y - _startPos.y) < _threshold;
    }

    private void StartNewLoop()
    {
        hitObjs.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position,transform.position +  -transform.up * raycastLength);
        Gizmos.DrawLine(transform.position, transform.position + transform.up * raycastLength);
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * raycastLength);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * raycastLength);
    }
}