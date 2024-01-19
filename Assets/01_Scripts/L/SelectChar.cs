using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectChar : MonoBehaviourPunCallbacks
{
    public Character character;
    public SelectChar[] chars;

    public void OnClickCharacterBtn()
    {
        DataMgr.instance.currentCharacter = character;
    }
}
