using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class UIManager_titleScene : MonoBehaviour
{
    [Header("XRs")]
    [SerializeField] XRRayInteractor leftRayInteractor;
    [SerializeField] XRRayInteractor rightRayInteractor;
    [SerializeField] InputActionAsset xriInputAction;

    InputAction leftGrip, leftTrigger, rightGrip, rightTrigger;

    [SerializeField] Transform cam;

    public enum ButttonType { Trigger, Grip };
    public ButttonType butttonType;

    [Header("Objects")]
    [SerializeField] Transform startBtn;
    //[SerializeField] Transform exitBtn;

    //[SerializeField] GameObject exitPannel;

    //[SerializeField] Transform exitY;
    //[SerializeField] Transform exitN;

    InterfaceAnimManager mainUI;
    LoadingSceneEffect loadingUI;
    void Start()
    {
        leftGrip = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Grip");
        leftTrigger = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Trigger");
        rightGrip = xriInputAction.FindActionMap("XRI RightHand").FindAction("Grip");
        rightTrigger = xriInputAction.FindActionMap("XRI RightHand").FindAction("Trigger");


        mainUI = GameObject.Find("Main").transform.GetChild(0).gameObject.GetComponent<InterfaceAnimManager>();
        loadingUI = GameObject.FindWithTag("Loading").gameObject.GetComponent<LoadingSceneEffect>();
        //  loadingUI.forceUnscaledTime = CSFHIAnimableState.disappeared;
    }

    void Update()
    {
        //어떤 버튼 이용할지 선택하기
        switch (butttonType)
        {
            case ButttonType.Grip:
                GetStartButton(leftGrip, rightGrip);
                //GetExitButton(leftGrip, rightGrip);
                break;
            case ButttonType.Trigger:
                GetStartButton(leftTrigger, rightTrigger);
                //GetExitButton(leftGrip, rightGrip);

                break;
        }

        transform.position = cam.position;
    }

    #region Control Input Action
    void GetStartButton(InputAction left, InputAction right)
    {
        if (left.triggered || right.triggered)
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                if (hit.transform == startBtn)
                {
                    print("start");
                    SceneManager.LoadScene(1);
                }
            }
            if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitR))
            {
                if (hitR.transform == startBtn)
                {
                    print("start");
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    //void GetExitButton(InputAction left, InputAction right)
    //{
    //    if (left.triggered || right.triggered)
    //    {
    //        if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
    //        {
    //            if (hit.transform == startBtn)
    //            {
    //                print("exit");
    //                OnExit(left, right);
    //            }
    //        }
    //        if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitR))
    //        {
    //            if (hitR.transform == startBtn)
    //            {
    //                print("exit");
    //                OnExit(left, right);
    //            }
    //        }
    //    }
    //}

    #endregion

    public void OnStart()
    {
        mainUI.startDisappear(true);
        loadingUI.Appear();
        SceneManager.LoadScene(1);
    }


    //void OnExit(InputAction left, InputAction right)
    //{
    //    exitPannel.SetActive(true);

    //    if (left.triggered || right.triggered)
    //    {
    //        if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
    //        {
    //            if (hit.transform == exitY)
    //                EixtY();

    //            if (hit.transform == exitN)
    //                ExitN();
    //        }
    //        if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitR))
    //        {
    //            if (hitR.transform == exitY)
    //                EixtY();
    //            if (hitR.transform == exitN)
    //                ExitN();
    //        }
    //    }
    //}

    //void EixtY()
    //{
    //    Application.Quit();
    //}
    //void ExitN()
    //{
    //    exitPannel.SetActive(false);
    //}
}
