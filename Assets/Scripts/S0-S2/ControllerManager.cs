using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] XRRayInteractor leftRayInteractor;
    [SerializeField] XRRayInteractor rightRayInteractor;
    [SerializeField] InputActionAsset xriInputAction;
    InputAction leftGrip, leftTrigger, rightGrip, rightTrigger;
    public enum ButttonType { Trigger, Grip };
    public ButttonType butttonType;

    [Header("Main Themes")]
    [SerializeField] Transform planet;
    [SerializeField] Transform blackHole;


    [Header("")]
    [SerializeField] string planetScene;
    [SerializeField] string blackHoleScene;

    //public enum NowSceneNumber { 0, 1, 2 };
    //public NowSceneNumber nowSceneNumber;
    // int sceneNum;
    void Start()
    {
        leftGrip = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Grip");
        leftTrigger = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Trigger");
        rightGrip = xriInputAction.FindActionMap("XRI RightHand").FindAction("Grip");
        rightTrigger = xriInputAction.FindActionMap("XRI RightHand").FindAction("Trigger");
    }

    void Update()
    {
        //ControllerCheck();
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

        //현재 어느 씬인지
        //switch (nowSceneNumber)
        //{
        //    case NowSceneNumber.0:
        //            sceneNum = 0; break;
        //    case NowSceneNumber.1:
        //            sceneNum = 1; break;
        //    case NowSceneNumber.2:
        //            sceneNum = 2; break;
        //}
    }
    void ControllerCheck()
    {
        if (rightTrigger.triggered)
            print(rightTrigger.name);
        if (rightGrip.triggered)
            print(rightGrip.name);
    }
    void GetButton(InputAction left, InputAction right)
    {
        if (left.triggered || right.triggered)
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                if (hit.transform == planet)
                {
                    print("planet으로 이동");
                    SceneManager.LoadScene(planetScene);
                }
                if (hit.transform == blackHole)
                {
                    print("블랙홀으로 이동");
                    //SceneManager.LoadScene(blackHoleScene);
                }
            }

            if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitR))
            {
                if (hitR.transform == planet)
                {
                    print("planet으로 이동");
                    if (planetScene != null)
                        SceneManager.LoadScene(planetScene);
                }
                if (hitR.transform == blackHole)
                {
                    print("블랙홀으로 이동");
                    if (blackHoleScene != null)
                        SceneManager.LoadScene(blackHoleScene);
                }
            }
        }
    }
}
