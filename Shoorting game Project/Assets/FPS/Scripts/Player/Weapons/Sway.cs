using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class Sway : MonoBehaviour
    {
        //the weapon lags behind a bit to give a realistic feel 

        #region Variables

        [SerializeField] float intensity = 1f;
        [SerializeField] float smooth = 10f;

        private Quaternion originRotation;

       

        #endregion




        #region Monobehaviour Callbacks 

        private void Start()
        {
            originRotation = transform.rotation;
        }

        void Update()
        {          
           UpdateSway();                     
        }

        #endregion




        #region Private Methods

        void UpdateSway()
        {
            //controls 
            float xmouse = Input.GetAxis("Mouse X");
            float ymouse = Input.GetAxis("Mouse Y");

            //Calculate target rotation 
            Quaternion xadj = Quaternion.AngleAxis(-intensity * xmouse, Vector3.up);
            Quaternion yadj = Quaternion.AngleAxis(intensity * ymouse, Vector3.right);
            Quaternion targetRotation = originRotation * xadj * yadj; // adding angles

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth); //rotate towards target rotation
            
        }

        #endregion
    }



}

