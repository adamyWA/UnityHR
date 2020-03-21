using Assets.Scripts.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Staffing
{
    public class EmployeeManager : MonoBehaviour
    {
        public Employee Employee;
        public Schedule Schedule;
        private void Start()
        {
            Employee.AddedToSchedule += HandleOnAddedToSchedule;
            Employee.RemovedFromSchedule += HandleOnRemovedFromSchedule;
            Employee.WasFired += HandleOnWasFired;
            Employee.WasHired += HandleOnWasHired;
            Employee.CoveredShift += HandleOnCoveredShift;
            Employee.AbandonedShift += HandleOnAbandonedShift;

        }

        public void HandleOnAddedToSchedule(Schedule schedule)
        {
            //do something when addtoschedule is called
            //perhaps add an employee to a schedule gameobject's roster?
            schedule.AddToScheduleRoster(Employee);
            Debug.Log("Employee: " + Employee.Parameters.Name + " was ADDED. Roster count is " + schedule.RosterOfEmployees.Count);
        }
        public void HandleOnRemovedFromSchedule(Schedule schedule)
        {
            schedule.RemoveFromScheduleRoster(Employee); 
            Debug.Log("Employee: " + Employee.Parameters.Name + "was REMOVED. Roster count is " + schedule.RosterOfEmployees.Count);
        }
        public void HandleOnWasFired()
        {
            Debug.Log(Employee.Parameters.Name + " was Fired!");
        }
        public void HandleOnWasHired()
        {
            Debug.Log(Employee.Parameters.Name + " was Hired!");
        }
        public void HandleOnCoveredShift(DayOfWeek day, Shifts shift)
        {
            Debug.Log("Employee " + Employee.Parameters.Name + " covered " + shift + " on " + day);
        }
        public void HandleOnAbandonedShift(DayOfWeek day, Shifts shift)
        {

        }
        private void Destroy()
        {
            Employee.AddedToSchedule -= HandleOnAddedToSchedule;
            Employee.RemovedFromSchedule -= HandleOnRemovedFromSchedule;
            Employee.WasFired -= HandleOnWasFired;
            Employee.WasHired -= HandleOnWasHired;
            Employee.CoveredShift -= HandleOnCoveredShift;
            Employee.AbandonedShift -= HandleOnAbandonedShift;
        }
    }
}
