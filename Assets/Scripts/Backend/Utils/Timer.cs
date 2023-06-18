using System;
using UnityEngine;

public class Timer
{
    private float interval;
    private float currentTime;

    private Action emptyCallback;
    private Action<GameObject> objCallback;

    public Timer(float interval)
    {
        this.interval = interval;
    }

    public Timer(float interval, Action callback) : this(interval)
    {
        this.emptyCallback = callback;
    }

    public Timer(float interval, Action<GameObject> callback) : this(interval)
    {
        this.objCallback = callback;
    }

    public void Start()
    {
        currentTime = interval;
    }

    public void Update(float deltaTime, GameObject obj)
    {
        currentTime -= deltaTime;
        if (currentTime <= 0f)
        {
            objCallback?.Invoke(obj);
            emptyCallback?.Invoke();
            currentTime = interval;
        }
    }
}
