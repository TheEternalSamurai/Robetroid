using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public float minCameraPosX;

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosX, Mathf.Infinity), transform.position.y, transform.position.z);
    }
}
