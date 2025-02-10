using UnityEngine;

public class collisionEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You are close");
    }
}
