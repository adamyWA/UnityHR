using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using System;
using Assets.Scripts.Utility;
using UnityEngine.UI;

public class DemoObject : MonoBehaviour {

    public Staff Staff;
    public Employee Employee;
    public Schedule Schedule;
    public EmployeeListener EmployeeListener;
    public ScheduleListener ScheduleListener;
    public StaffListener StaffListener;
    public GameObject EmployeeManifestation;
    public Text StaffMessage;

    // This Object should be attached to a GameObject in a Unity scene. Unity will call the Start() method when the object is loaded into the Scene (on game start) 
    void Start() {

        Staff = gameObject.AddComponent<Staff>(); //Add a new Staff object to the scene. Staff is "initialized" with the values in Staff.cs "Start()" method.
        StaffListener = gameObject.AddComponent<StaffListener>(); //Add a new StaffListener object to the scene. StaffListener is "initialized" with the values in StaffListener.cs "Start()" method.
        StaffListener.Staff = Staff; //Add a reference to the created Staff object to the StaffListener, so it can act on the same object when events are triggered.
        StaffMessage = GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        Schedule = gameObject.AddComponent<Schedule>(); //Add a Schedule object. Schedule is "initialized" with the values in Schedule.cs "Start()" method.
        ScheduleListener = gameObject.AddComponent<ScheduleListener>(); //Add a new ScheduleListener object to the scene. ScheduleListener is "initialized" with the values in ScheduleListener.cs "Start()" method.
        ScheduleListener.Schedule = Schedule; //Add a reference to the created Schedule object to the ScheduleListener, so it can act on the same object when events are triggered.
        CreateAnEmployee(); //Employee creation is a bit different, since multiple employees may exist in a scene. Calls the CreateAnEmployee() method, below.
    }
	void CreateAnEmployee()
    {
        EmployeeManifestation = GameObject.CreatePrimitive(PrimitiveType.Cube); //adds a Cube object to the scene, which will act as an anchor for an Employee, and the EmployeeListener that will reference it
        EmployeeListener = EmployeeManifestation.AddComponent<EmployeeListener>(); //Add an EmployeeListener object to the cube we created. EmployeeListener is "initialized" with the values in EmployeeListener.cs "Start()" method.
        Employee = EmployeeManifestation.AddComponent<Employee>(); //Add an Employee object to the cube we created. Employee is "initialized" with the values in Employee.cs "Start()" method.
        Employee.Randomize(Title.Janitor, new System.Random()); //Randomize the Employee, using Randomize(Title) method in Employee.cs
        EmployeeListener.Employee = Employee; //Add a reference to the Randomized Employee to the EmployeeListener
    }
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("1")) //hire an employee
        {
            //Invoke Staff.Hire, passing the Employee Object we created at Start.
            //This will trigger:
            //StaffListener.HandleOnHired(Employee emp)
            //Staff.Hire adds the passed Employee to the Staff.Employees list, if the Employee doesn't exist in the list already
            Staff.Hire(Employee); 
        }
        if(Input.GetKeyDown("2")) //have the employee cover all open shifts that they are eligible to work
        {
            //Initialize a new List of OpenShifts. OpenShift type is defined in Utility.Dataclasses.cs, as a simple object with 3 fields:
            //Title - Title enum defined in Utility.Enums.cs:
            //Day - built-in DayOfWeek enum 
            //Shift - Shift enum defined in Utility.Enums.cs
            //shiftsEmployeeCanCover will act as a copy of the OpenShifts List that belongs to the Employee we created previously.
            //A copy of the List is necessary, since we will be modifying the List while iterating over it. In C#, you cannot modify a list mid-iteration.
            var shiftsEmployeeCanCover = new List<OpenShift>(); 
            foreach(var shift in Employee.OpenShifts)
            {
                //add each existing OpenShift that the Employee can work to the copy
                shiftsEmployeeCanCover.Add(shift);
            }
            if (Employee.Employed) //do nothing if the Employee's Employed property is False. The employee can't take shifts if not Employed.
            {
                foreach (var shift in shiftsEmployeeCanCover) //iterate over the new List
                {
                    //Invoke Schedule.CoverShift, passing our Employee object, and a new Shift, created on the fly from the OpenShift info we copied from the Employee's OpenShifts
                    //This will trigger:
                    //ScheduleListener.HandleOnShiftCovered(Employee emp, OpenShift shift)
                    //Employee.Cover(OpenShift shift), which triggers:
                    //EmployeeListener.HandleOnCovered(Openshift shift)
                    Schedule.CoverShift(Employee, new OpenShift { Day = shift.Day, Shift = shift.Shift, RequiredTitle = shift.RequiredTitle });
                }
            }
        }
        if (Input.GetKeyDown("3")) //fire the employee
        {
            //Invoke Staff.Fire, passing our Employee object, and our Schedule object
            //This will trigger:
            //StaffListener.HandleOnFired(Employee emp, Schedule sched)
            //Schedule.OpenShift(OpenShift shift) --to remove any shifts that the employee is scheduled to work. This will trigger:
            //ScheduleListener.HandleOnShiftOpened(OpenShift shift)
            Staff.Fire(Employee, Schedule); //need to pass the schedule so the employee's shifts can be removed from it
        }
        if(Input.GetKeyDown("4")) //have the employee quit
        {
            //Invoke Employee.Quit, passing our Schedule object and our Staff object
            //This will trigger:
            //Staff.EmployeeExit(Employee employee) which triggers:
            //Schedule.OpenShift(OpenShift shift). This will trigger:
            //ScheduleListener.HandleOnShiftOpened(OpenShift shift)
            //StaffListener.HandleOnEmployeeExited(Employee emp)
            Employee.Quit(Schedule, Staff);
        }
        if(Input.GetKeyDown("5")) //give us some info about schedule, the employee, the staff
        {
            Debug.Log("Schedule currently has " + Schedule.OpenShifts.Count + " openings.");
            foreach(var shift in Schedule.OpenShifts)
            {
                Debug.Log(shift.RequiredTitle +  " shift " + shift.Day + shift.Shift + " is open on schedule");
            }
            Debug.Log("Employee is Employed: " + Employee.Employed);
            foreach (var shift in Employee.WorkShifts)
            {
                Debug.Log("Employee: " + Employee.Name + " is covering " + shift.Day + " " + shift.Shift + "as " + shift.RequiredTitle);
            }
            Debug.Log("Staff has " + Staff.Employees.Count + " employees currently.");
            foreach(var emp in Staff.Employees)
            {
                Debug.Log(Employee.Name + " is on staff.");
            }
        }
	}
}
