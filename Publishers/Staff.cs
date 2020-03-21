using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour {

    public bool HasEmployees;
    public bool Full;
    public int Max { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }
    public List<Employee> Employees;

    public event Action<Employee> Hired;
    public event Action<Employee, Schedule> Fired;
    public event Action<Employee> EmployeeExited;

	// Use this for initialization
	void Start () {
        #region Initialize
        HasEmployees = false;
        Full = false;
        Max = 10;
        Name = "Default Staff";
        Id = 1337;
        Employees = new List<Employee>();
        #endregion
    }

    public void Hire(Employee emp)
    {
        if (Hired != null)
        {
            if (!Employees.Contains(emp) && Employees.Count < Max)
            {
                Employees.Add(emp);
                if(Employees.Count >= Max)
                    Full = true;

                Hired(emp);
            }
        }
    }
    public void Fire(Employee emp, Schedule sched)
    {
        if (Fired != null)
        {   
            if(Employees.Contains(emp))
            {
                Employees.Remove(emp);
                if (Employees.Count <= Max)
                    Full = false;

                Fired(emp, sched);
            }
            
        }
    }
	public void EmployeeExit(Employee emp)
    {
        if (EmployeeExited != null)
        {
            if (Employees.Contains(emp))
            {
                Employees.Remove(emp);
                if (Employees.Count <= Max)
                    Full = false;

                EmployeeExited(emp);
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
