using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _renderer;

    private void Awake()
    {
        Init();
    }

    public Tile()
    {

    }

    protected virtual void Init()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
}
