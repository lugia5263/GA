using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOut : MonoBehaviour
{
 

    public void SceneNextBtn()
    {
        SceneManager.LoadScene("EnforceScene");
    }
    public void SeeneTrophyBtn()
    {
        SceneManager.LoadScene("TrophyScene");

    }
}
