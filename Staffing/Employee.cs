using Assets.Scripts.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Staffing
{
    public enum EmploymentStatus
    {
        Hired,
        Fired,
        Unemployed
    }
    public enum Shifts
    {
        Morning,
        Evening,
        Night
    }
    public enum JobTitle
    {
        MedicalTech,
        Nurse,
        Caregiver
    }
    public class EmployeeParameters
    {
        public string Name { get; set; }
        public List<Shifts> ShiftAvailability { get; set; }
        public JobTitle JobTitle { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public bool OnSchedule { get; set; }
        public Dictionary<DayOfWeek, Shifts> CoveredShifts { get; set; }

    }
    public class Employee : MonoBehaviour
    {
        public Schedule Schedule;
        public EmployeeParameters Parameters;
        #region constructors
        private void Start()
        {
            if(Parameters == null)
                Parameters = new EmployeeParameters();
        }

        #endregion

        #region Action definitions
        public event Action<Schedule> AddedToSchedule;
        public event Action<Schedule> RemovedFromSchedule;
        public event Action WasFired;
        public event Action WasHired;
        public event Action<DayOfWeek, Shifts> CoveredShift;
        public event Action<DayOfWeek, Shifts> AbandonedShift;
        #endregion
        #region Added to and removed from schedule events
        public void AddToSchedule(Schedule schedule)
        {
            if (AddedToSchedule != null)
            {
                if (schedule.RosterOfEmployees.Contains(this))
                {
                    if (Parameters.OnSchedule == false)
                    {
                        Parameters.OnSchedule = true;
                        AddedToSchedule(schedule);
                    }
                }
            }    
        }
        public void RemoveFromSchedule(Schedule schedule)
        {

            if (RemovedFromSchedule != null)
            {
                if (schedule.RosterOfEmployees.Contains(this))
                {
                    if (Parameters.OnSchedule)
                    {
                        Parameters.OnSchedule = false;
                        RemovedFromSchedule(schedule);
                    }
                }
            }
        }
        #endregion

        #region Hired and fired events
        public void Hire()
        {
            if(WasHired != null)
            {
                if (Parameters.EmploymentStatus != EmploymentStatus.Hired)
                {
                    Parameters.EmploymentStatus = EmploymentStatus.Hired;
                    WasHired();
                }
            }
        }
        public void Fire()
        {
            if(WasFired != null)
            {
                if (Parameters.EmploymentStatus == EmploymentStatus.Hired)
                {
                    Parameters.EmploymentStatus = EmploymentStatus.Fired;
                    WasFired();
                }
            }
        }
        #endregion

        #region Cover and abandon shift events
        public void CoverShift(DayOfWeek day, Shifts shift)
        {
            if (CoveredShift != null)
            {
                if (!Parameters.CoveredShifts.ContainsKey(day))
                {
                    if (!Parameters.CoveredShifts[day].Equals(shift))
                    {
                        Parameters.CoveredShifts.Add(day, shift);
                        CoveredShift(day, shift);
                    }
                }
            }
        }
        public void AbandonShift(DayOfWeek day, Shifts shift)
        {
            if (AbandonedShift != null)
            {
                if (Parameters.CoveredShifts.ContainsKey(day))
                {
                    if (Parameters.CoveredShifts[day].Equals(shift))
                    {
                        Parameters.CoveredShifts.Remove(day);
                        AbandonedShift(day, shift);
                    }
                }
            }
        }
        #endregion
    }
}
