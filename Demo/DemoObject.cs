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
		if(Input.GetKeyDown("1")) //hire an employee
        {
            Staff.Hire(Employee);
        }
        if(Input.GetKeyDown("2")) //have the employee cover a shift
        {
            Schedule.CoverShift(Employee, new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Morning });
        }
        if (Input.GetKeyDown("3")) //fire the employee
        {
            Staff.Fire(Employee, Schedule); //need to pass the schedule so the employee's shifts can be removed from it
        }
        if(Input.GetKeyDown("4")) //have the employee quit
        {
            Employee.Quit(Schedule, Staff);
        }
        if(Input.GetKeyDown("5")) //give us some info about schedule, the employee, the staff
        {
            Debug.Log("Schedule currently has " + Schedule.OpenShifts.Count + " openings.");
            foreach(var shift in Schedule.OpenShifts)
            {
                Debug.Log("Shift: " + shift.Day + shift.Shift + " is open on schedule");
            }
            Debug.Log("Employee is Employed: " + Employee.Employed);
            foreach (var shift in Employee.WorkShifts)
            {
                Debug.Log("Employee: " + Employee.Name + " is covering " + shift.Day + " " + shift.Shift);
            }
            Debug.Log("Staff has " + Staff.Employees.Count + " employees currently.");
            foreach(var emp in Staff.Employees)
            {
                Debug.Log(Employee.Name + " is on staff.");
            }
        }
	}
}
