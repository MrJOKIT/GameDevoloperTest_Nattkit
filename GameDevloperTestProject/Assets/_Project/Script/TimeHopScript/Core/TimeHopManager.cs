using System;
using System.Collections;
using System.Collections.Generic;
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
    [Header("Time UI")]
    [SerializeField] private GameObject timeUICanvas;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SetState(new MorningState());
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
    }

    private void SetState(ITimeState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();

        OnTimeChanged?.Invoke(currentState.GetTimePeriod(), dayCount, currentDay);
        currentTimePeriod = currentState.GetTimePeriod();
    }
    
    #endregion
    
    #region TimeUI

    private void TimeHopUI()
    {
        
    }
    
    #endregion
}
