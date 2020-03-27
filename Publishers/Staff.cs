using System;
using System.Collections;
using System.Linq;
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
            var notDuplicateEmployee = !Employees.Exists(x => x.Id == emp.Id);
            if (notDuplicateEmployee && Employees.Count < Max)
            {
                Employees.Add(emp);
                if(Employees.Count >= Max)
                    Full = true;
                HasEmployees = true;
                Hired(emp);
            }
        }
    }
    public void Fire(Employee emp, Schedule sched)
    {
        if (Fired != null)
        {   
            if(Employees.Any(x=>x.Title == emp.Title))
            {
                Employees.Remove(Employees.Where(x => x.Title == emp.Title).Where(y=>y.Gender == emp.Gender).FirstOrDefault());
                if (Employees.Count <= Max)
                    Full = false;
                if (Employees.Count == 0)
                    HasEmployees = false;
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
