using UnityEngine;

public class spawnObiecte : MonoBehaviour
{

    [SerializeField] GameObject prefabScale;
    [SerializeField] GameObject prefabRotate;
    [SerializeField] GameObject prefabJump;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int randomObject = Random.Range(0, 3);
                if(randomObject == 0)
                {
                    GameObject scale = Instantiate(prefabScale, transform);
                    scale.transform.localPosition = new Vector3(i, 0, j);
                    GameObject rotate = Instantiate(prefabRotate, transform);
                    rotate.transform.localPosition = new Vector3(i, 0, j);
                    GameObject jump = Instantiate(prefabJump, transform);
                    jump.transform.localPosition = new Vector3(i, 0, j);
                }
                
            }
        }
    }
}
