using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class Schedule : MonoBehaviour {

    public bool HasOpenShifts;
    public string Name { get; set; }
    public int Id { get; set; }
    public List<OpenShift> OpenShifts { get; set; }

    public event Action<Employee, OpenShift> ShiftCovered;
    public event Action<OpenShift> ShiftOpened;

	// Use this for initialization
	void Start () {
        #region Initialize
        HasOpenShifts = true;
        Id = 1337;
        Name = "Default Schedule";
        OpenShifts = new List<OpenShift> {
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Morning },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Evening },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Night },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Morning },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Evening },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Night },
        };
        #endregion
    }
	public void CoverShift(Employee emp, OpenShift shift)
    {
        if (ShiftCovered != null)
        {
            if (OpenShifts.Contains(shift) && emp.OpenShifts.Contains(shift)) //schedule has an openshift, employee doesnt
            {
                emp.Cover(shift);
                OpenShifts.Remove(shift);
                if(OpenShifts.Count == 0)
                {
                    HasOpenShifts = false;
                }
                ShiftCovered(emp, shift);
            }
        }
    }
    public void OpenShift(OpenShift shift)
    {
        if (ShiftOpened != null)
        {
            if (!OpenShifts.Contains(shift))
            {
                OpenShifts.Add(shift);
                HasOpenShifts = true;
                ShiftOpened(shift);
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
