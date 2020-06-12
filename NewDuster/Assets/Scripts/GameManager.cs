using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
    //private BoardManager boardScript;                        //Store a reference to our BoardManager which will set up the level.
    private int level = 3;                                    //Current level number, expressed in game as "Day 1".

    public int score = 0;
    public int Score { get { return score; } }
    public string player = "";

    Scene flappy1;
    
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        flappy1 = SceneManager.GetSceneByName("FlappyCar1");
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        //boardScript.SetupScene(level);

    }



    //Update is called every frame.
    void Update()
    {
       // nothing to do
    }

    public void Play()
    {
        SceneManager.LoadScene("MainSkene");
    }

    public void FlappyCar1()
    {
        //SceneManager.LoadScene
        SceneManager.LoadScene("FlappyCar1");
        SceneManager.SetActiveScene(flappy1);
    }

    public void FlappyCar2()
    {
        SceneManager.LoadScene("FlappyCar2");
    }

    public void FlappyCar3()
    {
        SceneManager.LoadScene("FlappyCar3");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }



    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}




