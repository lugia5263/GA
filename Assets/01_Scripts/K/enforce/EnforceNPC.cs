using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnforceNPC : MonoBehaviour
{
    public StateManager player_stateMgr;
    public EnforceMgr enforceMgr;

    private void Start()
    {
        enforceMgr = GameObject.Find("EnforceMgr").GetComponent<EnforceMgr>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        { //TODO: ��Ƽ������ �÷��̾� ��ôܰ� �����;���!!
            //enforceMgr.OnEnforcePanel(other.transform.GetComponent<StateManager>().def);
        }
    }
}
