using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour {

	// Use this for initialization
	void Start () {
       // CanvasGroup yourNameHere = GetComponent<CanvasGroup>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void QuitGame()
    {
        Debug.Log("Quitting!");
        Application.Quit();
    }

    public void Cancel()
    {
        GameObject go1 = transform.Find("ButtonYes").gameObject;
        GameObject go2 = transform.Find("ButtonCancel").gameObject;
        GameObject go3 = transform.Find("ButtonQuit").gameObject;
        GameObject go4 = transform.Find("ButtonStartOver").gameObject;
        go1.SetActive(false);
        go2.SetActive(false);
        go4.SetActive(false);
        go3.SetActive(true);
        //go3.GetComponent<CanvasRenderer>().SetAlpha(1f);
        //gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
    }
}
