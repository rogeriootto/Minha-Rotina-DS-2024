using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void goToMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void goToResults(){
        SceneManager.LoadScene(5);
    }

    public void doExitGame() {
        Application.Quit();
    }
}
