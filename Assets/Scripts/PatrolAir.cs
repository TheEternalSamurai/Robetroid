using UnityEngine;

public class PatrolAir : MonoBehaviour
{
    public float speed;
    public Transform[] moveSpots;

    private int moveSpotIndex = 0;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[moveSpotIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[moveSpotIndex].position) < .2f)
            moveSpotIndex = (moveSpotIndex + 1) % moveSpots.Length;
    }
}
