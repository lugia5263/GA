using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterClass
{
    Warrior, Gunner, Magician
}

public class SelectChar : MonoBehaviour
{
    public static string currentCharacter;
    public static int CharNum;
    public CharacterClass character;
    public SelectChar[] chars;

    public void OnClickCharacterBtn()
    {
        // �ش� ��ư�� character�� ���� ��ũ��Ʈ�� currentCharacter�� �Ҵ�
        currentCharacter = character.ToString();

        // ���Թ�ȣ�� �ٸ���ũ��Ʈ�� �Ѱ��ֱ�
        switch (currentCharacter)
        {
            case "Warrior":
                CharNum = 0;
                break;
            case "Gunner":
                CharNum = 1;
                break;
            case "Magician":
                CharNum = 2;
                break;
            default:
                break;
        }

        // �Ҵ�� ���� Ȯ��
        Debug.Log("Selected Character: " + currentCharacter);
    }
}