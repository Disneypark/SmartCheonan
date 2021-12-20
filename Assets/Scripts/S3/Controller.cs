using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
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

    void Update()
    {
        //int layerMask = 1 << LayerMask.NameToLayer("Planet"); // Planet 레이어만 충돌 체크
        //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, layerMask);
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance);

        if (Input.GetKeyDown(KeyCode.Alpha1) && !planetMove && hit.transform.CompareTag("Planet")) planetMoveOn();
        if (Input.GetKeyDown(KeyCode.Alpha2) && !planetMove && hit.transform.CompareTag("Planet")) planetMoveOff();

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