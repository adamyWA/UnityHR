using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class ScheduleData
{
    public bool HasOpenShifts;
    public string Name { get; set; }
    public int Id { get; set; }
    public List<OpenShift> OpenShifts { get; set; }
}
