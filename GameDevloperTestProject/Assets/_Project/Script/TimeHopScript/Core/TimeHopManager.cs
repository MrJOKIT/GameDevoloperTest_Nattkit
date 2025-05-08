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
    [SerializeField] private Transform clockHand;
    [SerializeField] private float timeInOneDay = 180f;
    private float afternoonTime;
    private float eveningTime;
    private float timeInOneDayCounter;
    private float clockHandRotateSpeed;
    
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
        if (currentState is MorningState) SetState(new AfternoonState());
        else if (currentState is AfternoonState) SetState(new EveningState());
        else if (currentState is EveningState)
        {
            AdvanceDay();
            SetState(new MorningState());
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
        
    }
    
    #endregion
    
    #region TimeProgression
    
    private void ClockHand()
    {
        clockHand.Rotate(0,0,-clockHandRotateSpeed * Time.deltaTime);
        timeInOneDayCounter += Time.deltaTime;
        if (timeInOneDayCounter >= eveningTime)
        {
            if (currentTimePeriod == TimePeriod.Evening) return;
            SetState(new EveningState());
        }
        else if (timeInOneDayCounter >= afternoonTime)
        {
            if (currentTimePeriod == TimePeriod.Afternoon) return;
            SetState(new AfternoonState());
        }
        else
        {
            if (currentTimePeriod == TimePeriod.Morning) return;
            SetState(new MorningState());
        }
        
        if (timeInOneDayCounter > timeInOneDay)
        {
            AdvanceDay();
            timeInOneDayCounter = 0f;
        }
    }
    
    //set ui
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
