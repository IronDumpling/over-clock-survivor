using System;

public class Timer
{
    private float interval;
    private float currentTime;
    private Action callback;

    public Timer(float interval, Action callback)
    {
        this.interval = interval;
        this.callback = callback;
    }

    public void Start()
    {
        currentTime = interval;
    }

    public void Update(float deltaTime)
    {
        currentTime -= deltaTime;
        if (currentTime <= 0f)
        {
            callback.Invoke();
            currentTime = interval;
        }
    }
}
