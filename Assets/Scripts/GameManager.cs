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
    public static int timeSpent;
    private static bool alreadyPlayedParticle = false;
    private static ArrayList levelsData = new ArrayList();
    private static Dictionary<string, int> thisLevelData = new Dictionary<string, int>();

    void Start() {
        howManyCorrect = 0;
        howManyWrong = 0;
        howManyTries = 0;
        timeSpent = 0;
        alreadyPlayedParticle = false;
        thisLevelData = new Dictionary<string, int>();
    }

    void Update() {
        timeSpent = (int)Time.time;

        if (howManyCorrect == 3) {
            if (!alreadyPlayedParticle) {
                alreadyPlayedParticle = true;
                Instantiate(winVFX, new Vector3(0, 5, 50), Quaternion.Euler(90, 0, 0));
            }
            StartCoroutine(WaitAndGoToNextLevel());
        }
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
        thisLevelData.Add("timeSpent", timeSpent);
        levelsData.Add(thisLevelData);
        foreach( var x in levelsData) {
            Debug.Log("level: " + ((Dictionary<string, int>)x)["level"]);
            Debug.Log("howManyCorrect: " + ((Dictionary<string, int>)x)["howManyCorrect"]);
            Debug.Log("howManyWrong: " + ((Dictionary<string, int>)x)["howManyWrong"]);
            Debug.Log("howManyTries: " + ((Dictionary<string, int>)x)["howManyTries"]);
            Debug.Log("timeSpent: " + ((Dictionary<string, int>)x)["timeSpent"]);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
