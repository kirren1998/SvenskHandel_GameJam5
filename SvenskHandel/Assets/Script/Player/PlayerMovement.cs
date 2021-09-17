using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]private float moveSpeed = 6;
        [SerializeField] private float sprintBoost = 3;
        private Rigidbody rb;
        [SerializeField] private float stamina = 100;
        [SerializeField] private float staminaGain = 4;
        [SerializeField] private float staminaLose = 20;
        [SerializeField] private Image staminaBar;
        [SerializeField] private float sprintThreshold = 10;
        private bool avoidJittering;
        float horizontal;
        float vertical;
    
        private bool sprinting;

        public Transform PickupDest;
        public Transform dropDest;
    
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    
        private void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            //staminaBar.fillAmount = stamina / 100;

            if (stamina < 1)
            {
                StartCoroutine(SprintThreshold());
            }
        }

        //Movement. Gain stamina when not sprinting set by staminaGain.
        void FixedUpdate()
        {
            if (!sprinting)
            {
                rb.MovePosition(rb.position + new Vector3(horizontal, 0, vertical) * (Time.fixedDeltaTime * moveSpeed));
                if (stamina < 100)
                {
                    stamina += staminaGain * Time.deltaTime;
                }
            }
        
            if (Input.GetKey(KeyCode.LeftShift) && !avoidJittering)
            {
                Sprint();
            }
            else
            {
                sprinting = false;
            }
        }

        //Increases movement speed and lower the stamina bar by staminaLose amount.
        private void Sprint()
        {
            stamina -= staminaLose * Time.deltaTime;
            sprinting = true;
            rb.MovePosition(rb.position + new Vector3(horizontal, 0, vertical) *
                (Time.fixedDeltaTime * (moveSpeed + sprintBoost)));
        }

        //Sprint threshold. When under stamina is under sprintThreshold you cant press sprint again.
        private IEnumerator SprintThreshold()
        {
            avoidJittering = true;
            yield return new WaitUntil((() => stamina > sprintThreshold));
            avoidJittering = false;
        }

        //Check if a package is close
              private void OnTriggerStay(Collider other)
              {
                  if (Input.GetKeyDown(KeyCode.F))
                  {
                      if (other.CompareTag("Pickup"))
                      {
                          if (other.gameObject.GetComponent<Pickup>() != null)
                          {
                              other.gameObject.GetComponent<Pickup>().PickupCheck(PickupDest, dropDest);
                          }
                      }
      
                      if (other.CompareTag("Terminal"))
                      {
                          other.gameObject.GetComponent<PostTerminal>().DeliverPackage();
                      }
                  }
              }
    }
}
