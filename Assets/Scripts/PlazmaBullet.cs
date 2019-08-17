using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazmaBullet : MonoBehaviour
{
    public float speed = 20f;

    private Rigidbody2D rigBody;

    private void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        rigBody.velocity = transform.right * speed;
    }
}
