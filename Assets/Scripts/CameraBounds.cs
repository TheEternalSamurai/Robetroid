using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBounds : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject bossConfiner;

    private void OnBecameVisible()
    {
        if (Camera.current.tag == "MainCamera")
        {
            CinemachineConfiner confiner = gameCamera.GetComponent<CinemachineConfiner>();
            confiner.m_BoundingShape2D = bossConfiner.GetComponent<Collider2D>();
        }
    }
}
