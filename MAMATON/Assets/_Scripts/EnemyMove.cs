using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 3f; // Enemy movement speed
    private bool hasContactedPlayer = false;

    void Update()
    {
        if (!hasContactedPlayer && PlayerMove.instance != null)
        {
            // Move towards the player's position, but keep y position fixed
            Vector3 targetPosition = PlayerMove.instance.transform.position;
            targetPosition.y = transform.position.y; // Freeze y axis
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the enemy's trigger contacts the player
        if (other.gameObject == PlayerMove.instance.gameObject)
        {
            hasContactedPlayer = true;
        }
    }
}
