using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StaffData
{
    public bool HasEmployees { get; set; }
    public bool Full;
    public int Max { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }
    public List<EmployeeData> Employees;
}