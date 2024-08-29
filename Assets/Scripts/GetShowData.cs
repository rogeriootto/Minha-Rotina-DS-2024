using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;

public class GetShowData : MonoBehaviour
{
    public GameObject textMesh;
    private string filename;
    public GameObject location;
    private string finalArray = "";

    // Start is called before the first frame update
    void Start()
    {
        filename = Path.Combine(Application.persistentDataPath, "results.csv");
        location.GetComponent<TextMeshProUGUI>().text = "Csv está localizado em:\n" + Path.Combine(Application.persistentDataPath, "results.csv");

        if (!File.Exists(filename))
        {
            textMesh.GetComponent<TextMeshProUGUI>().text = "Sem resultados para mostrar :(";
            return;
        }

        // Ler as linhas do arquivo CSV
        string[] lines = File.ReadAllLines(filename);

        // Ignorar a primeira linha que contém o cabeçalho
        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(';');
            finalArray += "Horário: " + columns[0] + "\n";
            finalArray += "Level: " + columns[1] + "\n";
            finalArray += "Acertos: " + columns[2] + "\n";
            finalArray += "Erros: " + columns[3] + "\n";
            finalArray += "Tentativas: " + columns[4] + "\n";
            finalArray += "Dicas: " + columns[5] + "\n";
            finalArray += "Tempo: " + columns[6] + " segundos\n\n";
        }

        // Exibir os dados no TextMeshProUGUI
        textMesh.GetComponent<TextMeshProUGUI>().text = finalArray;
    }
}
