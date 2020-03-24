using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class ScheduleListener : MonoBehaviour {

    public Schedule Schedule;
    public Staff Staff;
    // Use this for initialization
    void Start () {
        Schedule.ShiftCovered += HandleOnShiftCovered;
        Schedule.ShiftOpened += HandleOnShiftOpened;
    }
	void HandleOnShiftCovered(Employee emp, OpenShift shift)
    {
        Debug.Log("Scheduled shift for " + shift.RequiredTitle + " "+ shift.Day + " " + shift.Shift + " was covered by " + emp.Name + "!");
    }
    void HandleOnShiftOpened(OpenShift shift)
    {
        Debug.Log(shift.RequiredTitle + " " + shift.Shift + "shift on " + shift.Day + " was opened!");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
