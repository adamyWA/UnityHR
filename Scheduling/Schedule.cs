using Assets.Scripts.Staffing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Scheduling
{
    public class Schedule : MonoBehaviour
    {
        public Dictionary<DayOfWeek, List<Shifts>> OpenShifts { get; set; }
        public bool ScheduleIsCovered { get; set; }
        public List<Employee> RosterOfEmployees { get; set; }
        private void Start()
        {
            if (RosterOfEmployees == null)
                RosterOfEmployees = new List<Employee>();
            OpenShifts = new Dictionary<DayOfWeek, List<Shifts>>
            {
                { DayOfWeek.Monday, new List<Shifts> { Shifts.Morning, Shifts.Evening, Shifts.Night} },
                { DayOfWeek.Tuesday, new List<Shifts> { Shifts.Morning, Shifts.Evening, Shifts.Night} },
                { DayOfWeek.Wednesday, new List<Shifts> { Shifts.Morning, Shifts.Evening, Shifts.Night} },
                { DayOfWeek.Thursday, new List<Shifts> { Shifts.Morning, Shifts.Evening, Shifts.Night} },
                { DayOfWeek.Friday, new List<Shifts> { Shifts.Morning, Shifts.Evening, Shifts.Night} },
                { DayOfWeek.Saturday, new List<Shifts> { Shifts.Morning, Shifts.Evening, Shifts.Night} },
                { DayOfWeek.Sunday, new List<Shifts> { Shifts.Morning, Shifts.Evening, Shifts.Night} }
            };
        }
        /*
        public Schedule(List<DayOfWeek> daysOpen, List<Shifts> shiftsScheduledOpen) //allow caller to specify the days and hours of business
        {
            OpenShifts = new Dictionary<DayOfWeek, List<Shifts>>();
            foreach(DayOfWeek dayOpen in daysOpen)
            {
                if(!OpenShifts.ContainsKey(dayOpen))
                    OpenShifts.Add(dayOpen, shiftsScheduledOpen);
            }
        }
        */
        public event Action<DayOfWeek, Shifts, Employee> ShiftWasCovered;
        public event Action<DayOfWeek, Shifts, Employee> ShiftWasOpened;
        public event Action<Employee> EmployeeWasAddedToScheduleRoster;
        public event Action<Employee> EmployeeWasRemovedFromScheduleRoster;
        public event Action ScheduleWasFullyCovered;
        public event Action ScheduleGainedOpenings;

        public void CoverShift(DayOfWeek day, Shifts shift, Employee employee)
        {
            if(ShiftWasCovered != null)
            {
                if(OpenShifts.ContainsKey(day)) //key errors otherwise
                {
                    if (OpenShifts[day].Contains(shift)) 
                    {
                        if (OpenShifts[day].Remove(shift))
                        {
                            int openDayCounter = 0;
                            foreach(var item in OpenShifts.Keys)
                            {
                                //determine if any shifts are open, and call ScheduleWasFullyCovered if not
                                openDayCounter += OpenShifts[item].Count;
                            }
                            if (openDayCounter == 0)
                            {
                                if (ScheduleWasFullyCovered != null)
                                {
                                    ScheduleIsCovered = true;
                                    ScheduleWasFullyCovered(); //trigger ScheduleWasFullyCoveredEvent if no shifts remain on any day
                                }
                            }
                            ShiftWasCovered(day, shift, employee);
                        }
                    }
                }
            }
        }
        public void OpenShift(DayOfWeek day, Shifts shift, Employee employee)
        {
            if (ShiftWasOpened != null)
            {
                if (OpenShifts.ContainsKey(day))
                {
                    if(!OpenShifts[day].Contains(shift))
                    {
                        OpenShifts[day].Add(shift);
                        ShiftWasOpened(day,shift,employee);
                        if (ScheduleGainedOpenings != null)
                        {
                            ScheduleIsCovered = false;
                            ScheduleGainedOpenings();
                        }
                    }
                }
            }
        }
        public void AddToScheduleRoster(Employee employee)
        {
            if (EmployeeWasAddedToScheduleRoster != null)
            {
                if (!RosterOfEmployees.Contains(employee))
                {
                    RosterOfEmployees.Add(employee);
                    EmployeeWasAddedToScheduleRoster(employee);
                }
            }
        }
        public void RemoveFromScheduleRoster(Employee employee)
        {
            if (EmployeeWasRemovedFromScheduleRoster != null)
            {
                if (!RosterOfEmployees.Contains(employee))
                {
                    RosterOfEmployees.Add(employee);
                    EmployeeWasRemovedFromScheduleRoster(employee);
                }
            }
        }

    }
}
