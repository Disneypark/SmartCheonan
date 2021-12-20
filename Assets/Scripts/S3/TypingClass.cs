using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingClass : MonoBehaviour
{
    // Showing UI Text
    public Text DialogText;

    // Main Text
    public string originText;

    // Planaet Text
    public string mercuryText;
    public string venusText;
    public string earthText;
    public string marsText;
    public string jupiterText;
    public string saturnText;
    public string uranusText;
    public string neptuneText;
    public string noneText;

    // SubString Variable
    string subText;

    void Start()
    {
        mercuryText = "수성\n지름: 4천 8백 km (지구의 0.38배)\n중력: 0.38g\n태양으로부터 거리: 5천만 km\n자전주기: 59일\n공전주기: 88일\n온도:179도";
        venusText = "금성\n지름: 2만 4천 km (지구의 0.95배)\n중력: 0.9g\n태양으로부터 거리: 1억 km\n자전주기: 243일\n공전주기: 224일\n온도: 467도";
        earthText = "지구\n지름: 1만 2천 8백 km\n중력: 1g\n태양으로부터 거리: 1억 5천만 km\n자전주기: 1일\n공전주기: 365일\n온도: 17도";
        marsText = "화성\n지름: 지구의 0.5배\n중력: 0.38g\n태양으로부터 거리: 2억 3천만km\n자전주기: 1일\n공전주기: 687일\n온도: -80도";
        jupiterText = "목성\n지름: 14만 km\n중력: 2.5g\n태양으로부터 거리: 7억 8천만km\n자전주기: 10시간\n공전주기: 12년\n온도: -148도";
        saturnText = "토성\n지름: 지구의 9.4배\n중력: 0.9g\n태양으로부터 거리: 14억 km\n자전주기: 10시간\n공전주기: 29년\n온도: -176도";
        uranusText = "천왕성\n지름: 지구의 4배\n중력: 0.9g\n태양으로부터 거리: 28억 km\n자전주기: 17시간\n공전주기: 84년\n온도: -215도";
        neptuneText = "해왕성\n지름: 지구의 3.9배\n중력: 1.14g\n태양으로부터 거리: 45억 km\n자전주기: 16시간\n공전주기: 165년\n온도: -214도";
        noneText = "";

        //StopCoroutine("TypingAction");
        //StartCoroutine("TypingAction");
    }

    // Used Coroutine
    public IEnumerator TypingAction()
    {
        DialogText.text = "";

        for (int i = 0; i < originText.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);

            subText += originText.Substring(0, i);
            DialogText.text = subText;
            subText = "";
        }
    }
}
