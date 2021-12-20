using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneEffect : MonoBehaviour
{
    [SerializeField] InterfaceAnimManager interfaceAnimManager;
    public void Appear()
    {
        interfaceAnimManager.startAppear(true);
    }
    public void Disappear()
    {
        interfaceAnimManager.startDisappear(true);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SC1_testing"))
        {
            Destroy(gameObject.transform.parent);
        }
    }
}
