using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_test : MonoBehaviour
{
    public void OnClickGoTest()
    {
        SceneManager.LoadScene("Test");
    }
    public void OnClickGoTest1()
    {
        SceneManager.LoadScene("Test 1");
    }
}
