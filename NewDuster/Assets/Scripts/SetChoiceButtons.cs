using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetChoiceButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject go1 = transform.Find("ButtonYes").gameObject;
        GameObject go2 = transform.Find("ButtonCancel").gameObject;
        GameObject go3 = transform.Find("ButtonQuit").gameObject;
        GameObject go4 = transform.Find("ButtonStartOver").gameObject;

        go1.SetActive(false);
        go2.SetActive(false);

        go4.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetButtons()
    {
       GameObject go1 =  transform.Find("ButtonYes").gameObject;
       GameObject go2 = transform.Find("ButtonCancel").gameObject;
       GameObject go3 = transform.Find("ButtonQuit").gameObject;
       GameObject go4 = transform.Find("ButtonStartOver").gameObject;
        go1.SetActive(true);
        go2.SetActive(true);
        go4.SetActive(true);
        go3.SetActive(false);
        //go3.GetComponent<CanvasRenderer>().SetAlpha(0f);
        //gameObject.GetComponent<Button>().GetComponent<CanvasRenderer>().SetAlpha(0f);
        //GameObject.Find("ButtonQuit").SetActive(false);
    }
}
