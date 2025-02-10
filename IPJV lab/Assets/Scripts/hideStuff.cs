using UnityEngine;

public class hideStuff : MonoBehaviour
{

    [SerializeField] GameObject house1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        house1.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
