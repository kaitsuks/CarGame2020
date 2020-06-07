using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Playground/Attributes/Collection")]
public class Collection : MonoBehaviour {

    [HideInInspector]
    public int highNumber;
    

    // Use this for initialization
    void Start () {
        Debug.Log("collection is alive");
        highNumber = 1;
        Debug.Log("collection highNumber in Start = " + highNumber);
    }

    public bool AddNumber()
    {
        highNumber++;
        Debug.Log("collection highNumber now = "+ highNumber);
        return true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
