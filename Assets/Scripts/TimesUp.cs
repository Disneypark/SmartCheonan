using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class TimesUp : MonoBehaviour
{
    XRRayInteractor leftRayInteractor;
    XRRayInteractor rightRayInteractor;
    InputActionAsset xriInputAction;

    InputAction leftTrigger, rightTrigger;

    [SerializeField] Transform goToTitle;
    [SerializeField] Transform close;

    void Start()
    {
        leftRayInteractor = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
        rightRayInteractor = GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>();
        xriInputAction = Resources.Load<InputActionAsset>("XRI Default Input Actions");

        leftTrigger = xriInputAction.FindActionMap("XRI LeftHand").FindAction("Trigger");
        rightTrigger = xriInputAction.FindActionMap("XRI RightHand").FindAction("Trigger");
    }

    // Update is called once per frame
    void Update()
    {
        ExitY();
        ExitN();
    }


    void ExitY()
    {
        if (leftTrigger.triggered || rightTrigger.triggered)
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                if (hit.transform == goToTitle)
                {
                    print("go to title");
                    GotoTitle();
                }
            }
            if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitR))
            {
                if (hitR.transform == goToTitle)
                {
                    print("go to title");

                    GotoTitle();
                }
            }
        }
    }

    void ExitN()
    {
        if (leftTrigger.triggered || rightTrigger.triggered)
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {               
                if (hit.transform == close)
                {
                    print("close");
                    CloseTab();
                }
            }
            if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hitR))
            {
                if (hitR.transform == close)
                {
                    print("close");

                    CloseTab();
                }
            }
        }
    }


    void GotoTitle()
    {
        SceneManager.LoadScene(0);
    }
    void CloseTab()
    {
        gameObject.SetActive(false);
    }
}
