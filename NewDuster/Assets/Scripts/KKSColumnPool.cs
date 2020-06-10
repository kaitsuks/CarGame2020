using UnityEngine;
using System.Collections;

namespace KaitsuCar
{
    //voi käyttää myös: obs = GameObject.FindObjectsWithTag();
    //ja foreach(GameObject go in obs) 
    //{
    // go.GetComponent<Component>()...do something
    // Destroy(go); //esim
    //}



    //void Greet()
    //{//ehtolauseiden lisäksi switch case
    //public int intelligence = 5;
    //    switch (intelligence)
    //    {
    //        case 5:
    //            print("Why hello there good sir! Let me teach you about Trigonometry!");
    //            break;
    //        case 4:
    //            print("Hello and good day!");
    //            break;
    //        case 3:
    //            print("Whadya want?");
    //            break;
    //        case 2:
    //            print("Grog SMASH!");
    //            break;
    //        case 1:
    //            print("Ulg, glib, Pblblblblb");
    //            break;
    //        default:
    //            print("Incorrect intelligence level.");
    //            break;
    //    }

    public class KKSColumnPool : MonoBehaviour
    {
        public GameObject columnPrefab;                                    //The column game object.
        public GameObject column2Prefab;
        public int columnPoolSize = 5;                                    //How many columns to keep on standby.
        private int column2PoolSize = 5;
        public GameObject[] columns2orig;
        public float spawnRate = 3f;                                    //How quickly columns spawn.
        public float columnMin = -1f;                                    //Minimum y value of the column position.
        public float columnMax = 3.5f;                                    //Maximum y value of the column position.
        public float column2Min = -1f;                                    //Minimum y value of the column position.
        public float column2Max = 1.5f;
        private GameObject[] columns;                                    //Collection of pooled columns.
        private int currentColumn = 0;                                    //Index of the current column in the collection.
        private GameObject[] columns2;                                    //Collection of pooled columns.
        private int current2Column = 0;                                    //Index of the current column in the collection.

        private Vector2 objectPoolPosition = new Vector2(25, 0);        //A holding position for our unused columns offscreen.
        private float spawnXPosition = 10f;

        private float timeSinceLastSpawned;

        private Vector2 objectPool2Position = new Vector2(35, 0);        //A holding position for our unused columns offscreen.
        private float spawnX2Position = 10f;

        private float time2SinceLastSpawned;


        void Start()
        {
            timeSinceLastSpawned = 0f;
            time2SinceLastSpawned = 0f;

            //Initialize the columns collection.
            columns = new GameObject[columnPoolSize];
            //Loop through the collection... 
            for (int i = 0; i < columnPoolSize; i++)
            {
                //...and create the individual columns.
                columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
            }

            column2PoolSize =columns2orig.Length;
            Debug.Log("column2PoolSize " + column2PoolSize);

            //Initialize the columns collection.
            columns2 = new GameObject[column2PoolSize];
            //Loop through the collection... 
            for (int i = 0; i < column2PoolSize; i++)
            {
                //...and create the individual columns.
                columns2[i] = (GameObject)Instantiate(columns2orig[i], objectPool2Position, Quaternion.identity);
            }
        }


        //This spawns columns as long as the game is not over.
        void Update()
        {
            timeSinceLastSpawned += Time.deltaTime;

            if (KKSGameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0f;

                //Set a random y position for the column
                float spawnYPosition = Random.Range(columnMin, columnMax);

                //...then set the current column to that position.
                columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);

                //Increase the value of currentColumn. If the new size is too big, set it back to zero
                currentColumn++;

                if (currentColumn >= columnPoolSize)
                {
                    currentColumn = 0;
                    for(int i = 0; i < columnPoolSize; i++ )
                    {
                        columns[i].GetComponent<KKSColumn>().bCollided = false;
                        columns[i].GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }

            //POOL2
            time2SinceLastSpawned += Time.deltaTime;

            if (KKSGameControl.instance.gameOver == false && time2SinceLastSpawned >= spawnRate)
            {
                time2SinceLastSpawned = 0f;

                //Set a random y position for the column
                float spawnYPosition = Random.Range(column2Min, column2Max);

                //...then set the current column to that position.
                columns2[current2Column].transform.position = new Vector2(spawnX2Position, spawnYPosition);
                columns2[current2Column].GetComponent<SpriteRenderer>().sortingOrder = 20 - (int)spawnYPosition * 5;  //new Vector2(spawnX2Position, spawnYPosition);
                if (spawnYPosition < 0f)
                {
                    columns2[current2Column].GetComponent<SpriteRenderer>().sortingOrder = 30;
                }
                //Increase the value of currentColumn. If the new size is too big, set it back to zero
                current2Column++;

                if (current2Column >= column2PoolSize)
                {
                    current2Column = 0;
                    for (int i = 0; i < column2PoolSize; i++)
                    {
                        columns2[i].GetComponent<KKSColumn>().bCollided = false;
                    }
                }
            }
        }
    }
}



