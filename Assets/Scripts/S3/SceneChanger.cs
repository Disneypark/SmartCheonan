using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] XRRayInteractor rightRayInteractor;
    [SerializeField] XRRayInteractor leftRayInteractor;
    [SerializeField] InputActionAsset xriInputAction;
    [SerializeField] Transform collider;

    InputAction rightTrigger, leftTrigger;
    public enum ButttonType { Trigger };
    public ButttonType butttonType;

    void Start()
    {
        rightTrigger = xriInputAction.FindActionMap("XRI RightHand").FindAction("Trigger");
        leftTrigger = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Trigger");
    }

    void Update()
    {
        //어떤 버튼 이용할지 선택하기
        switch (butttonType)
        {
            case ButttonType.Trigger:
                GetButton(rightTrigger,leftTrigger);
                break;
        }
    }
    void GetButton(InputAction right, InputAction left)
    {
        Debug.Log("1");
        if (right.triggered || left.triggered)
        {
            Debug.Log("2");

            if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitR))
            {
                Debug.Log("3");
                if (hitR.transform.CompareTag("Changer"))
                {
                    SceneManager.LoadScene(2);
                    Debug.Log("4");
                }
            }

            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                Debug.Log("3");
                if (hit.transform.CompareTag("Changer"))
                {
                    SceneManager.LoadScene(2);
                    Debug.Log("4");
                }
            }
        }
    }
}