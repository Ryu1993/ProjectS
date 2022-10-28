using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField]
    Image screenLight;
    [SerializeField, Range(1,1000)]
    private int timeAcceleration;
    [SerializeField]
    private float gameSecond = 0;
    public int dayCount { get; private set; }
    private WaitForSecondsRealtime realSecond;
    public UnityAction timeProgressAction;
    public UnityAction dayProgressAction;
    public UnityAction halfDayProgressAction;
    public UnityAction halfhalfDayProgressAction;
    private bool isPause;

    private IEnumerator Start()
    {
        realSecond = new WaitForSecondsRealtime(1f);
        while(true)
        {
            if(!isPause)
            {
                gameSecond += timeAcceleration;
                if(gameSecond%240 == 0)
                {
                    halfhalfDayProgressAction?.Invoke();
                }
                if(gameSecond%720 == 0)
                {
                    halfDayProgressAction?.Invoke();
                }
                if (gameSecond >= 1440)
                {
                    gameSecond = 0;
                    dayCount++;
                    dayProgressAction?.Invoke();
                }
                timeProgressAction?.Invoke();
                ScreenLightSet();
            }
            yield return realSecond;
        }
    }

    public void ScreenLightSet()
    {
        Color nextColor = screenLight.color;
        if(gameSecond>720)
        {
            nextColor.a = (1440 - gameSecond) / 1440 / 1.5f;
        }
        else
        {
            nextColor.a = gameSecond / 1440 / 1.5f;
        }
        screenLight.color = nextColor;
    }





}
