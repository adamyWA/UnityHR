using System.Collections;
using Utility;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Employee : MonoBehaviour {

    public bool Employed;
    public bool HasOpenShifts;
    public string Name { get; set; }
    public int Id { get; set; }
    public Title Title { get; set; }
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
        OpenShifts = new List<OpenShift> { new OpenShift { Day = DayOfWeek.Monday, Shift = Shift.Morning } };
        WorkShifts = new List<OpenShift>();
        Name = "Default Employee";
        Id = 1337;
        Title = Title.Janitor;
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
    // Update is called once per frame
    void Update()
    {

    }
}
