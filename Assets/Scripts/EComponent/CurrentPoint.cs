using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Common;
using UnityEngine;
using Color = UnityEngine.Color;

public class CurrentPoint : MonoSingleton<CurrentPoint>
{
    public LayerMask mask;

    private Vector2 previousNormal = Vector2.zero;
    private void Update()
    {
        
        RaycastHit2D  hitInfo = Physics2D.Raycast(transform.position, -transform.up, 0.6f,mask.value);
        if(hitInfo.collider != null) {
            if (previousNormal != hitInfo.normal)
            {
                EComponentManager.Instance.ResetECListRun();
                Debug.Log("reset");
            }
            previousNormal = hitInfo.normal;
            
            hitInfo.transform.GetComponent<EComponentBlock>().RunBlock();
            
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position,transform.position +  -transform.up * 0.6f);
    }
}
