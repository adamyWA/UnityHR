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
    public event Action<Employee, OpenShift> ShiftOpened;

    private bool _scheduleInitialized;

	// Use this for initialization
	void Start () {
        #region Initialize
        HasOpenShifts = true;
        Id = 1337;
        Name = "Default Schedule";
        
        #endregion
    }
	public void CoverShift(Employee emp, OpenShift shift)
    {
        if (ShiftCovered != null)
        {
            if (OpenShifts.Contains(shift) && emp.OpenShifts.Contains(shift)) //schedule has an openshift on this date, so does employee
            {
                emp.Cover(shift);
                if (OpenShifts.Count == 0)
                {
                    HasOpenShifts = false;
                }
                ShiftCovered(emp, shift);
            }
        }
    }
    public void OpenShift(Employee emp, OpenShift shift)
    {
        if (ShiftOpened != null)
        {
            emp.RemoveFromShift(shift);
            ShiftOpened(emp, shift);
        }
    }
    private List<OpenShift> InitializeSchedule()
    {
        #region Create a week of OpenShifts
        var retVal = new List<OpenShift> {
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Morning, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Morning, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Morning, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Morning, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Evening, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Evening, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Evening, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Evening, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Night, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Night, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Morning, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Morning, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Morning, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Morning, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Evening, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Evening, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Evening, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Evening, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Night, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Tuesday, Shift = Shift.Night, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Morning, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Morning, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Morning, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Morning, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Evening, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Evening, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Evening, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Evening, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Night, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Wednesday, Shift = Shift.Night, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Morning, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Morning, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Morning, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Morning, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Evening, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Evening, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Evening, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Evening, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Night, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Thursday, Shift = Shift.Night, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Morning, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Morning, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Morning, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Morning, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Evening, RequiredTitle = Title.Manager },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Evening, RequiredTitle = Title.Marketer },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Evening, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Evening, RequiredTitle = Title.Security },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Night, RequiredTitle = Title.Janitor },
        new OpenShift { Day = DayOfWeek.Friday, Shift = Shift.Night, RequiredTitle = Title.Security },

        };
        #endregion
        _scheduleInitialized = true;
        return retVal;
    }
    // Update is called once per frame
    void Update () {
		if(!_scheduleInitialized)
        {
            OpenShifts = InitializeSchedule();
        }
	}
}
