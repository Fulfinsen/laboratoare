using UnityEngine;
using System.Collections;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] float scaleSpeed;
    [SerializeField] float maxScale;
    [SerializeField] float minScale;

    private Vector3 initialScale;
    private bool isScalingUp = true;
    private bool isWaiting = false;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (!isWaiting)
        {
            if (isScalingUp)
            {
                transform.localScale += Vector3.one * (scaleSpeed * Time.deltaTime);
                if (transform.localScale.x >= maxScale)
                {
                    isScalingUp = false;
                    StartCoroutine(scalare());
                }
            }
            else
            {
                transform.localScale -= Vector3.one * (scaleSpeed * Time.deltaTime);
                if (transform.localScale.x <= minScale)
                {
                    isScalingUp = true;
                    StartCoroutine(scalare());
                }
            }
        }
    }

    IEnumerator scalare()
    {
        isWaiting = true;
        yield return new WaitForSeconds(3f);
        isWaiting = false;
    }
}
