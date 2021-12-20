using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine;
using System;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class OculusInput : MonoBehaviour
    {

        public XRController controller = null;
        private GameObject _camera;

        Planet planet = Planet.moveOn;
        enum Planet
        {
            moveOn,
            moveOff,
        }

        bool onHit = false;
        bool planetMove = false;

        Vector3 startPos;
        float distance = 10.0f;

        const float ROTSPEED = 2.0f;
        const float ROTQUATER = 2.0f;

        RaycastHit hit;
        GameObject targetObj;
        [SerializeField] GameObject destPos;

        private void Awake()
        {
            _camera = GetComponent<XRRig>().cameraGameObject;
        }

        private void Update()
        {
            CommonInput();
        }

        private void CommonInput()
        {
            // A Button
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primary)) { }
            //output += "A Pressed: " + primary + "\n";

            // B Button
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondary)) { }
            // output += "B Pressed: " + secondary + "\n";

            // Touchpad/Joystick touch
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool touch)) { }
            // output += "Touchpad/Joystick Touch: " + touch + "\n";

            // Touchpad/Joystick press
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool press))
            { }
            // output += "Touchpad/Joystick Pressed: " + press + "\n";

            // Touchpad/Joystick position
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            { }
            // output += "Touchpad/Joystick Position: " + position + "\n";

            // Grip press
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool grip)) { }
            //  output += "Grip Pressed: " + grip + "\n";

            // Grip amount
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripAmount)) { }
            //  output += "Grip Amount: " + gripAmount + "\n";

            // Trigger press
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger))
            {
                if (!planetMove && hit.transform.CompareTag("Planet")) planetMoveOn();
                if (!planetMove && hit.transform.CompareTag("Planet")) planetMoveOff();
            }

            // output += "Trigger Pressed: " + trigger + "\n";

            // Index/Trigger amount
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerAmount)) { }
            //  output += "Trigger: " + triggerAmount;

            switch (planet)
            {
                case Planet.moveOn:
                    planetMove = true;
                    onHit = true;
                    targetObj.transform.Rotate(Vector3.up * Time.deltaTime * ROTQUATER);
                    targetObj.transform.position = Vector3.MoveTowards(targetObj.transform.position, destPos.gameObject.transform.position, ROTSPEED * Time.deltaTime);
                    if (targetObj.transform.gameObject.transform.position == destPos.gameObject.transform.position)
                    {
                        targetObj.GetComponent<BoxCollider>().enabled = true;
                        planetMove = false;
                    }
                    break;

                case Planet.moveOff:
                    planetMove = true;
                    onHit = true;
                    targetObj.transform.Rotate(Vector3.up * Time.deltaTime * ROTQUATER);
                    targetObj.transform.position = Vector3.MoveTowards(targetObj.transform.position, startPos, ROTSPEED * Time.deltaTime);
                    if (targetObj.transform.gameObject.transform.position == startPos)
                    {
                        targetObj.GetComponent<BoxCollider>().enabled = true;
                        planetMove = false;
                    }
                    break;
            }
        }
        void planetMoveOn()
        {
            if (hit.transform.CompareTag("Planet"))
            {
                planet = Planet.moveOn;
                targetObj = hit.transform.gameObject;
                targetObj.GetComponent<BoxCollider>().enabled = false;
                startPos = hit.transform.gameObject.transform.position;
            }
        }
        void planetMoveOff()
        {
            planet = Planet.moveOff;
            targetObj.GetComponent<BoxCollider>().enabled = false;
        }
    }
}