using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utility
{
    public enum Shift
    {
        Morning,
        Evening,
        Night
    }
    public enum Title
    {
        Manager,
        Marketer,
        Security,
        Janitor
    }
    public enum Gender
    {
        Male=0,
        Female=1
    }
    public class EnumUtility
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
