﻿using System.Collections;
using Utility;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Employee : MonoBehaviour {

    public bool Employed;
    public bool HasOpenShifts;
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public Guid Id { get; set; }
    public Title Title { get; set; }
    public int MaxShifts { get; set; }
    public List<OpenShift> OpenShifts { get; set; }
    public List<OpenShift> WorkShifts { get; set; }

    public event Action<Schedule, OpenShift> CalledOut;
    public event Action<Schedule, Staff> Quitted; //lol
    public event Action<OpenShift> Covered;

    // Use this for initialization
    void Start()
    {
        #region Initialize
        Employed = false;
        HasOpenShifts = true;
        MaxShifts = 5;
        WorkShifts = new List<OpenShift>();
        #endregion
    }
    public void Cover(OpenShift shift)
    {
        if(Covered != null)
        {
            if(OpenShifts.Contains(shift))
            {
                OpenShifts.Remove(shift);
                if (!WorkShifts.Contains(shift))
                    WorkShifts.Add(shift);
                Covered(shift);
            }
        }
    }
    void CallOut(Schedule sched, OpenShift shift)
    {
        if (CalledOut != null)
            CalledOut(sched,shift);
    }

    public void Quit(Schedule sched, Staff staff)
    {
        if(Quitted !=null)
        {
            Quitted(sched, staff);
        }
    }
    public void Randomize(Title title)
    {
        var rand = new System.Random();
        int numberOfOpenShifts = rand.Next(5, 10); //create a random number between 5 and 20. this decides how many openshifts to give the employee
        Title = title;
        Id = Guid.NewGuid();
        Gender = (Gender)rand.Next(0, 2);
        OpenShifts = new List<OpenShift>();

        for(var i=0;numberOfOpenShifts>=OpenShifts.Count;)
        {
            var randomShift = new OpenShift { Day = (DayOfWeek)rand.Next(0, 8), Shift = (Shift)rand.Next(0, 3), RequiredTitle = Title };
            if (!OpenShifts.Contains(randomShift))
            {
                OpenShifts.Add(randomShift);
                ++i;
            }
        }

        
        if (Gender == Gender.Male)
            Name = RandomName.MaleName;
        if (Gender == Gender.Female)
            Name = RandomName.FemaleName;
    }

// Update is called once per frame
    void Update()
    {

    }
}
