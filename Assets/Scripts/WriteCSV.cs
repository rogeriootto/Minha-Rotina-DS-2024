using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteCSV : MonoBehaviour
{
    string filename = "";
    public GameObject gameManager;

    void Start() {
        var gm = gameManager.GetComponent<GameManager>();
        var levelData = gm.getLevelsData();
        filename = Application.dataPath + "/results.csv";
        WriteFile(levelData);
    }

    public void WriteFile(ArrayList levelData)
    {
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("Horario; Nivel; Acertos; Erros; Tentativas; Dicas; Tempo");
        tw.Close();

        tw = new StreamWriter(filename, true);

        foreach( var x in levelData) {
            tw.WriteLine(System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ";" +
            ((Dictionary<string, int>)x)["level"] + ";" + 
            ((Dictionary<string, int>)x)["howManyCorrect"] + ";" +
            ((Dictionary<string, int>)x)["howManyWrong"] + ";" +
            ((Dictionary<string, int>)x)["howManyTries"] + ";" +
            ((Dictionary<string, int>)x)["howManyHints"] + ";" +
            ((Dictionary<string, int>)x)["timeSpent"]);
        }
        tw.Close();
    }
}