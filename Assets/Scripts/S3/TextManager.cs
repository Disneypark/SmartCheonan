using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    TypingClass typingClass;
    void Start()
    {
        typingClass = GetComponentInChildren<TypingClass>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "mercury") typingClass.originText = typingClass.mercuryText;
        if (other.gameObject.name == "venus") typingClass.originText = typingClass.venusText;
        if (other.gameObject.name == "earth") typingClass.originText = typingClass.earthText;
        if (other.gameObject.name == "mars") typingClass.originText = typingClass.marsText;
        if (other.gameObject.name == "jupiter") typingClass.originText = typingClass.jupiterText;
        if (other.gameObject.name == "saturn") typingClass.originText = typingClass.saturnText;
        if (other.gameObject.name == "uranus") typingClass.originText = typingClass.uranusText;
        if (other.gameObject.name == "neptune") typingClass.originText = typingClass.neptuneText;

        typingClass.StopCoroutine("TypingAction");
        typingClass.StartCoroutine("TypingAction");

        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        typingClass.DialogText.text = "";
    }
}
