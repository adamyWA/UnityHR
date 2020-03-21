using Assets.Scripts.Staffing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Scheduling
{
    public class HRManager : MonoBehaviour
    {
        public List<Dictionary<Employee, EmployeeManager>> EmployeeManagerPairs; 
        public ScheduleManager ScheduleManager;
        public StaffManager StaffManager;
        public Staff Staff;
        public Schedule Schedule;
        private int TotalEmployeeCount;
        private void Start()
        {
            TotalEmployeeCount = 0;
            Staff = gameObject.AddComponent<Staff>();
            EmployeeManagerPairs = new List<Dictionary<Employee, EmployeeManager>>();
            ScheduleManager = gameObject.AddComponent<ScheduleManager>();
            StaffManager = gameObject.AddComponent<StaffManager>();
            Schedule = gameObject.AddComponent<Schedule>();
        }
        private void Update()
        {
            if(Input.GetKeyDown("1")) //create Employee
            {
                EmployeeParameters empParams = new EmployeeParameters
                {
                    Name = EmployeeManagerPairs.Count.ToString(),
                    ShiftAvailability = new List<Shifts> { Shifts.Morning },
                    EmploymentStatus = EmploymentStatus.Unemployed,
                    JobTitle = JobTitle.Caregiver,
                    OnSchedule = false
                };
                var localDict = new Dictionary<Employee, EmployeeManager>();
                var localEmployee = gameObject.AddComponent<Employee>();
                var localManager = gameObject.AddComponent<EmployeeManager>();
                localManager.Employee = localEmployee;
                localDict.Add(localEmployee, localManager);
                EmployeeManagerPairs.Add(localDict);
                //set this employee's parameters in place
                EmployeeManagerPairs[EmployeeManagerPairs.Count-1].Keys.Where(x => x == localEmployee).FirstOrDefault().Parameters = empParams;
            }
            if (Input.GetKeyDown("2")) //add employee to staff
            {
                Staff.AddEmployee(EmployeeManagerPairs[EmployeeManagerPairs.Count-1].Keys.Where(x => x.Parameters.Name == Convert.ToString(EmployeeManagerPairs.Count-1)).FirstOrDefault());
                EmployeeManagerPairs[EmployeeManagerPairs.Count-1].Keys.Where(x => x.Parameters.Name == Convert.ToString(EmployeeManagerPairs.Count-1)).FirstOrDefault().Hire();
            }
            if(Input.GetKeyDown("3")) //add employee to schedule roster
            {
                Schedule.AddToScheduleRoster(EmployeeManagerPairs[EmployeeManagerPairs.Count-1].Keys.Where(x => x.Parameters.Name == Convert.ToString(EmployeeManagerPairs.Count-1)).FirstOrDefault());
            }
            
        }
    }
}
