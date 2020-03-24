using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utility
{
    public class Notification
    {
        public bool Success = false;
        public string Message = string.Empty;
    }
    public class OpenShift
    {
        public DayOfWeek Day { get; set; }
        public Shift Shift {get;set;}
        public Title RequiredTitle { get; set; }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                OpenShift x = (OpenShift)obj;
                return (Day == x.Day) && (Shift == x.Shift) && (RequiredTitle == x.RequiredTitle);
            }
        }
    }
    public class RandomName
    {
        public string Name { get; private set; }
        private System.Random _random;
        public RandomName(Gender gender, System.Random random)
        {
            _random = random;
            if(gender == Gender.Male)
            {
                Name = MaleFirstNames[_random.Next(0, MaleFirstNames.Count)] + " " + LastNames[_random.Next(0, LastNames.Count)];
            } else if(gender == Gender.Female)
            {
                Name = FemaleFirstNames[_random.Next(0, MaleFirstNames.Count)] + " " + LastNames[_random.Next(0, LastNames.Count)];
            }
        }
        private List<string> MaleFirstNames = new List<string>()
        {
            "Mark",
            "Frank",
            "Tom",
            "Hank"
        };
        private List<string> LastNames = new List<string>()
        {
            "Hoppinburger",
            "Schlomalonga",
            "BingBong",
            "Jebere",
            "Rose",
            "Finning",
            "Astronautica",
            "Jebere"
        };
        private List<string> FemaleFirstNames = new List<string>()
        {
            "Louise",
            "Tina",
            "Ava",
            "Jennifer",
            "Chloe",
            "Marissa",
            "Blinda",
            "Sally",
            "Sky",
            "Winnie",
            "Gretchen"
        };
    }
}
