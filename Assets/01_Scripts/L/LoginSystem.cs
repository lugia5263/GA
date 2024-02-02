using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public static string userEmail;
    public string password;
    public InputField userIDInput;
    //public InputField pwInput;

    public Text outputText;

    public void OnClickCreateBtn()
    {
        Debug.Log("ȸ�����Թ�ư ����");
        if (string.IsNullOrEmpty(userIDInput.text))
        {
            Debug.Log("userID�� �Է����ּ���!");
        }
        else
        {
            // PlayerPrefs�� userID ����
            PlayerPrefs.SetString("UserID", userIDInput.text);
            PlayerPrefs.Save();
        }
    }

    public void OnClickLoginBtn()
    {
        Debug.Log("�α��ι�ư ����");
        if (string.IsNullOrEmpty(userIDInput.text))
        {
            Debug.Log("userID�� �Է����ּ���!");
        }
        else
        {
            if (PlayerPrefs.HasKey("UserID"))
            {
                SceneManager.LoadScene("ChSelect");
            }
            else
            {
                Debug.Log("userID�� �����ϴ�. ȸ�������� ���ּ���");
            }
        }
    }
}
