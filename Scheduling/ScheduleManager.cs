using Assets.Scripts.Staffing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Scheduling
{
    public class ScheduleManager : MonoBehaviour
    {
        public Schedule Schedule;
        public Employee Employee;
        public void Start()
        {
            Schedule = gameObject.GetComponent<Schedule>();
            Schedule.EmployeeWasAddedToScheduleRoster += HandleEmployeeWasAddedToScheduleRoster;
        }
        void HandleEmployeeWasAddedToScheduleRoster(Employee employee)
        {
            employee.AddToSchedule(Schedule);
            Debug.Log("Employee on Schedule Roster: " + employee.Parameters.Name);
        }
    }
}
