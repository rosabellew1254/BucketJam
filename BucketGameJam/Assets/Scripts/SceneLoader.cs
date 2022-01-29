using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoToScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    } 
}
