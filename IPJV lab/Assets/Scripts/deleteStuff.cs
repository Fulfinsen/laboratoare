using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteStuff : MonoBehaviour
{

    public GameObject bench;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(bench);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
