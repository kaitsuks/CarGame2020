using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

namespace KaitsuCar
{
    public class KKSGameControl : MonoBehaviour
    {
        public static KKSGameControl instance;            //A reference to our game control script so we can access it statically.
        public Text scoreText;                        //A reference to the UI text component that displays the player's score.
        public Text conditionText;
        public GameObject gameOvertext;                //A reference to the object that displays the text which appears when the player dies.
        public GameObject car;
        private Rigidbody2D carRB;
        private int score = 0;                        //The player's score.
        private int condition = 100;
        public bool gameOver = false;                //Is the game over?
        public float scrollSpeed = -1.5f;


        void Awake()
        {
            //If we don't currently have a game control...
            if (instance == null)
                //...set this one to be it...
                instance = this;
            //...otherwise...
            else if (instance != this)
                //...destroy this one because it is a duplicate.
                Destroy(gameObject);
            carRB = car.GetComponent<Rigidbody2D>();
            score = GameManager.instance.score;
            scoreText.text = "Score: " + score.ToString();
        }

        void Update()
        {
            //If the game is over and the player has pressed some input...
            if (gameOver && Input.GetMouseButtonDown(0))
            {
                //...reload the current scene.
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void AddDiamonds()
        {
            //The bird can't score if the game is over.
            if (gameOver)
                return;
            //If the game is not over, increase the score...
            score = GameManager.instance.score;
            score++;
            GameManager.instance.score = score;
            //...and adjust the score text.
            scoreText.text = "Score: " + score.ToString();
        }

        public void CarDestroyed()
        {
            //Activate the game over text.
            gameOvertext.SetActive(true);
            //Set the game to be over.
            gameOver = true;
            carRB.velocity = Vector2.zero;
            scrollSpeed = 0f;
        }

        public void CarCondition()
        {
            condition--;
            //...and adjust the condition text.
            conditionText.text = "Condition: " + condition.ToString();
        }

        //public void Play()
        //{
        //    SceneManager.LoadScene("MainSkene");
        //}

        //public void FlappyCar1()
        //{
        //    SceneManager.LoadScene("FlappyCar1");
        //}

        //public void FlappyCar2()
        //{
        //    SceneManager.LoadScene("FlappyCar2");
        //}

        //public void Settings()
        //{
        //    SceneManager.LoadScene("Settings");
        //}

        //public void Quit()
        //{
        //    Application.Quit();
        //}



        //public void Instructions()
        //{
        //    SceneManager.LoadScene("Instructions");
        //}
    }

}


