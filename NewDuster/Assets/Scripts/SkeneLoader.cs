using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeneLoader : MonoBehaviour
{

    public void LoadSceneOnClick(int sceneNo)
    {
       SceneManager.LoadScene(sceneNo);
    }

}