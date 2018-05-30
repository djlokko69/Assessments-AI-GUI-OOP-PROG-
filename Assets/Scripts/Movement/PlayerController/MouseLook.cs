using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Camera.FirstPerson
{
    [AddComponentMenu("Player/Camera/FirstPerson")]
    public class MouseLook : MonoBehaviour
    {
        #region Variables
        [Header("Sensitivity")]
        public float sensX = 15;
        public float sensY = 15;
        [Header("Rotation Clamp")]
        public float miniY = -60;
        public float maxiY = 60;
        float rotationY = 0;
        public RotationalAxis axis = RotationalAxis.MouseX;
        #endregion
        #region RotationAxis
        public enum RotationalAxis
        {
            MouseXandY = 0,
            MouseX = 1,
            MouseY = 2
        }
        #endregion
        #region Start
        void Start()
        {
            if (this.GetComponent<Rigidbody>())
            {
                this.GetComponent<Rigidbody>().freezeRotation = true; // stopping the object rolating
            }
        }
        #endregion
        #region Update
        void Update()
        {
            if (axis == RotationalAxis.MouseXandY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensX;
                rotationY += Input.GetAxis("Mouse Y") * sensY;
                rotationY = Mathf.Clamp(rotationY, miniY, maxiY);
                //oiler
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axis == RotationalAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensX, 0);
            }
            else // (axis == RotationalAxis.MouseY)
            {
                rotationY += Input.GetAxis("Mouse Y") * sensY;
                rotationY = Mathf.Clamp(rotationY, miniY, maxiY);
                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
        #endregion
    }
}
