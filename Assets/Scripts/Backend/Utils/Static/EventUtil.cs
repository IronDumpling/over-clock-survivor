using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatChangeEvent : UnityEvent<float>
{

}

[System.Serializable]
public class TwoFloatChangeEvent : UnityEvent<float, float>
{

}

[System.Serializable]
public class FourFloatChangeEvent : UnityEvent<float, float, float, float>
{

}

[System.Serializable]
public class IntChangeEvent : UnityEvent<int>
{

}
