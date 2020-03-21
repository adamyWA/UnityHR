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
        Employee = gameObject.GetComponent<Employee>(); //game may contain multiple employees, only get the employee attached to the same gameobject

        Employee.Quitted += HandleOnQuitted;
        Employee.CalledOut += HandleOnCalledOut;
        Employee.Covered += HandleOnCovered;
    }
    void HandleOnCovered(OpenShift shift)
    {
        Debug.Log(Employee.Name + " covered " + shift.Day + " " + shift.Shift);
    }
    void HandleOnQuitted(Schedule sched, Staff staff)
    {
        if(staff.Employees.Contains(Employee))
        {
            staff.Employees.Remove(Employee);
        }
        foreach(var shift in Employee.WorkShifts)
        {
            sched.OpenShift(shift);
        }
        Employee.Employed = false;
        Employee.HasOpenShifts = true;
    }
    void HandleOnCalledOut(Schedule sched, OpenShift shift)
    {
        if(!Employee.OpenShifts.Contains(shift)) //make sure the employee is working this shift
        {
            Employee.OpenShifts.Add(shift);
            sched.OpenShift(shift);
            Employee.HasOpenShifts = true;
        }
    }
}
