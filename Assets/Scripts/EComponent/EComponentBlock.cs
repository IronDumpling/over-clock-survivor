using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EComponentBlock : MonoBehaviour
{
    public bool CanRun { get; set; }
    private void OnEnable()
    {
        EComponentManager.Instance.RegisterEC(this);
    }

    private void OnDisable()
    {
        EComponentManager.Instance.DeleteEC(this);
    }


    public void RunBlock()
    {
        if (!CanRun) return;
        this.transform.parent.GetComponent<EComponent>().RunByCurrent();
        CanRun = false;
    }
}
