using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GetShowData : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject textMesh;
    private ArrayList levelData;
    private string finalArray = "";

    // Start is called before the first frame update
    void Start()
    {
        var gm = gameManager.GetComponent<GameManager>();
        levelData = gm.getLevelsData();
        Debug.Log("levelData: " + levelData);
        Debug.Log("levelData.Count: " + levelData.Count);
        if (levelData.Count == 0) {
            textMesh.GetComponent<TextMeshProUGUI>().text = "Sem resultados para mostar :(";
        } else {
            foreach( var x in levelData) {
                finalArray += "Level " + ((Dictionary<string, int>)x)["level"] + ":\n";
                finalArray += "Acertos: " + ((Dictionary<string, int>)x)["howManyCorrect"] + "\n";
                finalArray += "Erros: " + ((Dictionary<string, int>)x)["howManyWrong"] + "\n";
                finalArray += "Tentativas: " + ((Dictionary<string, int>)x)["howManyTries"] + "\n";
                finalArray += "Número de Dicas usadas: " + ((Dictionary<string, int>)x)["howManyHints"] + "\n";
                finalArray += "Tempo gasto: " + ((Dictionary<string, int>)x)["timeSpent"] + " segundos\n\n";
            }
            textMesh.GetComponent<TextMeshProUGUI>().text = finalArray;
        }
    }
}
