using UnityEngine;

public class playerStop : MonoBehaviour
{
    [SerializeField] QTETest qucikTimeEvent;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");
            // You can stop the player or do other actions here
            // Example: stop movement script
            PlayerMove playerMove = other.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                playerMove.enabled = false; // disables the PlayerMove script
                qucikTimeEvent.Show();
            }
        }
    }
}
