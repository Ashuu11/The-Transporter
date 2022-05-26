using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class Motion : MonoBehaviour
    {
        #region Variables

        [SerializeField] float speed = 400f;
        [SerializeField] float sprintModifier = 2f;
        [SerializeField] float crouchModifier = 0.5f;
        [SerializeField] float crouchAmount = 0.5f; //how much the camera will go down while crouching 
        [SerializeField] Camera normalCam; //player eyes camera
        [SerializeField] Transform weaponParent;
        [SerializeField] LayerMask ground;
        [SerializeField] Transform groundDetector;
        [SerializeField] float jumpForce = 7f;
        [SerializeField] GameObject standingCollider;
        [SerializeField] GameObject crouchingCollider;

        private Vector3 weaponParentOrigin;
        private Vector3 targetWeaponBobPosition;
        private Vector3 weaponParentCurrentPos;
        private Vector3 origin;

        private float movementCounter;
        private float idleCounter;
        private float baseFOV;
        private float sprintFOVmodifier = 1.5f;
        
        private Rigidbody rb;
        private Weapon weapon; //reference to the weapon script

        private Text uiAmmo;

        private bool crouched;
        private bool isAiming;

        #endregion

        #region Monobehaviour Callbacks

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            weapon = GetComponent<Weapon>(); // get weapon script fromthe player

            baseFOV = normalCam.fieldOfView;
            origin = normalCam.transform.localPosition; //origin of camera
            weaponParentOrigin = weaponParent.localPosition; //saves the position of weapon parent 
            weaponParentCurrentPos = weaponParentOrigin;            
            
            uiAmmo = GameObject.Find("HUD/Ammo/Text").GetComponent<Text>();
        }

        void Update()
        {
            //Inputs
            float hMove = Input.GetAxisRaw("Horizontal");
            float vMove = Input.GetAxisRaw("Vertical");

            //Controls
            bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool jump = Input.GetKeyDown(KeyCode.Space);
            bool crouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);

            //States
            bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.15f, ground); //check for ground
            bool isJumping = jump && isGrounded;
            bool isSprinting = sprint && vMove > 0 && !isJumping && isGrounded;
            bool isCrouching = crouch && !isSprinting && !isJumping && isGrounded;

            //Crouching
            if(isCrouching)
            {
                SetCrouch(!crouched);
            }

            //Jumping
            if (isJumping)
            {
                if(crouched)
                {
                    SetCrouch(false);
                }
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }

            //Headbob
            
            if(!isGrounded)
            {
                //In the Air
                Headbob(idleCounter, 0.01f, 0.01f);
                idleCounter += 0;
                weaponParent.localPosition = Vector3.MoveTowards(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f * 0.2f);
            }
            else if (hMove == 0 && vMove == 0) 
            { 
                //Idle
                Headbob(idleCounter, 0.025f, 0.025f); //when idle
                idleCounter += Time.deltaTime;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);

            }
            else if (!isSprinting && !crouched)
            {
                //Walking
                Headbob(movementCounter, 0.035f, 0.035f);  //when in motion
                movementCounter += Time.deltaTime * 6f;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);

            }
            else if(crouched)
            {
                //Crouching
                Headbob(movementCounter, 0.02f, 0.02f);  //when in motion
                movementCounter += Time.deltaTime * 4f;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);

            }
            else
            {
                //Sprinting
                Headbob(movementCounter, 0.15f, 0.075f);  //when in sprint                      == alter the values of parameter as well as the multipliers to change the speed of weapon bob
                movementCounter += Time.deltaTime * 13.5f;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);

            }


            //UI Refreshers
            weapon.RefreshAmmo(uiAmmo);

        }

        private void FixedUpdate()
        {
            //Inputs
            float hMove = Input.GetAxisRaw("Horizontal");
            float vMove = Input.GetAxisRaw("Vertical");

            //Variables
            float adjustedSpeed = speed;

            //Controls
            bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool jump = Input.GetKey(KeyCode.Space);
            bool aim = Input.GetMouseButton(1);

            //States
            bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground); //check for ground
            bool isJumping = jump && isGrounded;
            bool isSprinting = sprint && vMove > 0 && !isJumping && isGrounded;
            isAiming = aim && !isSprinting;
            
            //Sprinting Controller
            if (isSprinting)
            {
                if (crouched)
                {
                    SetCrouch(false);
                }
                adjustedSpeed *= sprintModifier;
            }
            else if(crouched)   //CrouchController
            {
                adjustedSpeed *= crouchModifier;
            }
            
            //Aiming
            isAiming = weapon.Aim(isAiming);
            
            //Camera stuffs
            if (isSprinting)
            {
                normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVmodifier, Time.deltaTime * 8f); //change FOV of camera while sprinting - frame by frame
            }
            else
            {
                normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 4f); 
            }

            if(crouched)
            {
                normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, origin + Vector3.down * crouchAmount, Time.deltaTime * 6f );
            }
            else
            {
                normalCam.transform.localPosition = Vector3.Lerp(normalCam.transform.localPosition, origin, Time.deltaTime * 6f);
            }

            //Main movement code
            Vector3 direction = new Vector3(hMove, 0, vMove);
            direction.Normalize(); //normalize so that speed is equal while moving in all direction
            Vector3 targetVelocity = transform.TransformDirection(direction) * adjustedSpeed * Time.deltaTime; //motion velocity
            targetVelocity.y = rb.velocity.y;
            rb.velocity = targetVelocity;
        }

        #endregion

        #region Private methods

        void Headbob(float z, float xIntensity, float yIntensity)
        {
            float aimAdjust = 1f;
            if(isAiming)
            {
                aimAdjust = 0.1f;
            }
            //Breating and walking effect 

            targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(z) * xIntensity * aimAdjust , Mathf.Sin(z * 2) * yIntensity * aimAdjust, 0);
        }

        void SetCrouch(bool state)
        {
            if (crouched = state) return;
            
            crouched = state;

            if(crouched)
            {
                standingCollider.SetActive(false);
                crouchingCollider.SetActive(true);
                weaponParentCurrentPos += Vector3.down * crouchAmount;
            }
            else
            {
                standingCollider.SetActive(true);
                crouchingCollider.SetActive(false);
                weaponParentCurrentPos -= Vector3.down * crouchAmount;
            }
        }

        #endregion


    }

}
