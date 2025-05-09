using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class MorningState : ITimeState
{
    public void Enter() => Debug.Log("Morning begins");
    public void Exit() => Debug.Log("Leaving Morning");
    public TimePeriod GetTimePeriod() => TimePeriod.Morning;
}

public class AfternoonState : ITimeState
{
    public void Enter() => Debug.Log("Afternoon begins");
    public void Exit() => Debug.Log("Leaving Afternoon");
    public TimePeriod GetTimePeriod() => TimePeriod.Afternoon;
}

public class EveningState : ITimeState
{
    public void Enter() => Debug.Log("Evening begins");
    public void Exit() => Debug.Log("Leaving Evening");
    public TimePeriod GetTimePeriod() => TimePeriod.Evening;
}
public class TimeHopManager : MonoBehaviour
{
    public static TimeHopManager Instance { get; private set; }

    public static Action<TimePeriod, int, WeekDay> OnTimeChanged;

    [Header("Time Manager")]
    private ITimeState currentState;
    [SerializeField] private TimePeriod currentTimePeriod;
    [SerializeField] private int dayCount = 1;
    [SerializeField] private WeekDay currentDay = WeekDay.Monday;
    
    [Space(10)]
    [Header("Time Progression")]
    [SerializeField] private bool timeStopped = false;
    [SerializeField] private Transform clockHand;
    [SerializeField] private float timeInOneDay = 180f;
    private float afternoonTime;
    private float eveningTime;
    private float timeInOneDayCounter;
    private float clockHandRotateSpeed;

    [Space(10)]
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private bool onTimeSkip;
    public bool OnTimeSkip => onTimeSkip;
    [SerializeField] private TimePeriod skipTargetPeriod;
    [SerializeField] private float skipSpeed = 10;
    private float skipSpeedTime = 1f;
    
    [Space(10)]
    [Header("Time UI")]
    [SerializeField] private TextMeshProUGUI dayCountText;
    [SerializeField] private TextMeshProUGUI weekDayText;
    
    [Header("Time Enviroment")]
    [SerializeField] private Camera camera;
    [SerializeField] private float fadeDuration = 1.5f;
    private Coroutine fadeRoutine;
    
    [SerializeField] private Color morningColor = new Color(0.8f, 0.9f, 1f);
    [SerializeField] private Color afternoonColor = new Color(1f, 0.95f, 0.8f);
    [SerializeField] private Color eveningColor = new Color(0.2f, 0.2f, 0.4f);

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        afternoonTime = timeInOneDay / 2;
        eveningTime = (afternoonTime / 2) / 2 + timeInOneDay / 2;

        clockHandRotateSpeed = 360f / timeInOneDay;
        SetState(new MorningState());
        SetTimeUI();
    }

    private void Update()
    {
        ClockHand();
    }

    #region TimeHop
    [ContextMenu("Trigger Time Hop")]
    public void TriggerTimeHop()
    {
        if (currentState is MorningState) SkipTimePeriod(TimePeriod.Afternoon);
        else if (currentState is AfternoonState) SkipTimePeriod(TimePeriod.Evening);
        else if (currentState is EveningState)
        {
            SkipTimePeriod(TimePeriod.Morning);
        }
    }

    private void AdvanceDay()
    {
        dayCount++;
        currentDay = (WeekDay)(((int)currentDay + 1) % 7);
        SetTimeUI();
    }

    private void SetState(ITimeState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();

        OnTimeChanged?.Invoke(currentState.GetTimePeriod(), dayCount, currentDay);
        currentTimePeriod = currentState.GetTimePeriod();
        LightChanged(currentTimePeriod);
        Announcement.Instance.SetAnnouncementText(currentTimePeriod.ToString());
    }
    
    private void ClockHand()
    {
        if (timeStopped) return;
        
        clockHand.Rotate(0,0,-clockHandRotateSpeed * skipSpeedTime * Time.deltaTime);
        timeInOneDayCounter += skipSpeedTime * Time.deltaTime;
        if (timeInOneDayCounter >= eveningTime)
        {
            if (onTimeSkip)
            {
                if (skipTargetPeriod == TimePeriod.Evening)
                {
                    SkipSuccess();
                    SetState(new EveningState());
                }
            }
            else
            {
                if (currentTimePeriod != TimePeriod.Evening)
                {
                    SetState(new EveningState());
                }
            }
            
        }
        else if (timeInOneDayCounter >= afternoonTime)
        {
            if (onTimeSkip)
            {
                if (skipTargetPeriod == TimePeriod.Afternoon)
                {
                    SkipSuccess();
                    SetState(new AfternoonState());
                }
            }
            else
            {
                if (currentTimePeriod != TimePeriod.Afternoon)
                {
                    SetState(new AfternoonState());
                }
            }
        }
        else
        {
            if (onTimeSkip)
            {
                if (skipTargetPeriod == TimePeriod.Morning)
                {
                    SkipSuccess();
                    SetState(new MorningState());
                }
            }
            else
            {
                if (currentTimePeriod != TimePeriod.Morning)
                {
                    SetState(new MorningState());
                }
            }
            
        }
        
        if (timeInOneDayCounter > timeInOneDay)
        {
            if (onTimeSkip)
            {
                if (skipTargetPeriod == TimePeriod.Morning)
                {
                    SkipSuccess();
                    SetState(new MorningState());
                }
            }
            AdvanceDay();
            timeInOneDayCounter = 0f;
        }
    }

    private void SkipTimePeriod(TimePeriod skipTargetPeriod)
    {
        if (onTimeSkip)
        {
            return;
        }
        loadingCanvas.SetActive(true);
        onTimeSkip = true;
        this.skipTargetPeriod = skipTargetPeriod;
        skipSpeedTime = skipSpeed * clockHandRotateSpeed;

    }

    private void SkipSuccess()
    {
        loadingCanvas.SetActive(false);
        onTimeSkip = false;
        skipSpeedTime = 1f;
    }

    #endregion
    
    #region TimeUI
    
    private void SetTimeUI()
    {
        dayCountText.text = "DAY " + dayCount;
        weekDayText.text = currentDay.ToString();
    }
    
    private void LightChanged(TimePeriod time)
    {
        Color targetColor = camera.backgroundColor;

        switch (time)
        {
            case TimePeriod.Morning:
                targetColor = morningColor;
                break;
            case TimePeriod.Afternoon:
                targetColor = afternoonColor;
                break;
            case TimePeriod.Evening:
                targetColor = eveningColor;
                break;
        }
        
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);
        
        fadeRoutine = StartCoroutine(FadeToColor(targetColor));
    }
    
    private IEnumerator FadeToColor(Color targetColor)
    {
        Color startColor = camera.backgroundColor;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            camera.backgroundColor = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        camera.backgroundColor = targetColor;
    }
    #endregion
}
