using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("MovementSettings")]
    [Tooltip("Will determine how fast the player will move while walking")]
    [SerializeField] [Range(1f, 10f)] private float movementSpeed;
    [Tooltip("How much faster you will move while running(also applies to turnRate)")]
    [SerializeField] [Range(1.5f, 5f)] private float sprintMultiplier;
    [Tooltip("Determines how fast the character will turn and change direction")]
    [SerializeField] [Range(1f, 10f)] private float turnRate;

    [Header("BoxContollChanges")]
    [Tooltip("Determines the amount (in percent) the player will loose controll over his movement when picking up packages over the limit")]
    [SerializeField] [Range(0f, 100f)] private int lossOfControllPercent;
    [Tooltip("How many boxes the player can hold before starting to loose controll")]
    [SerializeField] [Range(1f, 10f)] private int maxHeldBoxes;
    [Tooltip("How many boxes the player is holding")]
    [SerializeField] [Range(1f, 10f)] private int currentHeldBoxes;

    [Header("StaminaSettings")]
    [Tooltip("How many point of stamina you will restore each second while not running")]
    [SerializeField] [Range(0.1f, 10f)] private float staminaGainRate;
    [Tooltip("How many point of stamina you will use each second while running")]
    [SerializeField] [Range(0.1f, 50f)] private float staminaUseRate;
    [Tooltip("How many long the user have to wait before running again")]
    [SerializeField] [Range(0.1f, 10f)] private float afterSprintCooldown;
    [Tooltip("How many long after the stamina becomes full for the bar to become invisible")]
    [SerializeField] [Range(0.1f, 1f)] private float hideStaminaBarDelay = .5f;
    [Tooltip("The maximum amount of stamina the user has")]
    [SerializeField] private float maxStamina = 100f;
    [Tooltip("The reference to the slider that shows the player the stamina level")]
    [SerializeField] private Slider staminaMeter;

    //[Header("OtherStuff")]

    [Header("Info(don't touch, only for show)")]
    [Tooltip("The amount of stamina the user has currently")]
    [SerializeField] private float stamina = 100f;
    [Tooltip("Wether the player is running currently")]
    [SerializeField] private bool isRunning = false;
    [Tooltip("Wether the player can run")]
    [SerializeField] private bool canRun = true;
    [Tooltip("How long time the bar has to be full before it goes invisible")]
    [SerializeField] private float hideStaminaBarTimer = 0;
    private Vector2 currentDirection;
    private float horizontal, vertical;
    void Update()
    {
        Sprint();
        Movement();
        UpdateInfo();
        LookDirection();
        if (stamina == 100)
        {
            hideStaminaBarTimer += Time.deltaTime;
            staminaMeter.gameObject.SetActive(hideStaminaBarTimer <= hideStaminaBarDelay);
        }
        else
        {
            staminaMeter.gameObject.SetActive(true);
            hideStaminaBarTimer = 0;
        }
    }

    private void Movement()
    {
        float currentLoss = 1 - currentHeldBoxes > maxHeldBoxes ? (currentHeldBoxes - maxHeldBoxes) * lossOfControllPercent : 0;
        float movementStrenght = Time.deltaTime * movementSpeed * (isRunning? sprintMultiplier : 1f) * currentLoss;
        float turnStrenght = Time.deltaTime * turnRate * (isRunning? sprintMultiplier : 1f) * currentLoss;

        horizontal = Mathf.Lerp(horizontal, Input.GetAxis("Horizontal") * movementStrenght, turnStrenght);

        vertical = Mathf.Lerp(vertical, Input.GetAxis("Vertical") * movementStrenght, turnStrenght);

        transform.position += new Vector3(horizontal ,0 , vertical);
    }
    private void Sprint()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) && canRun)//Checks if you have been running and stopped, to give you a cooldown 
        {
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
        staminaMeter.value = stamina / maxStamina;
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