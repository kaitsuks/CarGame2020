using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitByEscape : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            //Application.Quit();
            GameObject go1 = transform.Find("ButtonYes").gameObject;
            GameObject go2 = transform.Find("ButtonCancel").gameObject;
            GameObject go4 = transform.Find("ButtonStartOver").gameObject;
            go1.SetActive(true);
            go2.SetActive(true);
            go4.SetActive(true);
        }
    }
}
