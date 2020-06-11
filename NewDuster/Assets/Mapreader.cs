using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


//var pitch:float = transform.eulerAngles.x;
//var yaw:float   = transform.eulerAngles.y;
//var roll:float  = transform.eulerAngles.z;
//pitch -= 15;
//yaw   += 90;
//roll   = 45;
//transform.rotation = Quaternion.Euler(y, x, z);

//Vector3 relative = transform.InverseTransformPoint(target.position);
//float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
//transform.Rotate(0, angle, 0);

//float deg = 30.0F;
//   float rad = deg * Mathf.Deg2Rad;
//    Debug.Log(deg + " degrees are equal to " + rad + " radians.");


public class Mapreader : MonoBehaviour
{

    public TileBase tieTiili;
    public TileBase kiviTiili;
    public GameObject m_Explosion;
    public GameObject myCar;
    public GameObject rightM1;
    public GameObject rightM2;
    public GameObject rightM3;
    public GameObject rightM4;
    public GameObject rightM5;
    public GameObject leftM1;
    public GameObject leftM2;
    public GameObject leftM3;
    public GameObject leftM4;
    public GameObject leftM5;
    //kohdetaulukko
    private GameObject[,] targets;
    private SpriteRenderer[,] renderers;
    public GameObject tolppaPrefab;
    public GameObject tolppa2Prefab;
    public GameObject flagBluePrefab;
    public GameObject flowerPrefab;
    public GameObject ruohoPrefab;
    public GameObject miniCar;
    //public GameObject tolppa3Prefab;
    public TileBase redSign;
    public TileBase flagBlue;
    public TileBase flower;

    public Sprite testSprite;
    public Sprite flagBlueSprite;
    public Sprite flagRedSprite;
    public Sprite cactus2Sprite;
    public Sprite tree1Sprite;
    public Sprite miniCarSprite;

    private Vector2 objectPoolPosition;

    public GameObject landscape;
    public GameObject landscape2;
    private BoxCollider2D landscapeColllider;
    private float landscapeLenght;
    private BoxCollider landscapeCollider;
    private SpriteRenderer rightM1renderer;
    private SpriteRenderer rightM2renderer;
    private SpriteRenderer rightM3renderer;
    private SpriteRenderer rightM4renderer;
    private SpriteRenderer rightM5renderer;
    private SpriteRenderer leftM1renderer;
    private SpriteRenderer leftM2renderer;
    private SpriteRenderer leftM3renderer;
    private SpriteRenderer leftM4renderer;
    private SpriteRenderer leftM5renderer;
    //   private GameObject myCar;
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
    Vector3Int gridPos;
    Vector3Int checkPos;
    int arraySize = 12;

    private float[,,] trigo; //TAULUKKO 11 X 11, RUUTUA, ETÄISYYS JA SUUNTA
    private int[,,] local; //TAULUKKO 11 X 11, RUUTUA, kohteen sijainti ja laji tarvittaessa
    private float aAngle; //"viereinen" kulma
    private float aAngleDeg; //kulma asteina
    private float dist; //etäisyys eli hypotenuusa
    private float a; //vastainen kateetti
    private float b; //viereinen kateetti
    private int aInt; //vastainen kateetti
    private int bInt; //viereinen kateetti

    private float spriteX;
    private float spriteY;

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
        renderers = new SpriteRenderer[arraySize, arraySize];
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
                //trigo[j, i, 0] = Mathf.Atan2(j, i);
                aAngle = Mathf.Atan2(j, i);
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

                //targets[j, i] = ruohoPrefab;
                targets[j, i].transform.position = new Vector2(0f + j, -50f + i);
                renderers[j, i] = targets[j, i].GetComponent<SpriteRenderer>();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        carPosition = myCar.transform.position;
        gridPos = m_Grid.WorldToCell(carPosition);
        gridPos.z = 0;
        //if (m_Collider.GetTile(gridPos) != null)
        //{
        //    Debug.Log("KOLARI AITAAN");
        //}

        //euler = myCar.transform.rotation.eulerAngles.z;
        euler = 360f - myCar.transform.rotation.eulerAngles.z; //TEST
        StartCoroutine(ReadMap());
    }

    IEnumerator ReadMap()
    {
        int offsetX = arraySize / 2;
        int offsetY = arraySize / 2;
        Vector3Int findPos;
        //findPos = new Vector3Int((gridPos.x) - arraySize / 2, (gridPos.y) - arraySize / 2, 0);
        findPos = new Vector3Int((gridPos.x - offsetX), (gridPos.y - offsetY), 0);

        //LUETAAN KOKO RUUDUKKO
        //Debug.Log("LUETAAN RUUDUKKO PAIKASSA " + gridPos);
        for (int j = 0; j < arraySize; j++)
        {
            //LUETAAN TAULUKKO
            for (int i = 0; i < arraySize; i++)
            {
                //ALUSTETAAN TARGETTIEN PAIKAT
               // targets[i, j].transform.position = new Vector2(spriteX + i, spriteY + j);
                //TARKISTETAAN RUUTU
                checkPos = new Vector3Int((findPos.x + j), (findPos.y + i), 0);
                //checkPos = new Vector3Int(gridPos.x + i, gridPos.y + j, 0); //TEST
                if (m_Collider.GetTile(checkPos) != null)
                {
                    //Debug.Log("TIETÄ LÖYDETTY " + checkPos);
                    local[j, i, 0] = 1;
                }

                //if (m_Foreground.GetTile(checkPos)  == redSign)
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


                ////trigo[j, i, 0] = Mathf.Atan2(j, i);
                //aAngle = Mathf.Atan2(j, i);
                //trigo[j, i, 0] = aAngle;
                ////LASKETAAN ETÄISYYS
                //dist = j / Mathf.Sin(aAngle);
                //trigo[j, i, 1] = dist;
           // }
        //}
        //PIIRRETÄÄN TUULILASILLE
        
        //for (int j = 0; j < arraySize; j++)
        //{
            //
            //for (int i = 0; i < arraySize; i++)
            //{
                //Debug.Log("Ruutu " + i * j);


                //TARKISTETAAN RUUTU
                checkPos = new Vector3Int((findPos.x + j), (findPos.y + i), 0);
                //if (local[j, i, 0] != 0 )
                {
                    //Vector3 check = new Vector3(j, i, 0);
                    dist = Vector3.Distance(gridPos,  checkPos);
                    // Debug.Log("gridPos,  checkPos, dist " + gridPos + "  " + checkPos + "  " + dist);

                    //testSprite;
                    //flagBlueSprite
                    //flagRedSprite;
                    //cactus2Sprite;
                    //tree1Sprite;

                    if (local[j, i, 0] == 0)
                    {
                        targets[j, i] = ruohoPrefab;
                        renderers[j, i].sprite = testSprite;
                        //targets[i, j] = (GameObject)Instantiate(ruohoPrefab, new Vector2(objectPoolPosition.x + i, objectPoolPosition.y + j), Quaternion.identity);
                        //targets[i, j] = ruohoPrefab;
                        //DrawObject(i, j);
                        //StartCoroutine(DrawObject(i, j));
                        // Debug.Log("PIIRRETTY TOLPPA " + i * j);
                    }

                    if (local[j, i, 0] == 1)
                    {
                        targets[j, i] = flagBluePrefab;
                        renderers[j, i].sprite = flagBlueSprite;
                        //DrawObject(i, j);
                        //StartCoroutine(DrawObject(i, j));
                        //Debug.Log("PIIRRETTY TOLPPA " + i * j);
                    }
                    //else
                    //    targets[i, j] = ruohoPrefab;

                    if (local[j, i, 0] == 2)
                    {
                        targets[j, i] = tolppa2Prefab;
                        renderers[i, j].sprite = flagRedSprite;
                        //DrawObject(i, j);
                        //StartCoroutine(DrawObject(i, j));
                        //  Debug.Log("PIIRRETTY punainen tolppa " + i * j);
                    }
                    //else
                    //    targets[i, j] = ruohoPrefab;

                    if (local[j, i, 0] == 3)
                    {
                        targets[j, i] = flagBluePrefab;
                        renderers[j, i].sprite = cactus2Sprite;
                        //DrawObject(i, j);
                        //StartCoroutine(DrawObject(i, j));
                        //  Debug.Log("PIIRRETTY SININEN LIPPU " + i * j);
                    }
                    //else
                    //    targets[i, j] = ruohoPrefab;

                    if (local[j, i, 0] == 4)
                    {
                        targets[i, j] = flowerPrefab;
                        renderers[i, j].sprite = tree1Sprite;
                        //StartCoroutine(DrawObject(i, j));
                        //Debug.Log("PIIRRETTY sininen KUKKA " + i * j);
                    }
                    //else
                    //    targets[i, j] = ruohoPrefab;

                    //targets[arraySize / 2, arraySize / 2] = miniCar;
                    //renderers[arraySize / 2, arraySize / 2].sprite = miniCarSprite;


                    aAngle = 2 * Mathf.PI - trigo[j, i, 0];
                    //aAngle = trigo[j, i, 0];
                    aAngleDeg = Mathf.Rad2Deg * aAngle - 90;
                    //miniCar.transform.rotation = myCar.transform.rotation;
                //euler = 360f - euler; //TEST, EULER MENEE 360 ASTETTA VASTAPÄIVÄÄN
                //aAngleDeg = aAngleDeg + euler; //TEST
                //newSize2 = 1f;
                newSize2 = newSize2 / (dist);  // / 10f; //sizeFactor //TEST
                if (newSize2 > 5f) newSize2 = 5f; //rajoitin
                if (newSize2 < 0.1f) newSize2 = 0.1f; //rajoitin
                scaleVector = new Vector3(1f, newSize2, 1f);
                    //targets[j, i].transform.position = new Vector3(spriteX + aAngle * 120 - 130 + euler / 10, spriteY +1f +dist/100, 0f);
                    //  targets[j, i].transform.position = new Vector3(spriteX - aAngleDeg /24 + euler / 24, spriteY + dist / 10, 0f);
                    //  testSprite.transform.position = new Vector3(spriteX - aAngleDeg / 24 + euler / 24, spriteY + dist / 10, 0f);

                    //testataan laatikkoa        targets[i, j].transform.position = new Vector3(spriteX  +270 - (aAngleDeg - euler), spriteY, 0f);
                    //targets[j, i].transform.position = new Vector3(spriteX - 10, spriteY, 0f);
                    //targets[j, i].transform.position = new Vector3(spriteX - 2, spriteY, 0f); //TEST
                    //flowerPrefab.transform.position = new Vector3(spriteX - 1, spriteY + 1, 0f); //TEST
                    //flowerPrefab.transform.position = new Vector3(150, 0, 0f); //TEST
                    //Debug.Log("Target " + targets[j, i] + " position " + targets[i, j].transform.position + "SCALE " + targets[i, j].transform.localScale);
                    //   renderers[j, i].color = new Color(newSize2, dist, 0.6f);
                    //   renderers[j, i].sortingOrder = (int)newSize2 * 10; //HUOM ei välilyöntiä (int)newSize2 // TEST
                    landscape.transform.position = new Vector2(euler, spriteY + 3);
                landscape2.transform.position = new Vector2(euler + landscapeLenght, spriteY + 3);
                    //miniCar.transform.position = new Vector2(objectPoolPosition.x + arraySize/2, objectPoolPosition.y + arraySize / 2);
                    miniCar.transform.position = new Vector2(0 + arraySize / 2, -50 + arraySize / 2);
                    //miniCar.transform.rotation = new Quaternion(myCar.transform.rotation.x, myCar.transform.rotation.y, myCar.transform.rotation.z - 180f, 0);
                    miniCar.transform.rotation = myCar.transform.rotation;
                }

            }
        }

        //flowerPrefab.transform.position = new Vector3(spriteX - 1, spriteY + 1, 0f); //TEST
        //rightM1.transform.position = new Vector3(spriteX - 1, spriteY + 3, 0f); //TEST

        //TYHJENNETÄÄN TAULUKKO
        for (int j = 0; j < arraySize; j++)
        {
            for (int i = 0; i < arraySize; i++)
            {
                //TYHJENNETÄÄN RUUTU
                local[j, i, 0] = 0; //TEST
                //tyhjennetään prefabit
                //targets[i, j] = ruohoPrefab; // TEST ei jaksa pyörittää
            }
        }
        //Debug.Log("TAULUKKO TYHJENNETTY");

        //if (m_Collider.GetTile(gridPos) != null)
        //{
        //    Debug.Log("KOLARI AITAAN");
        //}

        //SIIRRETÄÄN SPRITEJÄ SEN MUKAAN

        ////AUTON SUUNTA SELVITETTÄVÄ - ROTATION
        //if (euler > 0f && euler < 46f)
        //    //LUODE
        //{
        //    Debug.Log(" LUOTEESSA " + euler);
        //}

        //if (euler > 45 && euler < 90f)
        ////LÄNSI
        //{
        //    Debug.Log(" LÄNNESSÄ " + euler);
        //}

        //if (euler > 90f && euler < 136f)
        ////LOUNAS
        //{
        //    Debug.Log(" LOUNAASSA " + euler);
        //}

        //if (euler > 135f && euler < 181f)
        ////ETELÄ
        //{
        //    Debug.Log(" ETELÄSSÄ " + euler);
        //}

        //if (euler > 180f && euler < 236f)
        ////KAAKKO
        //{
        //    Debug.Log(" KAAKOSSA " + euler);
        //}

        //if (euler > 235f && euler < 271f)
        ////ITÄ
        //{
        //    Debug.Log(" IDÄSSÄ " + euler);
        //}

        //if (euler > 270f && euler < 336f)
        ////KOILLINEN
        //{
        //    Debug.Log(" KOILLISESSA " + euler);
        //}

        //if (euler > 335f && euler < 361f || euler == 0f)
        ////POHJOINEN
        //{
        //    Debug.Log(" POHJOISESSA " + euler);
        //}


        //KAHDEKSAN ILMANSUUNNAN MUKAAN LUETTAVA RUUDUT
        //Debug.Log(" EULER " + euler);
        //Debug.Log(" ROTATION " + myCar.transform.rotation);
       
        yield return new WaitForSeconds(1.5f);
    }

    IEnumerator DrawObject(int i, int j)
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
        yield return new WaitForSeconds(0.5f);
        // return; // TEST
    }
}
