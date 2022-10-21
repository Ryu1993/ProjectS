using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Parameter
{
    public static readonly int speed = Animator.StringToHash("Speed");
    public static readonly int jump = Animator.StringToHash("Jump");
    public static readonly int mainTex = Shader.PropertyToID("_MainTex");
}
