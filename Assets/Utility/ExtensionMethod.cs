using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class ExtensionMethod
{

    public static bool FindComponentChild<T>(this Transform transform, out T target) where T : UnityEngine.Component
    {
        for(int i= 0; i<transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out target))
            {
                return true;
            }
        }
        target = null;
        return false;
    }



}
