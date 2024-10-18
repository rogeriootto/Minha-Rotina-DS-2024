using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using NativeFilePickerNamespace; // Certifique-se de que você tem essa namespace da biblioteca

public class ExportCSV : MonoBehaviour
{
    string csvFilePath = "";
    public Button exportButton;

    void Start()
    {
        // Defina o caminho do arquivo CSV salvo
        csvFilePath = Path.Combine(Application.persistentDataPath, "results.csv");

        // Verifica se o botão está configurado e adiciona o listener
        if (exportButton != null)
        {
            exportButton.onClick.AddListener(ExportCSVToUser);
        }
    }

    public void ExportCSVToUser()
    {
        // Verifica se o arquivo CSV existe
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found!");
            return;
        }

        // Chama o Native File Picker para exportar o arquivo CSV
        NativeFilePicker.ExportFile(csvFilePath, (success) =>
        {
            if (success)
            {
                Debug.Log("CSV exported successfully.");
            }
            else
            {
                Debug.LogError("Failed to export CSV.");
            }
        });
    }
}
