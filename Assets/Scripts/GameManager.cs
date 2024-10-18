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
    public static int howManyMusicPause;
    public static int howManyMusicUnPause;
    private static bool alreadyPlayedParticle = false;
    public static ArrayList levelsData = new ArrayList();
    private static Dictionary<string, int> thisLevelData = new Dictionary<string, int>();
    public static bool isArmarioSet = false;
    public static bool isCriancaSet = false;
    public static bool isVasoSet = false;
    public static bool isCountDownOver = false;
    private static float countDown = 5;
    
    public GameObject timer;

    public GameObject conteudo;

    private static bool isGaming = false;

    void Start() {
        howManyCorrect = 0;
        howManyWrong = 0;
        howManyTries = 0;
        howManyHints = 0;
        timeSpent = 0;
        hintTimeSpent = 0;
        howManyMusicPause = 0;
        howManyMusicUnPause = 0;
        alreadyPlayedParticle = false;
        thisLevelData = new Dictionary<string, int>();
        isArmarioSet = false;
        isCriancaSet = false;
        isVasoSet = false;
        isCountDownOver = false;
        countDown = 5;
    }

    void Update() {
       if (isGaming) {
            if (isCountDownOver) {
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
            else {
                countDown -= 1 * Time.deltaTime;
                timer.GetComponent<TMPro.TextMeshProUGUI>().text = countDown.ToString("0");

                if (countDown <= 0.9) {
                    timer.SetActive(false);
                    isCountDownOver = true;
                    conteudo.SetActive(true);
                }
            }
       }
    }

    private void generateHint() {
        if (!isArmarioSet) {
            GameObject.FindWithTag("armario-certo").GetComponent<DragDrop>().hint = true;
            return;
        }
        if (!isCriancaSet) {
            GameObject.FindWithTag("criança-certo").GetComponent<DragDrop>().hint = true;
            return;
        }
        if (!isVasoSet) {
            GameObject.FindWithTag("vaso-certo").GetComponent<DragDrop>().hint = true;
            return;
        }   
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
        thisLevelData.Add("howManyMusicPause", howManyMusicPause);
        thisLevelData.Add("howManyMusicUnPause", howManyMusicUnPause);
        levelsData.Add(thisLevelData);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

     public void goToFirstLevel(){
        isGaming = true;
        levelsData = new ArrayList();
        SceneManager.LoadScene(1);
    }

    public void goToMainMenu(){
        isGaming = false;
        SceneManager.LoadScene(0);
    }

}
