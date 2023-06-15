using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _renderer;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
}
