using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Staffing
{
    public class Staff : MonoBehaviour
    {
        private List<Employee> _employees;
        public List<Employee> Employees { get { return _employees; } }
        public int Count { get { return _employees.Count; } }
        public void Start()
        {
            _employees = new List<Employee>();
        }
        public event Action<Employee> EmployeeAdded;
        public event Action<Employee> EmployeeRemoved;



        public void AddEmployee(Employee employee)
        {
            if (!_employees.Contains(employee))
            {
                _employees.Add(employee);
                if (EmployeeAdded != null)
                    EmployeeAdded(employee);
            }
        }
        public void RemoveEmployee(Employee employee)
        {
            if (_employees.Contains(employee))
            {
                _employees.Remove(employee);
                if (EmployeeRemoved != null)
                    EmployeeRemoved(employee);
            }
        }  
    }

}
