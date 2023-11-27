using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MapSelector : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button fileButtonPrefab;

    void Start()
    {
        PopulateFileList();
    }

    void PopulateFileList()
    {
        string streamingAssetsPath = Application.streamingAssetsPath + "/Maps/";
        string[] files = Directory.GetFiles(streamingAssetsPath);
        int pos = 0;

        for (int i = 0; i < files.Length; i++)
        {
            string filePath = files[i];
            string fileName = Path.GetFileName(filePath);
            if (fileName.EndsWith(".meta"))
            {
                continue;
            }
            Button fileButton = Instantiate(fileButtonPrefab, scrollRect.content);
            fileButton.GetComponentInChildren<TextMeshProUGUI>().text = fileName;

            // Ajustez la position y du bouton en fonction de l'index de la boucle
            RectTransform buttonTransform = fileButton.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = new Vector2(0, -pos * buttonTransform.rect.height);
            pos++;
            fileButton.onClick.AddListener(() => OnFileButtonClick(fileName));
        }
    }

    void OnFileButtonClick(string fileName)
    {
        // Gérer le clic sur un fichier ici
        Debug.Log("Selected File: " + fileName);
    }
}
