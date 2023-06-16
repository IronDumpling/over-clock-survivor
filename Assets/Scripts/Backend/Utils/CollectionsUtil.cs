using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionsUtil
{
    public static bool TryGetElement<T>(this List<T> list, int index, out T element)
    {
        if (index < list.Count)
        {
            element = list[index];
            return true;
        }
        element = default(T);
        Debug.LogWarning($"Try to access out of bound index: {index}");
        return false;
    }
}
