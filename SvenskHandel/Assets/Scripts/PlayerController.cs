using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("MovementSettings")]
    [SerializeField] [Range(0.1f,10)] private float movementSpeed;
    [SerializeField] [Range(1.1f, 5f)] private float sprintMultiplier;
    [SerializeField] [Range(0.1f, 10)] private float turnRate;

    [Header("StaminaSettings")]
    [SerializeField] [Range(0.1f, 10)] private float staminaGainRate;
    [SerializeField] [Range(0.1f, 50)] private float staminaUseRate;
    [SerializeField] [Range(0.1f, 10)] private float afterSprintCooldown;
    [SerializeField] private float stamina = 100f;

    [Header("OtherStuff")]
    [SerializeField] private Slider staminaMeter;

    [Header("Info(don't touch)")]
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool canRun = true;
    private Vector2 currentDirection;
    private float horizontal, vertical;
    void Update()
    {
        Sprint();
        Movement();
        UpdateInfo();
    }

    private void Movement()
    {
        float movementStrenght = Time.deltaTime * movementSpeed * (isRunning? sprintMultiplier : 1f);
        float turnStrenght = Time.deltaTime * turnRate * (isRunning? sprintMultiplier : 1f);

        horizontal = Mathf.Lerp(horizontal, Input.GetAxis("Horizontal") * movementStrenght, turnStrenght);

        vertical = Mathf.Lerp(vertical, Input.GetAxis("Vertical") * movementStrenght, turnStrenght);

        transform.position += new Vector3(horizontal ,0 , vertical);
    }
    private void Sprint()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) && canRun)//Checks if you have been running and stopped, to give you a cooldown 
        {
            Debug.Log("Woops, can't sprint, dont spam this ;P");
            canRun = false;
            isRunning = false;
            StartCoroutine(CantSprint());
            return;
        }
        if (!Input.GetKey(KeyCode.LeftShift) || !canRun)//checks if you can and are trying to run
        {
            stamina = Mathf.Clamp(stamina += Time.deltaTime * staminaGainRate, 0, 100);
            isRunning = false;
            return;
        }

        if (stamina > 0)
        {
            stamina = Mathf.Clamp(stamina -= Time.deltaTime * staminaUseRate, 0, 100);
            isRunning = true;
        }
        else
        {
            isRunning = false;
            canRun = false;
            StartCoroutine(CantSprint());
        } 
    }
    private void UpdateInfo()
    {
        staminaMeter.value = stamina / 100;
    }
    private IEnumerator CantSprint()
    {
        Debug.Log("Woops, can't sprint");
        yield return new WaitForSeconds(afterSprintCooldown);
        canRun = true;
    }

    private void LookDirection()
    {
        currentDirection = new Vector2(horizontal, vertical).normalized;

        if (currentDirection != Vector2.zero)
        {
            Debug.DrawRay(transform.position, currentDirection * 100, Color.red);
            Debug.Log("stuffs happening");
        }

    }
}