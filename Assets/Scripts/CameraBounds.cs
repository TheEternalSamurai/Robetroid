using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBounds : MonoBehaviour
{
    public GameObject gameCamera;

    private void OnBecameVisible()
    {
        if (Camera.current.CompareTag("MainCamera"))
        {
            Debug.Log("BossBounds");
            CinemachineVirtualCamera cineCamera = gameCamera.GetComponent<CinemachineVirtualCamera>();
            cineCamera.enabled = false;
        }
    }
}
