using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MovementSettings")]
    [SerializeField] [Range(0.1f,10)] private float movementSpeed;
    [SerializeField] [Range(1.1f, 5f)] private float sprintStrenght;
    [SerializeField] [Range(0.1f, 10)] private float turnRate;
    [Header("StaminaSettings")]
    [SerializeField] [Range(0.1f, 10)] private float staminaGainRate;
    [SerializeField] [Range(0.1f, 10)] private float staminaUseRate;
    [SerializeField] private float stamina = 100f;
    private bool isRunning = false;
    private Vector2 currentDirection;
    private float horizontal, vertical;
    void Update()
    {
        Movement();

        currentDirection = new Vector2(horizontal, vertical).normalized;

        if (currentDirection != Vector2.zero)
        {
            Debug.DrawRay(transform.position, currentDirection * 100, Color.red);
            Debug.Log("stuffs happening");
        }
    }

    private void Movement()
    {
        float movementStrenght = Time.deltaTime * movementSpeed * (isRunning? sprintStrenght : 1f);
        float turnStrenght = Time.deltaTime * turnRate * (isRunning? sprintStrenght : 1f);

        horizontal = Mathf.Lerp(horizontal, Input.GetAxis("Horizontal") * movementStrenght, turnStrenght);

        vertical = Mathf.Lerp(vertical, Input.GetAxis("Vertical") * movementStrenght, turnStrenght);

        transform.position += new Vector3(horizontal ,0 , vertical);
    }
}