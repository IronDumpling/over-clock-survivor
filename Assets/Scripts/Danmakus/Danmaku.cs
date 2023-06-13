using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku : MonoBehaviour
{
    public float dmg = 5;
    public float speed = 10f;

    private void Awake()
    {
        Invoke(nameof(DestroySelf), 5f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime;
    }
}
