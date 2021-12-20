using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float curTime;

    GameObject popUpPrefab;
    GameObject popUp;

    public GameObject cam;

    [SerializeField] float TimesUpFloat = 120;

    private void Start()
    {
        popUpPrefab = Resources.Load<GameObject>("Prefabs/TimesUpUI");
    }
    bool isActive;
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > TimesUpFloat)
        {
            if (!isActive)
            {
                isActive = true;
                print("time over");
                cam = GameObject.FindGameObjectWithTag("MainCamera");
                popUp = GameObject.Instantiate(popUpPrefab,cam.transform);

                //popUp.transform.position = Vector3.zero;
                //popUp.transform.eulerAngles = Vector3.zero;
            }

            //popUppre = popUp;
        }
        //if (popUp != null)
        //{
        //    popUp.transform.parent = cam.transform;
        //}
    }
}
