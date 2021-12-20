using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class EnterNextRoom : MonoBehaviour
{
    [SerializeField] GameObject timerPrefab;

    [SerializeField] XRRayInteractor leftRayInteractor;
    [SerializeField] XRRayInteractor rightRayInteractor;
    [SerializeField] InputActionAsset xriInputAction;

    InputAction leftGrip, leftTrigger, rightGrip, rightTrigger;
    public enum ButttonType { Trigger, Grip };
    public ButttonType butttonType;

    [SerializeField] Transform collider;



    void Start()
    {
        leftGrip = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Grip");
        leftTrigger = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Trigger");
        rightGrip = xriInputAction.FindActionMap("XRI RightHand").FindAction("Grip");
        rightTrigger = xriInputAction.FindActionMap("XRI RightHand").FindAction("Trigger");
    }

    void Update()
    {
        //어떤 버튼 이용할지 선택하기
        switch (butttonType)
        {
            case ButttonType.Grip:
                GetButton(leftGrip, rightGrip);
                break;
            case ButttonType.Trigger:
                GetButton(leftTrigger, rightTrigger);
                break;
        }
    }
    void GetButton(InputAction left, InputAction right)
    {
        if (left.triggered || right.triggered)
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                if (hit.transform == collider)
                {
                    EnterNextScene();
                }
            }
            if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitR))
            {
                if (hitR.transform == collider)
                {
                    EnterNextScene();
                }
            }
        }
    }


    void EnterNextScene()
    {
        print("enter next scene");

        GameObject timerPresent = GameObject.FindGameObjectWithTag("Timer");

        if (timerPresent == null)
        {
            bool isActive = false;
            if (!isActive)
            {
                GameObject timer = GameObject.Instantiate(timerPrefab);
                isActive = true;
            }
        }
        else
        {
            timerPresent.GetComponent<Timer>().curTime = 0;
        }
        SceneManager.LoadScene(2);
    }
}