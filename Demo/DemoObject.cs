using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using System;
public class DemoObject : MonoBehaviour {

    public Staff Staff;
    public Employee Employee;
    public Schedule Schedule;
    public EmployeeListener EmployeeListener;
    public ScheduleListener ScheduleListener;
    public StaffListener StaffListener;
    public GameObject EmployeeManifestation;
	// Use this for initialization
	void Start () {
        Staff = gameObject.AddComponent<Staff>();
        StaffListener = gameObject.AddComponent<StaffListener>();
        StaffListener.Staff = Staff;
        Schedule = gameObject.AddComponent<Schedule>();
        ScheduleListener = gameObject.AddComponent<ScheduleListener>();
        ScheduleListener.Schedule = Schedule;
        CreateAnEmployee();
        Employee = EmployeeManifestation.GetComponent<Employee>();
        EmployeeListener = EmployeeManifestation.GetComponent<EmployeeListener>();
        EmployeeListener.Employee = Employee;
	}
	void CreateAnEmployee()
    {
        EmployeeManifestation = GameObject.CreatePrimitive(PrimitiveType.Cube);
        EmployeeManifestation.AddComponent<Employee>();
        EmployeeManifestation.AddComponent<EmployeeListener>();
    }
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("1"))
        {
            Staff.Hire(Employee);
        }
        if(Input.GetKeyDown("2"))
        {
            Schedule.CoverShift(Employee, new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Morning });
        }
        if(Input.GetKeyDown("3"))
        {
            Employee.Quit(Schedule, Staff);
        }
	}
}
