using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -10);
    }
}
