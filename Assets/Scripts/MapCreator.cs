using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public enum blocType
{
    VOID,
    GROUNDTOP,
    GROUNDBOT,
    FAKEGROUND,
    SPIKE,
    FLAG
}

public class MapCreator : MonoBehaviour
{
    public blocType[,] array = new blocType[100, 15];
    public GameObject player;
    public GameObject prefabGroundTop;
    public GameObject prefabGroundBot;
    public GameObject prefabFakeGround;
    public GameObject prefabSpike;
    public GameObject prefabFlag;
    public RectTransform panelRectTransform;
    public string mapName;

    private bool editorMode = false;
    private bool destroyMode = false;
    private GameObject selectedPrefab;
    private blocType selectedPrefabType;
    private bool drawing = false;
    private string privatePath;
    private string filePath;

    void Start()
    {
        privatePath = Application.streamingAssetsPath + "/Maps/";
        LoadMap(mapName);
        drawMap();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && editorMode)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, Input.mousePosition))
            {
                drawing = false;
                return;
            }
            drawing = true;
        }
        if (Input.GetMouseButtonUp(0) || !editorMode)
        {
            drawing = false;
        }
        if (drawing)
        {
            if (destroyMode)
            {
                Vector2 clicPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(clicPosition, Vector2.zero);

                if (hit.collider != null)
                {
                    GameObject objetTouche = hit.collider.gameObject;
                    if (objetTouche.tag == ("Destroyable") || objetTouche.tag == ("killPlayer"))
                    {
                        Destroy(objetTouche);
                        int posX1 = 0;
                        int posY1 = 0;
                        if (clicPosition.x >= 0)
                        {
                            posX1 = (int)(clicPosition.x + 0.5);
                        }
                        else
                        {
                            posX1 = (int)(clicPosition.x - 0.5);
                        }
                        if (clicPosition.y >= 0)
                        {
                            posY1 = (int)(clicPosition.y + 0.5);
                        }
                        else
                        {
                            posY1 = (int)(clicPosition.y - 0.5);
                        }
                        if (posX1 < 0 || posY1 < 0) { return; }
                        array[posX1, posY1] = blocType.VOID;
                    }
                }
                return;
            }
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePos.x + ", " + mousePos.y);
            Debug.Log((int)mousePos.x + ", " + (int)mousePos.y);
            int posX = 0; 
            int posY = 0;
            if (mousePos.x >= 0)
            {
                posX = (int)(mousePos.x + 0.5);
            } else
            {
                posX = (int)(mousePos.x - 0.5);
            }
            if (mousePos.y >= 0)
            {
                posY = (int)(mousePos.y + 0.5);
            }
            else
            {
                posY = (int)(mousePos.y - 0.5);
            }
            if (posX < 0 || posY < 0) { return; }
            array[posX, posY] = selectedPrefabType;
            Vector2 clicPosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit1 = Physics2D.Raycast(clicPosition1, Vector2.zero);

            if (hit1.collider != null)
            {
                GameObject objetTouche1 = hit1.collider.gameObject;
                if (objetTouche1.tag == ("Destroyable") || objetTouche1.tag == ("killPlayer"))
                {
                    Destroy(objetTouche1);
                }
            }
            Instantiate(selectedPrefab, new Vector3(posX, posY, -1), Quaternion.identity);
        }
    }

    public void switchEditor()
    {
        editorMode = !editorMode;
        if (editorMode)
        {
            drawMap();
        }
    }

    public bool getEditorMode()
    {
        return editorMode;
    }

    public void switchDestroyMode()
    {
        destroyMode = true;
    }

    public void switchPrefab(GameObject prefab)
    {
        destroyMode = false;
        selectedPrefab = prefab;
        selectedPrefabType = GetBlocTypeForPrefab(prefab);
    }

    private blocType GetBlocTypeForPrefab(GameObject prefab)
    {
        if (prefab == prefabGroundTop)
        {
            return blocType.GROUNDTOP;
        }
        else if (prefab == prefabFakeGround)
        {
            return blocType.FAKEGROUND;
        }
        else if (prefab == prefabSpike)
        {
            return blocType.SPIKE;
        }
        else if (prefab == prefabGroundBot)
        {
            return blocType.GROUNDBOT;
        }
        else if (prefab == prefabFlag)
        {
            return blocType.FLAG;
        }
        else
        {
            return blocType.GROUNDTOP;
        }
    }
    
    public void drawMap()
    {
        GameObject[] destroyable = GameObject.FindGameObjectsWithTag("Destroyable");
        foreach (GameObject obj in destroyable)
        {
                Destroy(obj);
        }
        GameObject[] KillPlayers = GameObject.FindGameObjectsWithTag("killPlayer");
        foreach (GameObject obj in KillPlayers)
        {
            Destroy(obj);
        }

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (array[x, y] == blocType.GROUNDTOP)
                {
                    Instantiate(prefabGroundTop, new Vector3(x, y, -1), Quaternion.identity);
                }
                if (array[x, y] == blocType.GROUNDBOT)
                {
                    Instantiate(prefabGroundBot, new Vector3(x, y, -1), Quaternion.identity);
                }
                if (array[x, y] == blocType.FAKEGROUND)
                {
                    Instantiate(prefabFakeGround, new Vector3(x, y, -1), Quaternion.identity);
                }
                if (array[x, y] == blocType.SPIKE)
                {
                    Instantiate(prefabSpike, new Vector3(x, y, -1), Quaternion.identity);
                }
                if (array[x, y] == blocType.FLAG)
                {
                    Instantiate(prefabFlag, new Vector3(x, y, -1), Quaternion.identity);
                }
            }
        }
    }

    public void SaveMap(TMP_InputField str)
    {
        filePath = privatePath + str.text;
        Debug.Log(filePath);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Create);

        List<List<int>> serializableArray = new List<List<int>>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                row.Add((int)array[i, j]);
            }
            serializableArray.Add(row);
        }

        formatter.Serialize(fileStream, serializableArray);
        fileStream.Close();

        Debug.Log("Tableau sauvegard� !");
    }

    public void LoadMap(string MapName)
    {
        filePath = privatePath + MapName;
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            List<List<int>> serializableArray = (List<List<int>>)formatter.Deserialize(fileStream);
            fileStream.Close();

            for (int i = 0; i < serializableArray.Count; i++)
            {
                for (int j = 0; j < serializableArray[i].Count; j++)
                {
                    array[i, j] = (blocType)serializableArray[i][j];
                }
            }

            Debug.Log("Tableau charg� !");
        }
        else
        {
            Debug.LogWarning("Aucun fichier de sauvegarde trouv�.");
        }
    }
} 
