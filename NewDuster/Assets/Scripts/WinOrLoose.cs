using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLoose : MonoBehaviour {

    GameObject go;
    UIScript ui;
    // Use this for initialization
    void Start () {

        go = GameObject.Find("UserInterface");
        ui = go.GetComponent(typeof(UIScript)) as UIScript;


    }

    public void LooseGame()
    {
        ui.GameOver(0);
    }

    public void WinGame()
    {
        ui.GameWon(0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
