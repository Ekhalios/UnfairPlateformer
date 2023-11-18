using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;  // R�f�rence au transform du joueur
    public float smoothSpeed = 0.975f;  // Vitesse de suivi de la cam�ra
    public float DistanceY = 3.0f;

    void LateUpdate()
    {
        if (target != null && target.position.x >= 8.5f)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, DistanceY, transform.position.z);
            transform.position = desiredPosition;
        }
    }
}
