using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Mapreader : MonoBehaviour
{

    public TileBase tieTiili;
    public TileBase kiviTiili;
    public GameObject m_Explosion;
    //public GameObject myCar;

    private GameObject myCar;

    private Grid m_Grid;
    private Tilemap m_Foreground;
    private Tilemap m_BackGround;
    private Tilemap m_TreeGround;
    private Tilemap m_Road;
    private Tilemap m_Collider;
    //private GridInformation m_Info;
    private Vector3 carPosition;
    private Sprite leftPole;
    private Sprite rightPole;
    private float euler;

    // Start is called before the first frame update
    void Start()
    {
        m_Grid = GameObject.Find("Grid").GetComponent<Grid>();
        //m_Info = m_Grid.GetComponent<GridInformation>();
        m_Foreground = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        //m_BackGround = GameObject.Find("Ruoho").GetComponent<Tilemap>();
        //m_TreeGround = GameObject.Find("Puut").GetComponent<Tilemap>();
        m_Road = GameObject.Find("Road").GetComponent<Tilemap>();
        m_Collider = GameObject.Find("ColliderTilemap").GetComponent<Tilemap>();
        leftPole = GameObject.Find("LeftMark").GetComponent<Sprite>();
        rightPole = GameObject.Find("RightMark").GetComponent<Sprite>();
        myCar = GameObject.Find("car_yellow");
        carPosition = myCar.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        carPosition = myCar.transform.position;
        Vector3Int gridPos = m_Grid.WorldToCell(carPosition);
        if (m_Collider.GetTile(gridPos) != null)
        {
            Debug.Log("KOLARI AITAAN");
        }

        euler = myCar.transform.rotation.eulerAngles.z;

        //AUTON SUUNTA SELVITETTÄVÄ - ROTATION
        if (euler > 0f && euler < 46f)
            //LUODE
        {
            Debug.Log(" LUOTEESSA " + euler);
        }

        if (euler > 45 && euler < 90f)
        //LÄNSI
        {
            Debug.Log(" LÄNNESSÄ " + euler);
        }

        if (euler > 90f && euler < 136f)
        //LOUNAS
        {
            Debug.Log(" LOUNAASSA " + euler);
        }

        if (euler > 135f && euler < 181f)
        //ETELÄ
        {
            Debug.Log(" ETELÄSSÄ " + euler);
        }

        if (euler > 180f && euler < 236f)
        //KAAKKO
        {
            Debug.Log(" KAAKOSSA " + euler);
        }

        if (euler > 235f && euler < 271f)
        //ITÄ
        {
            Debug.Log(" IDÄSSÄ " + euler);
        }

        if (euler > 270f && euler < 336f)
        //KOILLINEN
        {
            Debug.Log(" KOILLISESSA " + euler);
        }

        if (euler > 335f && euler < 361f || euler == 0f)
        //POHJOINEN
        {
            Debug.Log(" POHJOISESSA " + euler);
        }


        //KAHDEKSAN ILMANSUUNNAN MUKAAN LUETTAVA RUUDUT
        //Debug.Log(" EULER " + euler);
        //Debug.Log(" ROTATION " + myCar.transform.rotation);


        //SIIRRETÄÄN SPRITEJÄ SEN MUKAAN


    }
}
