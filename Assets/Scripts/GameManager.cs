using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winVFX;
    public static int howManyCorrect;
    public static int howManyWrong;
    public static int howManyTries;
    public static float timeSpent;
    public static float hintTimeSpent;
    public static int howManyHints;
    private static bool alreadyPlayedParticle = false;
    public static ArrayList levelsData = new ArrayList();
    private static Dictionary<string, int> thisLevelData = new Dictionary<string, int>();
    public static bool isArmarioSet = false;
    public static bool isCriancaSet = false;
    public static bool isVasoSet = false;

    void Start() {
        howManyCorrect = 0;
        howManyWrong = 0;
        howManyTries = 0;
        howManyHints = 0;
        timeSpent = 0;
        hintTimeSpent = 0;
        alreadyPlayedParticle = false;
        thisLevelData = new Dictionary<string, int>();
        isArmarioSet = false;
        isCriancaSet = false;
        isVasoSet = false;
    }

    void Update() {
        timeSpent += 1 * Time.deltaTime;
        hintTimeSpent += 1 * Time.deltaTime;

        if (hintTimeSpent > 60 && (!isArmarioSet || !isCriancaSet || !isVasoSet)) {
            hintTimeSpent = 0;
            generateHint();
        }

        if (howManyCorrect == 3) {
            if (!alreadyPlayedParticle) {
                alreadyPlayedParticle = true;
                Instantiate(winVFX, new Vector3(0, 5, 50), Quaternion.Euler(90, 0, 0));
            }
            StartCoroutine(WaitAndGoToNextLevel());
        }
    }

    private void generateHint() {
        if (!isArmarioSet) {
            GameObject.FindWithTag("armario").GetComponent<DragDrop>().hint = true;
            return;
        }
        if (!isCriancaSet) {
            GameObject.FindWithTag("criança").GetComponent<DragDrop>().hint = true;
            return;
        }
        if (!isVasoSet) {
            GameObject.FindWithTag("vaso").GetComponent<DragDrop>().hint = true;
            return;
        }   
    }

    public void goToFirstLevel(){
        levelsData = new ArrayList();
        SceneManager.LoadScene(1);
    }

    public void goToMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void goToResults(){
        SceneManager.LoadScene(5);
    }

    public ArrayList getLevelsData() {
        return levelsData;
    }

    private IEnumerator WaitAndGoToNextLevel() {
        yield return new WaitForSeconds(3);
        goToNextLevel();
    }

    public static void goToNextLevel() {
        thisLevelData.Add("level", SceneManager.GetActiveScene().buildIndex);
        thisLevelData.Add("howManyCorrect", howManyCorrect);
        thisLevelData.Add("howManyWrong", howManyWrong);
        thisLevelData.Add("howManyTries", howManyTries);
        thisLevelData.Add("howManyHints", howManyHints);
        thisLevelData.Add("timeSpent", (int)timeSpent);
        levelsData.Add(thisLevelData);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void doExitGame() {
        Application.Quit();
    }
}
