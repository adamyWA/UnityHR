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
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                OpenShift x = (OpenShift)obj;
                return (Day == x.Day) && (Shift == x.Shift);
            }
        }
    }
}
