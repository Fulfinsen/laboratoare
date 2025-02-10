using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class timer : MonoBehaviour
{

    private Animator animator;
    private float timp = 0f;
    public float switchTime = 3f; // Time between animations

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timp += Time.deltaTime;

        animator.SetFloat("timePassed", timp);

        if (timp >= switchTime)
        {
            // Update the TimePassed parameter to switch animations
            
            timp = 0f; // Reset the timer
            animator.SetFloat("timePassed", timp);

            animator.SetTrigger("ResetCycle");
        }

        Debug.Log($"Timer: {timp}, TimePassed: {animator.GetFloat("timePassed")}");
    }
}
