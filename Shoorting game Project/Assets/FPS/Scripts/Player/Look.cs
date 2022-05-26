using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class Look : MonoBehaviour
    {
        #region Variables
        [SerializeField] Transform player;
        [SerializeField] Transform cams; //eye of the player
        [SerializeField] private float xSenstivity = 160f;
        [SerializeField] private float ySenstivity = 160f;
        [SerializeField] Transform weapon;

        private float maxAngle = 85f;
        private Quaternion camCenter;

        public static bool cursorLocked = true; //accessible from any script
        #endregion

        #region Monobehaviour Callbacks

        void Start()
        {
            camCenter = cams.localRotation; // rotation origin for camera

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            SetX();
            SetY();
            UpdateCursorLock();
        }

        #endregion

        #region Private Methods 

        void SetY() //affects camera vertical looks
        {
            float input = Input.GetAxis("Mouse Y") * ySenstivity * Time.deltaTime;

            Quaternion adj = Quaternion.AngleAxis(input, -Vector3.right); //returns a rotation with certain degree from and axis
            Quaternion delta = cams.localRotation * adj; //calculates angle for the camera Y rotation    

            if (Quaternion.Angle(camCenter, delta) < maxAngle) //Clamping the camera angle - if angle from the range of center to delta is less than max angle then only move
            {
                cams.localRotation = delta;// changes camera Y rotation

            }

            weapon.rotation = cams.rotation; //rotates the gun with the camera in vertical axis


        }

        void SetX() //rotates player sidewise
        {
            float input = Input.GetAxis("Mouse X") * xSenstivity * Time.deltaTime;

            Quaternion adj = Quaternion.AngleAxis(input, Vector3.up); //returns a rotation with certain degree from and axis
            Quaternion delta = player.localRotation * adj; //calculates angle for the player X rotation    
            player.localRotation = delta; // changes player X rotation            
        }

        void UpdateCursorLock()
        {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursorLocked = false;
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursorLocked = true;
                }
            }
        }

        #endregion

       


    }
}

