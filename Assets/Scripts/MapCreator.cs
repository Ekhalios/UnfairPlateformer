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
    public bool editorMode = true;

    private int groundLevel = 0;

    void Start()
    {
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
        player.transform.position = new Vector2(15, (groundLevel + 1));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && editorMode)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePos.x + ", " + mousePos.y);
            Debug.Log((int)mousePos.x + ", " + (int)mousePos.y);
            Instantiate(prefabGroundTop, new Vector3((int)(mousePos.x + 0.5), (int)(mousePos.y + 0.5), 0), Quaternion.identity);
        }
    }
} 
