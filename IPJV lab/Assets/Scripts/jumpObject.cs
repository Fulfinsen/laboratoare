using Unity.Cinemachine;
using UnityEngine;

public class jumpObject : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxHeight;
    [SerializeField] float minHeight;

    float direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > maxHeight || transform.position.y < minHeight)
        {
            direction *= -1f;
        }

        transform.Translate(Vector3.up * speed * Time.deltaTime * direction, Space.World);
    }
}
