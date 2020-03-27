using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

[Serializable]
public class EmployeeData
{
    public bool Employed;
    public bool HasOpenShifts;
    public string Name;
    public string Gender;
    public string Id;
    public string Title;
    public int MaxShifts;
    public List<ShiftData> OpenShifts;
    public List<ShiftData> WorkShifts;
}
[Serializable]
public class ShiftData
{
    public string Day;
    public string Shift;
    public string Title;
}
