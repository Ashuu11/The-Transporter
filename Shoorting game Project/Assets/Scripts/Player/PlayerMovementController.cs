using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    //Player behaviour elements
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float crouchSpeed = 7f;
    [SerializeField] private float sprintSpeed = 70f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpRaycastDistance = 2.3f;
    [SerializeField] private float staminaDepletFactor = 55;
    [SerializeField] private float staminaRefillFactor = 5;
    [SerializeField] private Camera PlayerCamera; //normal camera ... eyes 

    //ui elements 
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Text staminaText;
    [SerializeField] private Color maxStaminaColor;
    [SerializeField] private Color zeroStaminaColor;


    private bool IsCrouching = false;
    private bool IsSprinting = false;
    private CapsuleCollider playerCollider;
    private float currentStamina;

    private Rigidbody rb;
    private float NormalFOV;
    private float SprintFOVmodifier = 1.5f;

    private void Start()
    {
        NormalFOV = PlayerCamera.fieldOfView; //getting the normal field of view
        rb = GetComponent<Rigidbody>();
        currentStamina = playerStats.maxStamina;
        playerCollider = GetComponent<CapsuleCollider>();

        StaminaUiUpdate();
    }


    private void Update()
    {
        Jump();
    }


    private void FixedUpdate()
    {

        Move();

        Crouching();
        Sprint();
    }



    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal"); //getting the inputs
        float vAxis = Input.GetAxisRaw("Vertical");  // from user

        if (IsSprinting == true) //Determining the speed to be used based on walk, sprint, crouch...
        {

            speed = sprintSpeed;
        }
        else if (IsCrouching == true)
        {
            speed = crouchSpeed;

        }
        else
        {
            speed = walkSpeed;
        }

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.fixedDeltaTime; // calculate for new position 
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement); // moves the player to new position
        rb.MovePosition(newPosition);
    }


    private bool IsGrounded() //function to check whether the player is on ground
    {
        //Debug.DrawRay(transform.position, Vector3.down * jumpRaycastDistance, Color.blue); 
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance); //Fires a ray from the current position in downward direction for some distance
    }

    private void Jump()
    {
        if (IsGrounded()) // check if the player is on ground
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse); // add jumpForce as impulse in Y axis and no effect on X and Z axis
            }
        }
    }

    private void Crouching()
    {
        if (Input.GetKey(KeyCode.C))
        {
            IsCrouching = true;
            playerCollider.height = 1;
        }
        else
        {
            IsCrouching = false;
            playerCollider.height = 2;
        }
    }

    private void Sprint()
    {
        //write code for stamina handling 
        if ((Input.GetKey(KeyCode.W)) && ((Input.GetKey(KeyCode.LeftShift)) /*|| (Input.GetKey(KeyCode.RightShift))*/ ))
        {
            if (currentStamina > 0)
            {
                IsSprinting = true;
                currentStamina -= (staminaDepletFactor * Time.deltaTime);
            }
            else
            {
                IsSprinting = false;
            }
        }
        else
        {
            IsSprinting = false;
            PlayerCamera.fieldOfView = NormalFOV;
            if (currentStamina < playerStats.maxStamina)//stamina regeneration
            {
                currentStamina += (staminaRefillFactor * Time.deltaTime);
            }
        }

        if (IsSprinting) // this will change the FOV when we are sprinting..
        {
            PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, NormalFOV * SprintFOVmodifier, Time.deltaTime * 8f);
        }
        else
        {
            PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, NormalFOV, Time.deltaTime * 8f);
        }

        StaminaUiUpdate();
    }

    private void StaminaUiUpdate()
    {
        //write code for stamina ui
        float staminaPercentage = CalculateStaminaPercentage();

        staminaText.text = "Stamina: " + currentStamina.ToString("0");
        staminaText.color = Color.Lerp(zeroStaminaColor, maxStaminaColor, staminaPercentage / 100);
    }

    private float CalculateStaminaPercentage()
    {
        return ((float)currentStamina / (float)playerStats.maxStamina) * 100;//casting is used to treat values as floats
    }


}
