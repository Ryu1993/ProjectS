using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [SerializeField, Range(1,10)]
    private int timeAcceleration;
    private int gameSecond = 0;
    public int dayCount { get; private set; }
    private WaitForSecondsRealtime second;
    public UnityAction timeProgressAction;


    private void Awake()
    {
        second = new WaitForSecondsRealtime(1f);
    }


    private IEnumerator TimeCount()
    {
        gameSecond += 120 * timeAcceleration;
        if (gameSecond == 86400)
        {
            gameSecond = 0;
            dayCount++;
        }
        timeProgressAction?.Invoke();
        yield return second;
    }



}
