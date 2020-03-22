using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utility
{
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
    public static class RandomName
    {
        private static List<string> MaleFirstNames = new List<string>()
        {
            "Mark",
            "Frank",
            "Tom",
            "Hank"
        };
        private static List<string> MaleLastNames = new List<string>()
        {
            "Hoppinburger",
            "Schlomalonga",
            "BingBong",
            "Jebere"
        };
        private static List<string> FemaleFirstNames = new List<string>()
        {
            "Louise",
            "Tina",
            "Ava",
            "Jennifer"
        };
        private static List<string> FemaleLastNames = new List<string>()
        {
            "Rose",
            "Finning",
            "Astronautica",
            "Jebere"
        };
        public static string MaleName {
            get {
                string name = string.Empty;
                var random = new System.Random();
                name += MaleFirstNames[random.Next(0, MaleFirstNames.Count-1)];
                name += " " + MaleLastNames[random.Next(0, MaleLastNames.Count - 1)];
                return name;
            }
        }
        public static string FemaleName
        {
            get
            {
                string name = string.Empty;
                var random = new System.Random();
                name += FemaleFirstNames[random.Next(0, FemaleFirstNames.Count - 1)];
                name += " " + FemaleLastNames[random.Next(0, FemaleLastNames.Count - 1)];
                return name;
            }
        }

    }
}
