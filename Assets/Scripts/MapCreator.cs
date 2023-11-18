using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum blocType
{
    VOID,
    GROUNDTOP,
    GROUNDBOT,
    FAKEGROUND,
    SPIKE
}

public class MapCreator : MonoBehaviour
{
    public blocType[,] array = new blocType[100, 15];
    public GameObject player;
    public GameObject prefabGroundTop;
    public GameObject prefabGroundBot;
    public GameObject prefabFakeGround;
    public GameObject prefabSpike;
    public RectTransform panelRectTransform;

    private bool editorMode = false;
    private bool destroyMode = false;
    private int groundLevel = 0;
    private GameObject selectedPrefab;
    private blocType selectedPrefabType;

    void Start()
    {
        selectedPrefab = prefabGroundTop;
        selectedPrefabType = blocType.GROUNDTOP;
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                array[x, y] = blocType.VOID;
                if (y == groundLevel)
                {
                    array[x, y] = blocType.GROUNDTOP;
                } else if (y < groundLevel)
                {
                    array[x, y] = blocType.GROUNDBOT;
                }
            }
        }
        array[18, groundLevel] = blocType.FAKEGROUND;
        array[19, groundLevel] = blocType.FAKEGROUND;
        array[20, groundLevel + 1] = blocType.SPIKE;
        array[21, groundLevel + 1] = blocType.SPIKE;
        array[22, groundLevel] = blocType.FAKEGROUND;
        array[23, groundLevel] = blocType.FAKEGROUND;
        array[24, groundLevel + 2] = blocType.GROUNDTOP;
        array[25, groundLevel + 2] = blocType.GROUNDTOP;
        array[27, groundLevel + 1] = blocType.SPIKE;


        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (array[x, y] == blocType.GROUNDTOP)
                {
                    Instantiate(prefabGroundTop, new Vector3(x, y, 0), Quaternion.identity);
                }
                if (array[x, y] == blocType.GROUNDBOT)
                {
                    Instantiate(prefabGroundBot, new Vector3(x, y, 0), Quaternion.identity);
                }
                if (array[x, y] == blocType.FAKEGROUND)
                {
                    Instantiate(prefabFakeGround, new Vector3(x, y, 0), Quaternion.identity);
                }
                if (array[x, y] == blocType.SPIKE)
                {
                    Instantiate(prefabSpike, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && editorMode)
        {
            if (destroyMode)
            {
                Vector2 clicPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Collider2D hitCollider = Physics2D.OverlapPoint(clicPosition);

                if (hitCollider != null)
                {
                    GameObject objetTouche = hitCollider.gameObject;
                    if (objetTouche.tag == ("Destroyable") || objetTouche.tag == ("killPlayer"))
                    {
                        Destroy(objetTouche);
                    }
                }
                return;
            }
            if (RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, Input.mousePosition))
            {
                return;
            }
            Vector2 clicPosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hitCollider1 = Physics2D.OverlapPoint(clicPosition1);

            if (hitCollider1 != null)
            {
                GameObject objetTouche1 = hitCollider1.gameObject;
                if (objetTouche1.tag == ("Destroyable") || objetTouche1.tag == ("killPlayer"))
                {
                    Destroy(objetTouche1);
                }
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
            Instantiate(selectedPrefab, new Vector3(posX, posY, 0), Quaternion.identity);
        }
    }

    public void switchEditor()
    {
        editorMode = !editorMode;
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
        else
        {
            return blocType.GROUNDTOP;
        }
    }
    
    public void drawMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (array[x, y] == blocType.GROUNDTOP)
                {
                    Instantiate(prefabGroundTop, new Vector3(x, y, 0), Quaternion.identity);
                }
                if (array[x, y] == blocType.GROUNDBOT)
                {
                    Instantiate(prefabGroundBot, new Vector3(x, y, 0), Quaternion.identity);
                }
                if (array[x, y] == blocType.FAKEGROUND)
                {
                    Instantiate(prefabFakeGround, new Vector3(x, y, 0), Quaternion.identity);
                }
                if (array[x, y] == blocType.SPIKE)
                {
                    Instantiate(prefabSpike, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }
} 
