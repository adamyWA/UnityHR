using System.Collections;
using Utility;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Employee : MonoBehaviour
{

    public bool Employed { get; set; }
    public bool HasOpenShifts { get; set; }
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
    public event Action<OpenShift> RemovedFromShift;
    public string JobDescription;

    // Use this for initialization
    void Start()
    {
        #region Initialize
        MaxShifts = 5;
        HasOpenShifts = true;
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
    public void RemoveFromShift(OpenShift shift)
    {
        if (RemovedFromShift != null)
        {
            if (WorkShifts.Contains(shift))
            {
                WorkShifts.Remove(shift);
                if (!OpenShifts.Contains(shift))
                    OpenShifts.Add(shift);
                RemovedFromShift(shift);
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
    //create a random employee of the passed title
    public void Randomize(Title title, System.Random random, bool randomizeShifts = true)
    {
        Title = title;
        Id = Guid.NewGuid();
        if (randomizeShifts)
            RandomizeShifts(random);
        RandomizeGender(random);
        
        RandomizeName(random, Gender);
    }
    //create a random employee of the title and gender passed
    public void Randomize(Title title, Gender gender, System.Random random, bool randomizeShifts = true)
    {
        if (randomizeShifts)
            RandomizeShifts(random);
        Title = title;
        Id = Guid.NewGuid();
        Gender = gender;
        RandomizeName(random, Gender);
    }
    public void Randomize(Title title, Gender gender, bool randomizeShifts = true)
    {
        System.Random random = new System.Random();
        if (randomizeShifts)
            RandomizeShifts(random);
        Title = title;
        Id = Guid.NewGuid();
        Gender = gender;
        RandomizeName(random, Gender);
    }
    public void RandomizeName(System.Random random, Gender gender)
    {
        var randomName = new RandomName(Gender, random);
        Name = randomName.Name;
    }
    public void RandomizeGender(System.Random random)
    {
        Gender = (Gender)random.Next(0, 2);
    }
    public void RandomizeTitle(System.Random random)
    {
        Title = (Title)random.Next(0, 3);
    }
    public void RandomizeShifts(System.Random random)
    {
        int numberOfOpenShifts = random.Next(5, 10); //create a random number between 5 and 20. this decides how many openshifts to give the employee
        OpenShifts = new List<OpenShift>();

        for (var i = 0; numberOfOpenShifts >= OpenShifts.Count;)
        {
            var randomShift = new OpenShift { Day = (DayOfWeek)random.Next(0, 7), Shift = (Shift)random.Next(0, 3), RequiredTitle = Title };
            if (!OpenShifts.Contains(randomShift))
            {
                OpenShifts.Add(randomShift);
                ++i;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
