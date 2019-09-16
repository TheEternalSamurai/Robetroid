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
            CinemachineVirtualCamera cineCamera = gameCamera.GetComponent<CinemachineVirtualCamera>();
            cineCamera.enabled = false;

            gameObject.GetComponent<PatrolAir>().enabled = true;
        }
    }
}
