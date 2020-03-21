using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffListener : MonoBehaviour {

    public Staff Staff;
	// Use this for initialization
	void Start () {
        Staff = GetComponent<Staff>();
        Staff.Hired += HandleOnHired;
	}
	void HandleOnHired(Employee emp)
    {
        emp.Employed = true;
        Debug.Log(emp.Name + " was Hired!");
    }
    void HandleOnFired(Employee emp)
    {
        Debug.Log(emp.Name + " was Fired!");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
