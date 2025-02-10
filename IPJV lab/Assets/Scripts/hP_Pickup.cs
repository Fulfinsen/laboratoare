using UnityEngine;

public class hP_Pickup : MonoBehaviour
{

    [SerializeField] float maxHealthIncreaseAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            characterMovement playerHealth = other.GetComponent<characterMovement>();

            if (playerHealth != null)
            {
                playerHealth.IncreaseMaxHealth(maxHealthIncreaseAmount);
                Destroy(gameObject); // Destroy the pick-up after use
            }
        }
    }
}
