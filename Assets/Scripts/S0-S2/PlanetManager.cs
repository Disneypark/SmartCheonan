using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlanetManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] GameObject leftBtn;
    [SerializeField] GameObject rightBtn;

    [Header("Positions")]
    [SerializeField] Transform MainPosition;
    [SerializeField] Transform SubPositionLeft;
    [SerializeField] Transform SubPositionRight;

    [SerializeField] List<GameObject> planePrefabtList = new List<GameObject>();

    int listCount;

    int mainCount, rightCount, leftCount;
    void Start()
    {
        listCount = planePrefabtList.Count;

        rightCount = mainCount + 1;
        leftCount = mainCount - 1;

        CreatePlanet(MainPosition, mainCount);
        CreatePlanet(SubPositionRight, rightCount);
        CreatePlanet(SubPositionLeft, listCount - 1);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo))
        {
            if (Input.GetMouseButtonDown(0) && hitinfo.transform.gameObject == leftBtn)
            {
                OnClickArrow(-1);
            }

            if (Input.GetMouseButtonDown(0) && hitinfo.transform.gameObject == rightBtn)
            {
                OnClickArrow(+1);
            }
        }
    }
    void CreatePlanet(Transform pos, int idx)
    {
        //Instantiate 전에 남은 행성 Destroy , If문 넣은 이유는 start문에서 에러 방지
        if (pos.childCount != 0)
            Destroy(pos.GetChild(0).gameObject);

        GameObject planet = GameObject.Instantiate(planePrefabtList[idx]);
        planet.transform.position = pos.position;
        planet.transform.parent = pos;

        //가운데에 있는 행성에 Grab 기능 추가
        // if (pos.name.Contains("Main"))
        {
            //  planet.AddComponent(typeof(XRGrabInteractable));
        }
    }
    void OnClickArrow(int count)
    {
        mainCount += count; rightCount += count; leftCount += count;

        mainCount = (mainCount + listCount) % listCount;
        leftCount = (leftCount + listCount) % listCount;
        rightCount = (rightCount + listCount) % listCount;

        CreatePlanet(MainPosition, mainCount);
        CreatePlanet(SubPositionRight, rightCount);
        CreatePlanet(SubPositionLeft, leftCount);
    }
}
