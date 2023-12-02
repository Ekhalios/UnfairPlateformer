using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class MapSelector : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button fileButtonPrefab;
    public float maxY = 90; // La limite maximale vers le haut
    public float minY = -100f;
    int nbrButton = 0;

    private string fileSelected;
    private List<Button> fileButtons = new List<Button>();
    private float heightButton = 0;

    void Start()
    {
        heightButton = fileButtonPrefab.GetComponent<RectTransform>().rect.height;
        scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, 365);
        PopulateFileList();
        string mapFileName = PlayerPrefs.GetString("MapFileName", "DefaultMapConfig");
    }

    void Update()
    {
        /*minY = nbrButton * heightButton + 90;

        // Récupération de la position y actuelle du contenu de la Scroll View
        float currentY = scrollRect.content.anchoredPosition.y;

        if (currentY > maxY)
        {
            scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, maxY);
        }
        if (currentY < minY)
        {
            scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, minY);
        }*/
    }
    void PopulateFileList()
    {
        string streamingAssetsPath = Application.streamingAssetsPath + "/CustomMaps/";
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
            buttonTransform.anchoredPosition = new Vector2(0, -pos * buttonTransform.rect.height + 90);
            fileButtons.Add(fileButton);
            pos++;
            nbrButton++;
            fileButton.onClick.AddListener(() => OnFileButtonClick(fileName));
        }
    }

    void OnFileButtonClick(string fileName)
    {
        fileSelected = fileName;
    }

    public void DeleteFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath + "/CustomMaps/", fileSelected);

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                File.Delete(filePath + ".meta");
                Button buttonToRemove = fileButtons.Find(button => button.GetComponentInChildren<TextMeshProUGUI>().text == fileSelected);
                if (buttonToRemove != null)
                {
                    fileButtons.Remove(buttonToRemove);
                    Destroy(buttonToRemove.gameObject);
                }
                nbrButton--;
                fileSelected = "";
                Debug.Log("Fichier supprimé avec succès : " + fileSelected);
            }
            else
            {
                Debug.LogWarning("Le fichier n'existe pas : " + fileSelected);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erreur lors de la suppression du fichier " + fileSelected + ": " + e.Message);
        }
    }

    public void switchScene()
    {
        PlayerPrefs.SetString("MapFileName", "/CustomMaps/" + fileSelected);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }

    public string getfileSelected()
    {
        return fileSelected;
    }

    public void switchNewMap()
    {
        PlayerPrefs.SetString("MapFileName", "/Maps/Level0");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}
