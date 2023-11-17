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
        array[8, groundLevel] = blocType.FAKEGROUND;
        array[9, groundLevel] = blocType.FAKEGROUND;
        array[10, groundLevel + 1] = blocType.SPIKE;
        array[11, groundLevel + 1] = blocType.SPIKE;



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
        player.transform.position = new Vector2(50, (groundLevel + 1));
    }
}
