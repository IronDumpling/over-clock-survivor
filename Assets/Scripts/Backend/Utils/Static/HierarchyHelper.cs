using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HierarchyHelper
{
    /// <summary>
    /// 使用递归查找名字为name的tr的子物体，若没有名为name的子物体则返回null
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="name"></param>
    /// <returns>null或者名为name的Transform</returns>
    public static Transform GetChildRecursion(Transform tr, string name)
    {
        if (tr.name == name)
        {
            return tr;
        }
        for (int i = 0; i < tr.childCount; i++)
        {
            Transform tempTR = GetChildRecursion(tr.GetChild(i), name);
            if (tempTR != null)
            {
                return tempTR;
            }
        }
        return null;
    }
    /// <summary>
    /// 非递归版
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Transform GetChild(Transform tr, string name)
    {
        Stack<Transform> trStack = new Stack<Transform>();
        trStack.Push(tr);
        while (trStack.Count > 0)
        {
            Transform curTR = trStack.Pop();
            if (curTR.name == name)
            {
                return curTR;
            }
            for (int i = 0; i < curTR.childCount; i++)
            {
                trStack.Push(curTR.GetChild(i));
            }
        }
        return null;
    }

    /// <summary>
    /// find a component in a child gameobject, return null if dont have this object or this object dont have this T component
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="name"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetChild<T>(Transform tr, string name) where T : Component
    {
        Transform targetTR = GetChild(tr, name);
        if (targetTR == null)
        {
            return null;
        }
 
        return targetTR.GetComponent<T>();
    }
    /// <summary>
    /// 根据组件找包含组件的子辈，只能取到子辈，不能取到孙辈
    /// </summary>
    /// <param name="tr"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetChildren<T>(Transform tr) where T : Component
    {
        List<T> typeArr = new List<T>();
        
        for (int i = 0; i < tr.childCount; i++)
        {
            T typeObj = tr.GetChild(i).GetComponent<T>();
            if (typeObj == null) continue;
            typeArr.Add(typeObj);

        }
        if (typeArr.Count != 0)
        {
            return typeArr; 
        }

        return null;

    }
    
}