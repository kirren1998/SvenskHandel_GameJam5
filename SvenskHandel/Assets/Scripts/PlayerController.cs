using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region MovementSettings
    [Header("MovementSettings")]
    [Tooltip("Will determine how fast the player will move while walking")]
    [SerializeField] [Range(1f, 10f)] private float movementSpeed;
    [Tooltip("How much faster you will move while running(also applies to turnRate)")]
    [SerializeField] [Range(1.5f, 5f)] private float sprintMultiplier;
    [Tooltip("Determines how fast the character will turn and change direction")]
    [SerializeField] [Range(1f, 10f)] private float turnRate;
    #endregion

    #region BoxControlleChanges
    [Header("BoxContollChanges")]
    [Tooltip("Determines the amount (in percent) the player will loose controll over his movement when picking up packages over the limit")]
    [SerializeField] [Range(0f, 100f)] private int lossOfControllPercent;
    [Tooltip("How many boxes the player can hold before starting to loose controll")]
    [SerializeField] [Range(1f, 10f)] private int maxHeldBoxes;
    #endregion

    #region StaminaSettings
    [Header("StaminaSettings")]
    [Tooltip("How many point of stamina you will restore each second while not running")]
    [SerializeField] [Range(0.1f, 10f)] private float staminaGainRate;
    [Tooltip("How many point of stamina you will use each second while running")]
    [SerializeField] [Range(0.1f, 50f)] private float staminaUseRate;
    [Tooltip("How many long the user have to wait before running again")]
    [SerializeField] [Range(0.1f, 10f)] private float afterSprintCooldown;
    [Tooltip("How many long after the stamina becomes full for the bar to become invisible")]
    [SerializeField] [Range(0.1f, 10f)] private float hideStaminaBarDelay = .5f;
    [Tooltip("The maximum amount of stamina the user has")]
    [SerializeField] private float maxStamina = 100f;
    [Tooltip("The reference to the slider that shows the player the stamina level")]
    [SerializeField] private Image staminaMeter;
    #endregion

    #region AnimationSettings
    [Header("AnimationSettings")]
    private float animationSpeed;
    private Animator workerAnimator => GetComponent<Animator>();
    //[Tooltip("The animator that is connected to the worker")]
    #endregion

    #region Info
    [Header("Info(don't touch, only for show)")]
    [Tooltip("How many boxes the player is holding")]
    [SerializeField] private int currentHeldBoxes;
    [Tooltip("The amount of stamina the user has currently")]
    [SerializeField] private float stamina = 100f;
    [Tooltip("Wether the player is running currently")]
    [SerializeField] private bool isRunning = false;
    [Tooltip("Wether the player can run")]
    [SerializeField] private bool canRun = true;
    [Tooltip("How long time the bar has to be full before it goes invisible")]
    [SerializeField] private float hideStaminaBarTimer = 0;
    [Tooltip("The stamina Canvas")]
    [SerializeField] private Transform staminaCanvas;
    #endregion

    #region invisible variables
    private Vector3 currentDirection;
    private float horizontal, vertical;
    #endregion

    #region DoneFunctions
    void Update()
    {
        Sprint();
        Movement();
        UpdateInfo();
        LookDirection();
        StaminaBarInfo();
        AnimationUpdate();
    }

    /// <summary>
    /// This function uses the horizontal and vertical inputs from unity, making both computer and controller input usable
    /// </summary>
    private void Movement()
    {
        // how much controll you are loosing for handeling to many boxes
        float currentLoss = 1 - (currentHeldBoxes > maxHeldBoxes ? (currentHeldBoxes - maxHeldBoxes) * lossOfControllPercent : 0);

        float movementStrenght = Time.deltaTime * movementSpeed * (isRunning? sprintMultiplier : 1f) * currentLoss;
        float turnStrenght = Time.deltaTime * turnRate * (isRunning? sprintMultiplier : 1f) * currentLoss;

        horizontal = Mathf.Lerp(horizontal, Input.GetAxis("Horizontal") * movementStrenght, turnStrenght);

        vertical = Mathf.Lerp(vertical, Input.GetAxis("Vertical") * movementStrenght, turnStrenght);

        transform.position += new Vector3(horizontal ,0 , vertical);
    }



    /// <summary>
    /// Changes the speed of handleing and movementspeed greater by a margin while using the sprint function
    /// </summary>
    private void Sprint()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) && canRun)//Checks if you have been running and stopped, to give you a cooldown 
        {
            canRun = false;
            isRunning = false;
            //staminaMeter.color = Color.red;
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
            //staminaMeter.color = Color.red;
            StartCoroutine(CantSprint());
        } 
    }



    /// <summary>
    /// Makes sure that all of the information that is needed is updated regularly
    /// </summary>
    private void UpdateInfo()
    {
        staminaMeter.fillAmount = stamina / maxStamina;
        staminaMeter.color = canRun? new Color(1 - stamina / maxStamina, 0, stamina / maxStamina) : Color.red;
        staminaCanvas.position = transform.position;
    }



    /// <summary>
    /// This is a cooldown that will make sure that the player can not toggle sprint on and off rapidly while out of stamina, or simply spamming the button
    /// </summary>
    /// <returns>Checks the bool back to true so that you can sprint again</returns>
    private IEnumerator CantSprint()
    {
        Debug.Log("Woops, can't sprint");
        yield return new WaitForSeconds(afterSprintCooldown);
        canRun = true;
    }



    /// <summary>
    /// Makes the physical character change direction based on what direction he is mooving
    /// </summary>
    private void LookDirection()
    {
        currentDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (currentDirection != Vector3.zero)
        {
            transform.forward = currentDirection;
            Debug.DrawRay(transform.position, currentDirection * 100, Color.red);
            Debug.Log("stuffs happening");
        }
    }


    /// <summary>
    /// This function will hide the stamina bar after a set timer to not clutter the screen to much
    /// </summary>
    private void StaminaBarInfo()
    {
        if (stamina == 100)
        {
            hideStaminaBarTimer = Mathf.Clamp(hideStaminaBarTimer += Time.deltaTime, 0, hideStaminaBarDelay);
            staminaMeter.gameObject.SetActive(hideStaminaBarTimer < hideStaminaBarDelay);
            Debug.Log(hideStaminaBarTimer < hideStaminaBarDelay);
        }
        else
        {
            staminaMeter.gameObject.SetActive(true);
            hideStaminaBarTimer = 0;
        }
    }

    #endregion

    /// <summary>
    /// Sets the right animation and makes the animation speed equal to the speed that the character is mooving
    /// </summary>
    private void AnimationUpdate()
    {
        workerAnimator.SetFloat("AnimationSpeed", Mathf.Abs(vertical) + Mathf.Abs(horizontal));
        workerAnimator.speed = (Mathf.Abs(vertical) + Mathf.Abs(horizontal) * 100) + 1;
    }
}