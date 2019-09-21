using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraBounds : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject healthBarCanvas;
    public bool showHealthBar;

    private void OnBecameVisible()
    {
        if (Camera.current.CompareTag("MainCamera"))
        {
            CinemachineVirtualCamera cineCamera = gameCamera.GetComponent<CinemachineVirtualCamera>();
            cineCamera.enabled = false;

            gameObject.GetComponent<PatrolAir>().enabled = true;
            healthBarCanvas.SetActive(showHealthBar);
        }
    }
}
