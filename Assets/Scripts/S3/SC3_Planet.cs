using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using SimpleKeplerOrbits;

public class SC3_Planet : MonoBehaviour
{
    [SerializeField] XRRayInteractor leftRayInteractor;
    [SerializeField] XRRayInteractor rightRayInteractor;
    [SerializeField] InputActionAsset xriInputAction;
    [SerializeField] GameObject destPos;

    InputAction leftTrigger, rightTrigger;

    bool onHit = false;
    bool planetMove = false;
    bool planetOn = false;

    Vector3 startPos;

    private float distance = 100.0f;
    public float ROTSPEED = 5.0f;
    const float ROTQUATER = 5.0f;

    RaycastHit hit;
    GameObject targetObj;

    public enum Planet { moveOn, moveOff, none }
    Planet planet = Planet.none;
    public enum ButtonType { Trigger, Grip }
    ButtonType buttonType = ButtonType.Trigger;

    void Start()
    {
        rightTrigger = xriInputAction.FindActionMap("XRI RightHand").FindAction("Trigger");
        leftTrigger = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Trigger");
    }

    void Update()
    {
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance);

        switch (buttonType)
        {
            case ButtonType.Trigger:
                GetButton(rightTrigger, leftTrigger);
                break;
        }

        switch (planet)
        {
            case Planet.moveOn:
                planetMove = true;
                targetObj.transform.position = Vector3.MoveTowards(targetObj.transform.position, destPos.gameObject.transform.position, ROTSPEED * Time.deltaTime);
                if (targetObj.transform.gameObject.transform.position == destPos.gameObject.transform.position)
                {
                    planetOn = true;
                    targetObj.GetComponent<SphereCollider>().enabled = true;
                    planetMove = false;
                }
                break;

            case Planet.moveOff:
                planetOn = false;
                targetObj.transform.position = Vector3.MoveTowards(targetObj.transform.position, startPos, ROTSPEED * Time.deltaTime);
                targetObj.GetComponent<KeplerOrbitMover>().enabled = true;
                break;

            case Planet.none:
                break;
        }
    }

    void planetMoveOn()
    {
        if (hit.transform.CompareTag("Planet") && !planetMove)
        {
            planet = Planet.moveOn;
            targetObj = hit.transform.gameObject;
            targetObj.GetComponent<KeplerOrbitMover>().enabled = false;
            targetObj.GetComponent<SphereCollider>().enabled = false;
            startPos = hit.transform.gameObject.transform.position;
        }
    }
    void planetMoveOff()
    {
        if (hit.transform.CompareTag("Planet") && !planetMove) planet = Planet.moveOff;
    }

    void GetButton(InputAction right, InputAction left)
    {
        if (right.triggered)
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) || rightRayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                if (hit.transform.CompareTag("Planet") && !planetOn) planetMoveOn();
                if (hit.transform.CompareTag("Planet") && planetOn) planetMoveOff();
            }
        }

        if (left.triggered)
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) || rightRayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                if (hit.transform.CompareTag("Planet") && !planetOn) planetMoveOn();
                if (hit.transform.CompareTag("Planet") && planetOn) planetMoveOff();
            }
        }
    }
}
