using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Playground/Attributes/KksCollection")]
public class KksCollection : MonoBehaviour
{

    [HideInInspector]
    public int highNumber;
    private int tarveOrderNr;
    private int osaOrderNr = 999;
    private int orderNumber;
    private GameObject sillanOsa;
    private GameObject siltaTarve;
    private GameObject[] sillanOsat;
    private GameObject siltaPala;
    private enum luvut {yksi, kaksi, kolme };


    // Use this for initialization
    void Start()
    {
        Debug.Log("collection is alive");
        highNumber = 1;
        Debug.Log("KKSCOLLECTION highNumber in Start = " + highNumber);

        sillanOsat = GameObject.FindGameObjectsWithTag("Sillanosa");

        
            siltaPala = GameObject.FindWithTag("Sillanpaikka");
        

      
        string s = string.Empty;
        foreach (luvut t in System.Enum.GetValues(typeof(luvut)))
        {
            s += t.ToString();
        }
        Debug.Log("FOREACH TESTI" + s);

        if (sillanOsat != null) Debug.Log("LÖYTYY!!!");

        foreach (GameObject go in sillanOsat)
        {
            //if (go != this.gameObject)       // Here, you check if the game object is not this game object
            //{
            //    continue;
            //}
            
            osaOrderNr = go.GetComponent<InOrderCollectableAttribute>().orderNumber;
            Debug.Log("STARTTI, asetetaan sillanosa passiiviseksi, nro = " + osaOrderNr);
            // set go passive
            go.SetActive(false);

        }
    }

    public void  AddNumber(int oNr)
    {
        //GameObject.FindWithTag("Sillanosa"); 
        //tarveOrderNr = siltaTarve.GetComponent<InOrderCollectableAttribute>().orderNumber;
        tarveOrderNr = oNr;
        Debug.Log("ALOITETAAN = " + tarveOrderNr);

        foreach (GameObject go in sillanOsat)
        {
            osaOrderNr = go.GetComponent<InOrderCollectableAttribute>().orderNumber;
            Debug.Log("Käsitellään sillanosaa = " + osaOrderNr);
            if (tarveOrderNr == osaOrderNr)
            {
                go.SetActive(true);
                highNumber++;
                Debug.Log("collection highNumber now = " + highNumber);
                //return true;
            }
           
             if(highNumber == 9)
            {
                siltaPala.SetActive(false);
            } 
            
        }

        //return false;

    }

    // Update is called once per frame
    void Update()
    {

    }
}

