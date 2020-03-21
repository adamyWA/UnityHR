using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
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
    public interface IPublisher
    {
        string Message { get; set; }
        bool Success { get; set; }
        void NotifySubscribers();   
    }
}
