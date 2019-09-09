using System.Collections;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public GameObject plazmaBullet;
    public Transform firePoint;
    public int bulletsShotPerRound;
    public int shootWaitTime;

    private float timeSinceLastShot = 0f;

    private void Update()
    {
        if (timeSinceLastShot >= shootWaitTime)
        {
            StartCoroutine(Shoot());
            timeSinceLastShot = 0f;
        }

        timeSinceLastShot += Time.deltaTime;
    }

    private IEnumerator Shoot()
    {
        for (int i = 0; i < bulletsShotPerRound; i++)
        {
            Instantiate(plazmaBullet, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(.25f);
        }
    }
}