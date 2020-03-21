using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffListener : MonoBehaviour {

    public Staff Staff;
	// Use this for initialization
	void Start () {
        Staff = GetComponent<Staff>();
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
        foreach(var shift in emp.WorkShifts)
        {
            sched.OpenShift(shift);
            if (!emp.OpenShifts.Contains(shift))
                emp.OpenShifts.Add(shift);
        }
        foreach(var openShift in emp.OpenShifts)
        {
            if (emp.OpenShifts.Contains(openShift))
                emp.WorkShifts.Remove(openShift);

        }
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
