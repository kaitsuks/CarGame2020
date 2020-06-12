using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Mapreader : MonoBehaviour
{

    
    public GameObject m_Explosion; //EI KÄYTÖSSÄ

    public GameObject myCar; //AJOPELI
    public GameObject miniCar; //AJOPELI TOISAALLA

    //FIRST PERSON VIEW -RAAMIT
    public GameObject rightM1;
    //public GameObject rightM2;
    //public GameObject rightM3;
    //public GameObject rightM4;
    //public GameObject rightM5;
    public GameObject leftM1;
    //public GameObject leftM2;
    //public GameObject leftM3;
    //public GameObject leftM4;
    //public GameObject leftM5;

    //PÄÄTOLPPIEN SPRITET
    private float spriteX;
    private float spriteY;

    //kohdetaulukOT
    int arraySize = 12;
    private GameObject[,] targets;
    private SpriteRenderer[,] renderers; 
    private GameObject[,] targets2;
    private SpriteRenderer[,] renderers2;
    //karttaobjekteja - targetteja
    public GameObject tolppaPrefab;
    public GameObject tolppa2Prefab;
    public GameObject flagBluePrefab;
    public GameObject flowerPrefab;
    public GameObject ruohoPrefab;
    

    //TILEBASE
    public TileBase tieTiili;
    public TileBase kiviTiili;
    public TileBase redSign;
    public TileBase flagBlue;
    public TileBase flower;

    //targettien spriteja
    public Sprite ruohoSprite;
    public Sprite flagBlueSprite;
    public Sprite flagRedSprite;
    public Sprite cactus2Sprite;
    public Sprite tree1Sprite;
    public Sprite miniCarSprite;
    public Sprite flowerSprite;

    //object pool targets
    private Vector2 objectPoolPosition;

    //SKROLLAAVAT TAUSTAT
    public GameObject landscape;
    public GameObject landscape2;
    private BoxCollider2D landscapeColllider;
    private float landscapeLenght;
    private BoxCollider landscapeCollider;

    //RENDAAJAT 1STPVIEW-NÄYTÖLLE EI TARVITA
    //private SpriteRenderer rightM1renderer;
    //private SpriteRenderer rightM2renderer;
    //private SpriteRenderer rightM3renderer;
    //private SpriteRenderer rightM4renderer;
    //private SpriteRenderer rightM5renderer;
    //private SpriteRenderer leftM1renderer;
    //private SpriteRenderer leftM2renderer;
    //private SpriteRenderer leftM3renderer;
    //private SpriteRenderer leftM4renderer;
    //private SpriteRenderer leftM5renderer;

   //GRIDI JA TILEMAPIT
    private Grid m_Grid;
    private Tilemap m_Foreground;
    private Tilemap m_BackGround;
    private Tilemap m_TreeGround;
    private Tilemap m_Road;
    private Tilemap m_Collider;
    //private GridInformation m_Info;

    //AUTON TIEDOT
    private Vector3 carPosition;
    private float euler;
    Vector3Int gridPos;
    Vector3Int checkPos;

    //private Sprite leftPole;
    //private Sprite rightPole;

    
    //TARKISTUSTAULUKOT JA MUUTTUJAT
    private float[,,] trigo; //TAULUKKO 11 X 11, RUUTUA, ETÄISYYS JA SUUNTA
    private int[,,] local; //TAULUKKO 11 X 11, RUUTUA, kohteen sijainti ja laji tarvittaessa
    private float aAngle; //"viereinen" kulma
    private float aAngleDeg; //kulma asteina
    private float dist; //etäisyys eli hypotenuusa
    private float a; //vastainen kateetti
    private float b; //viereinen kateetti
    private int aInt; //vastainen kateetti
    private int bInt; //viereinen kateetti

    

    public float xSkaalain = 3;

    float newSize;
    float newSize2;
    float sizeX;

    private Vector3 scaleChange, positionChange;
    private Vector3 scaleVector;

    // Start is called before the first frame update
    void Awake()
    {
        m_Grid = GameObject.Find("Grid").GetComponent<Grid>();
        //m_Info = m_Grid.GetComponent<GridInformation>();
        m_Foreground = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        //m_BackGround = GameObject.Find("Ruoho").GetComponent<Tilemap>();
        //m_TreeGround = GameObject.Find("Puut").GetComponent<Tilemap>();
        m_Road = GameObject.Find("Road").GetComponent<Tilemap>();
        m_Collider = GameObject.Find("ColliderTilemap").GetComponent<Tilemap>();
        //leftPole = GameObject.Find("LeftMark1").GetComponent<Sprite>();
        //rightPole = GameObject.Find("RightMark1").GetComponent<Sprite>();
        //myCar = GameObject.Find("car_yellow");
        carPosition = myCar.transform.position;
        trigo = new float[arraySize, arraySize, 2]; //TAULUKKO 11 X 11, RUUTUA, ETÄISYYS JA SUUNTA
        local = new int[arraySize, arraySize, 3]; //TAULUKKO 11 X 11, RUUTUA, kohteen sijainti 0 ja laji 1, ja 2 tarvittaessa
        //kohdeobjektitaulukko
        targets = new GameObject[arraySize, arraySize];
        targets2 = new GameObject[arraySize, arraySize];
        renderers = new SpriteRenderer[arraySize, arraySize];
        renderers2 = new SpriteRenderer[arraySize, arraySize];
        //rightM1renderer =  rightM1.GetComponent<SpriteRenderer>();
        //rightM2renderer =  rightM2.GetComponent<SpriteRenderer>();
        //rightM3renderer =  rightM3.GetComponent<SpriteRenderer>();
        //rightM4renderer =  rightM4.GetComponent<SpriteRenderer>();
        //rightM5renderer =  rightM5.GetComponent<SpriteRenderer>();
        //leftM1renderer =   leftM1.GetComponent<SpriteRenderer>();
        //leftM2renderer =   leftM2.GetComponent<SpriteRenderer>();
        //leftM3renderer =   leftM3.GetComponent<SpriteRenderer>();
        //leftM4renderer =   leftM4.GetComponent<SpriteRenderer>();
        //leftM5renderer =   leftM5.GetComponent<SpriteRenderer>();
        landscapeLenght = landscape2.GetComponent<BoxCollider2D>().size.x;

        scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
        positionChange = new Vector3(0.0f, -0.005f, 0.0f);
        spriteX = rightM1.transform.position.x;
        spriteY = rightM1.transform.position.y;
        objectPoolPosition = new Vector2(60f, 0f);

        InitGame();
    }

    private void Start()
    {
        
        StartCoroutine(ReadMap());
    }

    void InitGame()
    {
        for (int j = 0; j < arraySize; j++)
        {
            //TÄYTETÄÄN TAULUKKO
            for(int i = 0; i < arraySize; i++)
            {
                //LASKETAAN KULMA
                
                //aAngle = Mathf.Atan2(j + arraySize/2, i + arraySize / 2);
                aAngle = Mathf.Atan2(j - i, i/2);
                trigo[j, i, 0] = aAngle;
                //LASKETAAN ETÄISYYS
                dist = j / Mathf.Sin(aAngle);
                trigo[j, i, 1] = dist;
                //tyhjennetään local-taulukko varmuuden vuoksi
                local[j, i, 0] = 0;
                local[j, i, 1] = 0;
                local[j, i, 2] = 0;
                //luodaan targetit taulukkoon
                targets[j, i] = (GameObject)Instantiate(ruohoPrefab, new Vector2(objectPoolPosition.x + i, objectPoolPosition.y + j), Quaternion.identity);
                targets[j, i].transform.position = new Vector2(0f + j, -50f + i);
                targets2[j, i] = (GameObject)Instantiate(ruohoPrefab, new Vector2(objectPoolPosition.x + i, objectPoolPosition.y + j), Quaternion.identity);
                renderers[j, i] = targets[j, i].GetComponent<SpriteRenderer>();
                renderers2[j, i] = targets2[j, i].GetComponent<SpriteRenderer>();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        carPosition = myCar.transform.position;
        gridPos = m_Grid.WorldToCell(carPosition);
        gridPos.z = 0;
        //if (m_Road.GetTile(gridPos) != null) //SIJAINNIN TESTAUS
        //{
        //    Debug.Log("KOLARI AITAAN");
        //}
        euler = 360f - myCar.transform.rotation.eulerAngles.z; //AJOSUUNTA KORJATTUNA
        StartCoroutine(ReadMap());
    }

    IEnumerator ReadMap()
    {
        int offsetX = arraySize / 2;
        int offsetY = arraySize / 2;
        Vector3Int findPos;
        findPos = new Vector3Int((gridPos.x - offsetX), (gridPos.y - offsetY), 0);

        //LUETAAN KOKO RUUDUKKO
        //Debug.Log("LUETAAN RUUDUKKO PAIKASSA " + gridPos);
        for (int j = 0; j < arraySize; j++)
        {
            //LUETAAN TAULUKKO
            for (int i = 0; i < arraySize; i++)
            {

                //TARKISTETAAN RUUTU
                checkPos = new Vector3Int((findPos.x + j), (findPos.y + i), 0);
                if (m_Collider.GetTile(checkPos) != null)
                {
                    //Debug.Log("TIETÄ LÖYDETTY " + checkPos);
                    local[j, i, 0] = 1;
                }
                if (m_Road.GetTile(checkPos) == redSign)
                {
                    //  Debug.Log("PUNAINEN TOLPPA LÖYDETTY " + checkPos);
                    local[j, i, 0] = 2;
                }
                if (m_Road.GetTile(checkPos) == flagBlue)
                {
                    local[j, i, 0] = 3;
                }
                if (m_Road.GetTile(checkPos) == flower)
                {
                    local[j, i, 0] = 4;
                }

            }
        } // int j

                //UUDET LOOPIT TÄHÄN?
                //LUETAAN KOKO RUUDUKKO
                //Debug.Log("LUETAAN RUUDUKKO PAIKASSA " + gridPos);
                for (int j = 0; j < arraySize; j++)
                {
                    //LUETAAN TAULUKKO
                    for (int i = 0; i < arraySize; i++)
                    {

                        //TARKISTETAAN RUUTU
                        checkPos = new Vector3Int((findPos.x + j), (findPos.y + i), 0);
                //if (local[j, i, 0] != 0 )
                {
                    //ETÄISYYDEN LASKEMINEN VEKTOREILLA
                    dist = Vector3.Distance(gridPos, checkPos);
                   
                    if (local[j, i, 0] == 0)
                    {
                        targets[j, i] = ruohoPrefab;
                        renderers[j, i].sprite = ruohoSprite;
                        //targets[i, j] = (GameObject)Instantiate(ruohoPrefab, new Vector2(objectPoolPosition.x + i, objectPoolPosition.y + j), Quaternion.identity);
                        //targets[i, j] = ruohoPrefab;
                        //DrawObject(i, j);
                        //StartCoroutine(DrawObject(i, j));
                        // Debug.Log("PIIRRETTY TOLPPA " + i * j);
                    }

                    if (local[j, i, 0] == 1)
                    {
                        targets[j, i] = flagBluePrefab;
                        targets2[j, i] = flagBluePrefab;
                        renderers[j, i].sprite = flagBlueSprite;
                    }
                    //else
                    //    targets[i, j] = ruohoPrefab;

                    if (local[j, i, 0] == 2)
                    {
                        targets[j, i] = tolppa2Prefab;
                        targets2[j, i] = tolppa2Prefab;
                        renderers[i, j].sprite = flagRedSprite;
                    }
                    if (local[j, i, 0] == 3)
                    {
                        targets[j, i] = flagBluePrefab;
                        targets2[j, i] = flagBluePrefab;
                        renderers[j, i].sprite = flagBlueSprite;
                    }
                    if (local[j, i, 0] == 4)
                    {
                        targets[i, j] = flowerPrefab;
                        targets2[i, j] = flowerPrefab;
                        renderers[i, j].sprite = flowerSprite;
                    }
                }
            }
        }

                    //PIIRRETÄÄN TUULILASILLE

                    for (int j = 0; j < arraySize; j++)
                    {
                        for (int i = 0; i < arraySize; i++)
                        {
                //Debug.Log("Ruutu " + i * j);
                //VIITTAUS PITÄIS VAIHTAA KLOONIIN

                // testi

                //if (targets2[j, i] == null)
                //{
                //    targets2[j, i] = GameObject.Instantiate(targets[j, i]);
                //}

                //Debug.Log("TARGET2 " + targets2[j, i]);

                //HAETAAN SUUNTAKULMA
                //aAngle = 2 * Mathf.PI - trigo[j, i, 0];
                aAngle = trigo[j, i, 0];
                //MUUNNETAAN ASTEIKSI
                aAngleDeg = Mathf.Rad2Deg * aAngle - 270; // - 90;
                //Debug.Log("TARGET   " + targets2[j, i] +"  " + aAngleDeg);
                //miniCar.transform.rotation = myCar.transform.rotation;
                euler = 360f - euler; //TEST, EULER MENEE 360 ASTETTA VASTAPÄIVÄÄN TEST
                newSize2 = 2; //ALKUARVO
                if (dist > 1f)
                {
                    newSize2 = 6 * newSize2 / (dist);  // / 10f; //sizeFactor //TEST
                }
                            if (newSize2 > 10f) newSize2 = 10f; //rajoitin
                            if (newSize2 < 0.1f) newSize2 = 0.1f; //rajoitin

              //  newSize2 = 2; //TEST

                if (targets2[i, j] != null)
                {
                    if (!float.IsNaN(newSize2))
                    {
                        scaleVector = new Vector3(1f, newSize2, 1f);
                        targets2[i, j].transform.localScale = scaleVector;
                    }
                    renderers2[j, i].sprite = renderers[j, i].sprite; //pitää olla muuten siniset liput ei näy
                    
           //KOE         targets2[j, i].transform.position = new Vector3(spriteX - (euler * 2 - aAngleDeg)/12, spriteY -6 + dist, 0f);
                    targets2[j, i].transform.position = new Vector3(spriteX +32 - (euler - aAngleDeg)/16, spriteY - 6 + dist, 0f);
                    //targets2[j, i].transform.position = new Vector3(spriteX - aAngleDeg - euler -45, spriteY +1, 0f);
                    //targets2[j, i].transform.position = new Vector3(-20, -30, 0f);
                    //  targets[j, i].transform.position = new Vector3(spriteX - aAngleDeg - euler - 45, spriteY + 1, 0f);

                    //Debug.Log("Target " + targets2[j, i] + " position " + targets2[j, i].transform.position + "SCALE " + targets2[j, i].transform.localScale);
                       renderers[j, i].color = new Color(newSize2, dist, 0.6f); //VÄRI ETÄISYYDEN MUKAAN
                    //   renderers[j, i].sortingOrder = (int)newSize2 * 10; //HUOM ei välilyöntiä (int)newSize2 // JÄRJESTYS ETÄISYYDEN MUKAAN

                } //not null
                landscape.transform.position = new Vector2(euler, spriteY + 3);
                            landscape2.transform.position = new Vector2(euler + landscapeLenght, spriteY + 3);
                            miniCar.transform.position = new Vector2(0 + arraySize / 2, -50 + arraySize / 2);
                            //miniCar.transform.rotation = new Quaternion(myCar.transform.rotation.x, myCar.transform.rotation.y, myCar.transform.rotation.z - 180f, 0);
                            miniCar.transform.rotation = myCar.transform.rotation;
                        }
                    }
                
            //TYHJENNETÄÄN TAULUKKO
            for (int j = 0; j < arraySize; j++)
            {
                for (int i = 0; i < arraySize; i++)
                {
                    //TYHJENNETÄÄN RUUTU
                    local[j, i, 0] = 0; //TEST
                    //tyhjennetään prefabit
                    targets2[j, i] = ruohoPrefab; // TEST ei jaksa pyörittää, vai jaksaako?
                    targets[j, i] = ruohoPrefab;
                //GameObject.Destroy(targets2[j, i]); //TEST, ei voi tuhota
            }
            }
            //Debug.Log("TAULUKKO TYHJENNETTY");

            yield return new WaitForSeconds(0.1f);
        }
    
    //TÄMÄ COROUTINE EI KÄYTÖSSÄ
    //IEnumerator DrawObject(int i, int j)
    void DrawObject(int i, int j)
    { 
        //Debug.Log("PIIRRETÄÄN TOLPPA " + j *  i);
        //KULMA
        //aAngle = trigo[j, i, 0] + euler;
        //aAngle = (trigo[j, i, 0] + euler)/360; //TEST
        //aAngle = euler - trigo[j, i, 0]; //TEST WIRHE
        aAngle = 2 * Mathf.PI - trigo[j, i, 0];
        //aAngle =  trigo[j, i, 0];
        //Debug.Log(" ANGLE ON TOLPPAAN " + aAngle);
        aAngleDeg = Mathf.Rad2Deg * aAngle;
        //Debug.Log(" ANGLE ASTEINA TOLPPAAN " + aAngleDeg);
        //ETÄISYYS
        //dist = trigo[j, i, 1]; // LASKETTU VEKTOREILLA
        //  if (float.IsNaN(dist)) dist = 0.1f; //TURHA NYT
        //  Debug.Log(" ETÄISYYS TOLPPAAN " + dist);
        //euler = 360f - euler; //TEST, EULER MENEE 360 ASTETTA VASTAPÄIVÄÄN
        //aAngleDeg = aAngleDeg + euler; //TEST
        //Debug.Log(" DISTANSSI ON TOLPPAAN " + dist);
        //newSize = targets[i, j].transform.localScale.y;
        //dist = 0.1f; //TEST
        //newSize2 = 1f;
        //Debug.Log(" ORIGINAALI SKAALA ON " + newSize);
        //newSize = rightM1.transform.localScale.y * dist;
        newSize2 = 1f;
        // newSize2 = targets[i, j].transform.localScale.y;
        //newSize2 = newSize2 * (dist) /100f; //sizeFactor
        //   newSize2 = newSize2 / (dist);  // / 10f; //sizeFactor //TEST
        if (newSize2 > 5f) newSize2 = 5f; //rajoitin
        if (newSize2 < 0.1f) newSize2 = 0.1f; //rajoitin
                                              //Debug.Log(" UUSITTU Y SKAALA ON " + newSize2);
                                              //sizeX = rightM1.transform.localScale.x;
        sizeX = targets[i, j].transform.localScale.x;
        //Debug.Log(" X SKAALA ON " + sizeX);
        //scaleVector = new Vector3(sizeX, newSize2, 1f);
        //if (!float.IsNaN(transform.position.x) && !float.IsNaN(transform.position.y) && !float.IsNaN(transform.position.z))
        //{
        //    //Do stuff
        //}
        //  if (!float.IsNaN(newSize2))
        {
            scaleVector = new Vector3(1f, newSize2, 1f);
            //rightM1.transform.localScale = scaleVector;
            //rightM1.transform.position = new Vector3(- (aAngle/30 -15), spriteY - 10, 0f);
            //    targets[i, j].transform.localScale = scaleVector; // TEST
            //targets[i, j].transform.position = new Vector3(spriteX + aAngle * xSkaalain - 3, spriteY - 3f, 0f);
            //if (euler - aAngle < 45f)
            {
                //targets[i, j].transform.position = new Vector3(spriteX + aAngle * 120 - 130 + euler / 10, spriteY +1f +dist/100, 0f);
                //targets[i, j].transform.position = new Vector3(spriteX - aAngleDeg /24 + euler / 24, spriteY + dist / 10, 0f);
                //targets[i, j].transform.position = new Vector3(spriteX -10 + (aAngleDeg - euler), spriteY, 0f);
                //targets[i, j].transform.position = new Vector3(spriteX - 10, spriteY, 0f);
                //targets[i, j].transform.position = new Vector3(50, 0, 0f); //TEST
                flowerPrefab.transform.position = new Vector3(150, 0, 0f); //TEST
                Debug.Log(" Target " + targets[i, j] + " position " + targets[i, j].transform.position  + "SCALE " + targets[i, j].transform.localScale);
                //   renderers[i, j].color = new Color(newSize2, dist, 0.6f);
                //   renderers[i, j].sortingOrder = (int)newSize2 * 10; //HUOM ei välilyöntiä (int)newSize2 // TEST
                landscape.transform.position = new Vector2(euler, spriteY + 3);
                landscape2.transform.position = new Vector2(euler + landscapeLenght, spriteY + 3);

            }
        }
        //yield return new WaitForSeconds(0.5f);
        // return; // TEST
    }
}
