using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads the game scene from initialization
    /// </summary>
    public void LoadGame(){
        SceneManager.LoadScene(1);
    }
}
