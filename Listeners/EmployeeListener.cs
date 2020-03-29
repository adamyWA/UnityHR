using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class EmployeeListener : MonoBehaviour
{
    public Employee Employee;
    public Staff Staff;

    private void Start()
    {
        Employee.Quitted += HandleOnQuitted;
        Employee.CalledOut += HandleOnCalledOut;
        Employee.Covered += HandleOnCovered;
        Employee.RemovedFromShift += HandleOnRemovedFromShift;
    }
    void HandleOnRemovedFromShift(OpenShift shift)
    {
        Debug.Log("FROM EMPLOYEELISTENER: " + Employee.Name + " was removed from " + shift.Day + " " + shift.Shift + " as a " + shift.RequiredTitle);
    }
    void HandleOnCovered(OpenShift shift)
    {
        Debug.Log("FROM EMPLOYEELISTENER: " + Employee.Name + " covered " + shift.Day + " " + shift.Shift + " as a " + shift.RequiredTitle);
    }
    void HandleOnQuitted(Schedule sched, Staff staff)
    {
        staff.EmployeeExit(Employee);
        foreach(var shift in Employee.WorkShifts)
        {
            sched.OpenShift(Employee, shift);
            if (!Employee.OpenShifts.Contains(shift))
                Employee.OpenShifts.Add(shift);
        }
        foreach (var openShift in Employee.OpenShifts)
        {
            if (Employee.OpenShifts.Contains(openShift))
                Employee.WorkShifts.Remove(openShift);

        }
        Employee.Employed = false;
        Employee.HasOpenShifts = true;
    }

    void HandleOnCalledOut(Schedule sched, OpenShift shift)
    {
        if(!Employee.OpenShifts.Contains(shift)) //make sure the employee is working this shift
        {
            Employee.OpenShifts.Add(shift);
            sched.OpenShift(Employee, shift);
            Employee.HasOpenShifts = true;
        }
    }
}
