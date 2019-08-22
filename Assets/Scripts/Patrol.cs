using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float groundDetectionDistance;
    public Transform groundCheck;

    private bool movingRight = true;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundHit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDetectionDistance, LayerMask.GetMask("Ground"));

        if (!groundHit.collider)
            FlipDirection();
    }

    private void FlipDirection()
    {
        transform.eulerAngles += new Vector3(0f, 180f, 0f);
        movingRight = !movingRight;
    }
}
