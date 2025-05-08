using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimePeriod { Morning, Afternoon, Evening, }
public enum WeekDay { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }

public interface ITimeState
{
    void Enter();
    void Exit();
    TimePeriod GetTimePeriod();
}
