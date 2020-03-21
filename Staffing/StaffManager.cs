using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Staffing
{
    public class StaffManager : MonoBehaviour
    {
        public Staff Staff;
        private void Start()
        {
            Staff = gameObject.GetComponent<Staff>();
            Staff.EmployeeAdded += HandleOnEmployeeAdded;
            Staff.EmployeeRemoved += HandleOnEmployeeRemoved;
        }
        private void Destroy()
        {
            Staff.EmployeeAdded -= HandleOnEmployeeAdded;
            Staff.EmployeeRemoved -= HandleOnEmployeeRemoved;
        }
        public static void HandleOnEmployeeAdded(Employee employee)
        {
            Debug.Log(employee.Parameters.Name + " added to staff");
        }
        public static void HandleOnEmployeeRemoved(Employee employee)
        {
            employee.Fire();
            Debug.Log(employee.Parameters.Name + " removed from staff");
        }
    }
}
