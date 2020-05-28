using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Playground/Attributes/Number Collection")]
public class NumberCollection : MonoBehaviour {

    [HideInInspector]
    public int highNumber;
    

    // Use this for initialization
    void Start () {
        Debug.Log("numbercollection is alive");
        highNumber = 1;
        Debug.Log("numbercollection highNumber in Start = " + highNumber);
    }

    public bool AddNumber()
    {
        highNumber++;
        Debug.Log("numbercollection highNumber now = "+ highNumber);
        return true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
