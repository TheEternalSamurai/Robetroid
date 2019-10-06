using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private float playerWidth;
    private float playerHeight;

    void Start()
    {
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        playerHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        Vector2 rightCamBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, Camera.main.transform.position.z));
        Vector2 leftCamBound = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.position.z));
        viewPos.x = Mathf.Clamp(viewPos.x, leftCamBound.x + playerWidth, rightCamBound.x - playerWidth);
        Debug.Log(transform.position);
        transform.position = viewPos;
    }
}
