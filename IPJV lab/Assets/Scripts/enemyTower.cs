using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class enemyTower : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Ensure the tower reacts to collision events
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with the tower!");
        }
    }
}


