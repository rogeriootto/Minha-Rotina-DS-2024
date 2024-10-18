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
        filename = Path.Combine(Application.persistentDataPath, "results.csv");
        WriteFile(levelData);
    }

     public void WriteFile(ArrayList levelData)
    {
        // Verifica se o arquivo já existe
        bool fileExists = File.Exists(filename);
        
        // Se o arquivo não existir, crie-o e escreva o cabeçalho
        if (!fileExists)
        {
            using (TextWriter tw = new StreamWriter(filename, false))
            {
                tw.WriteLine("Horario; Nivel; Acertos; Erros; Tentativas; Dicas; Tempo; Musica (Pausas); Musica (Retomadas)");
            }
        }

        // Adiciona os dados ao final do arquivo
        using (TextWriter tw = new StreamWriter(filename, true))
        {
            foreach (var x in levelData) {
                tw.WriteLine(System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ";" +
                ((Dictionary<string, int>)x)["level"] + ";" + 
                ((Dictionary<string, int>)x)["howManyCorrect"] + ";" +
                ((Dictionary<string, int>)x)["howManyWrong"] + ";" +
                ((Dictionary<string, int>)x)["howManyTries"] + ";" +
                ((Dictionary<string, int>)x)["howManyHints"] + ";" +
                ((Dictionary<string, int>)x)["timeSpent"] + ";" +
                ((Dictionary<string, int>)x)["howManyMusicPause"] + ";" +
                ((Dictionary<string, int>)x)["howManyMusicUnPause"]);
            }
        }
    }
}