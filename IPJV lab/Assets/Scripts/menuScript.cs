using UnityEngine;

public class menuScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction = Vector3.right;
    bool canChangeDirection = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Current X Position: " + transform.position.x);
        //Debug.Log("Current Y Position: " + transform.position.y);

        if (transform.position.x > 500f && !canChangeDirection)
        {
            canChangeDirection = true;
            direction = Vector3.down;
        }

        canChangeDirection = false;

        if (transform.position.y < 300f && !canChangeDirection)
        {
            canChangeDirection = true;
            direction = Vector3.left;
        }

        canChangeDirection = false;

        if (transform.position.x < 433f && !canChangeDirection)
        {
            canChangeDirection = true;
            direction = Vector3.up;
        }

        canChangeDirection = false;

        if (transform.position.y > 464f && !canChangeDirection)
        {
            canChangeDirection = true;
            direction = Vector3.right;
        }

        transform.Translate(direction * speed * Time.deltaTime);

        
    }
}
