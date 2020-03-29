using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class StaffListener : MonoBehaviour {

    public Staff Staff;
	// Use this for initialization
	void Start () {
        Staff.Hired += HandleOnHired;
        Staff.Fired += HandleOnFired;
        Staff.EmployeeExited += HandleOnEmployeeExited;
	}
	void HandleOnHired(Employee emp)
    {
        emp.Employed = true;
        Debug.Log(emp.Name + " was Hired!");
    }
    void HandleOnFired(Employee emp, Schedule sched)
    {
        List<OpenShift> shiftsToRemove = new List<OpenShift>();
        foreach(var shift in emp.WorkShifts)
        {
            shiftsToRemove.Add(shift);
            if (!emp.OpenShifts.Contains(shift))
                emp.OpenShifts.Add(shift);
        }
        foreach(var openShift in emp.OpenShifts)
        {
            if (emp.WorkShifts.Contains(openShift))
                emp.WorkShifts.Remove(openShift);

        }
        foreach(var shift in shiftsToRemove)
            sched.OpenShift(emp, shift);

        emp.Employed = false;
        emp.HasOpenShifts = true;
        Debug.Log(emp.Name + " was Fired!");
    }
    void HandleOnEmployeeExited(Employee emp)
    {
        Debug.Log(emp.Name + " quit!");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
