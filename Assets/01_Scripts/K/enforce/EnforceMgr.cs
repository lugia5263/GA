using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Photon.Pun;

public class EnforceMgr : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public TextAsset forcetxtFile; //Jsonfile

    public StateManager stateMgr;
    public Button enBtn;

    [Header("��ȭ�Ͻðڽ��ϱ�?")]
    public GameObject enforcePanel;
    public Text weaponNowTxt;
    public Text weaponAftTxt;
    public Text wantEnforceTxt;
    public Text needMaterial;
    public Text needGold;

    public GameObject enforceEffect;
    public Jun_TweenRuntime lessTween;

    [Header("��ü���")]
    public GameObject successPanel;
    public Text successweaponNowTxt;
    public Text beforeAtk;
    public Text afterAtk;

    [Header("��ý���")]
    public GameObject failedPanel;
    public Text failweaponNowTxt;
    public Text beforeAtkF;
    public Text afterAtkF;

    [Header("��ȭ ���� ����")]
    public int playerWeaponLevel;
    public int playerGold;
    public int playerMaterial;
    public int playerAttackPower;

    [Header("NPC ��ȭ")]
    public DialogueTrigger dialogueTrigger; //�뺻
    public GameObject nPCConversation;



    private void Awake()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        
        var jsonitemFile = Resources.Load<TextAsset>("Json/EnforceTable");
        forcetxtFile = jsonitemFile;
        lessTween = GameObject.Find("lessTween").GetComponent<Jun_TweenRuntime>();
        enforcePanel = GameObject.Find("EnforcePanel");
        wantEnforceTxt = GameObject.Find("ReallyTxt").GetComponent<Text>();
        weaponNowTxt = GameObject.Find("ReadyBefore").GetComponent<Text>();
        weaponAftTxt = GameObject.Find("ReadyAfter").GetComponent<Text>();
        needMaterial = GameObject.Find("needMaterial").GetComponent<Text>();
        needGold = GameObject.Find("needGold").GetComponent<Text>();
        enforceEffect = GameObject.Find("EnforceEffect");

        successPanel = GameObject.Find("SuccessPanel");
        successweaponNowTxt = GameObject.Find("ForceLvS").GetComponent<Text>();
        beforeAtk = GameObject.Find("beforeAtk").GetComponent<Text>();
        afterAtk = GameObject.Find("afterAtk").GetComponent<Text>();


        failedPanel = GameObject.Find("FailedPanel");
        failweaponNowTxt = GameObject.Find("ForceLvF").GetComponent<Text>();
        beforeAtkF = GameObject.Find("beforeAtkF").GetComponent<Text>();
        afterAtkF = GameObject.Find("afterAtkF").GetComponent<Text>();
    }
    void Start()
    {
        playerWeaponLevel = dataMgrDontDestroy.WeaponLevel;
        playerMaterial = dataMgrDontDestroy.UserMaterial;
        playerGold = dataMgrDontDestroy.UserGold;
        playerAttackPower = dataMgrDontDestroy.AttackPower;
        enforceEffect.SetActive(false);
        successPanel.SetActive(false);
        failedPanel.SetActive(false);
        enforcePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("�浹�Ͼ");
                dialogueTrigger.Trigger(); // �뺻 ������
                nPCConversation.SetActive(true); // ��ȭâ ����
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void OnEnforcePanelBtn() // F ��ȭ�ϱ� ��ư ������ ��ȭ
    {
        OnEnforcePanel();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                StateManager stateManager = other.gameObject.GetComponent<StateManager>();
                stateManager.weaponLevel = playerWeaponLevel;
                stateManager.userMaterial = playerMaterial;
                stateManager.userGold = playerGold;
                stateManager.attackPower = playerAttackPower;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)   
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                // ��ȭâ �����ϱ� �÷��̾��� ������ �ݿ�
                enforcePanel.SetActive(false);
            }
        }
    }

    public void OnEnforcePanel() // â�� ����, �÷��̾� ���������� ����
    {
        int replace = playerWeaponLevel - 1;

        string json = forcetxtFile.text;
        var jsonData = JSON.Parse(json);

        weaponNowTxt.text = $"���� ��� ��ġ{playerWeaponLevel} �ܰ�";
        weaponAftTxt.text = $"���� ��� ��ġ{playerWeaponLevel + 1} �ܰ�";
        wantEnforceTxt.text = $"��ȭ �Ͻðڽ��ϱ�? \n ��ȭ Ȯ���� {(jsonData["Enforce"][replace]["Rate"])}% �Դϴ�.";

        needMaterial.text = $"{playerMaterial}  /  {(jsonData["Enforce"][replace]["Material"])}";
        needGold.text = $"{playerGold}  /  { (jsonData["Enforce"][replace]["Gold"])}";
        //���⿡ ĳ���� ������ �´� ��ȭ �ʱ�ȭ!!!frg
        enforcePanel.SetActive(true);
    }



    public void EnforceBtn() 
    {
        string json = forcetxtFile.text;
        var jsonData = JSON.Parse(json);

        int needMat = (int)(jsonData["Enforce"][playerWeaponLevel - 1]["Material"]);
        int needGold = (int)(jsonData["Enforce"][playerWeaponLevel - 1]["Gold"]);
        
        if (playerMaterial >= needMat && playerGold >= needGold)
        {
            playerMaterial -= needMat;
            playerGold -= needGold;
            EnforcResult(playerWeaponLevel);
        }
        else
        {
            lessTween.Play();
        }
    }

    public void EnforcResult(int playerWeaponLv) // ��ȭ ��ư. GameObject Player?
    {
        StartCoroutine(EnforceEffect(playerWeaponLv));
    }

    IEnumerator EnforceEffect(int playerWeaponLv)
    {
        int replace = playerWeaponLv - 1;

        string json = forcetxtFile.text;
        var jsonData = JSON.Parse(json);

        int successRate = (int)(jsonData["Enforce"][replace]["Rate"]); //TODO: �̰Ŷ� �ؿ� 20�̶� ���̺��� ��������!!
        int randomNumbuer = Random.Range(0, 101);

        Debug.Log(successRate);
        Debug.Log(randomNumbuer);

        //ȿ����
        enforceEffect.SetActive(true);
        yield return new WaitForSeconds(3f);
        enforceEffect.SetActive(false);

        if (randomNumbuer < successRate)
        {
            //�ڷ�ƾ �־ ��ȭ���� Ȥ�� �÷��̾����� ������
            Debug.Log("��ȭ ����!");
            beforeAtk.text = playerAttackPower.ToString();
            afterAtk.text = (playerAttackPower + (int)(jsonData["Enforce"][replace]["PlusAtk"])).ToString();
            successPanel.SetActive(true);
            playerAttackPower += 20;
            playerWeaponLevel += 1;
            successweaponNowTxt.text = $"{jsonData["Enforce"][playerWeaponLv]["ForceLv"]} �ܰ�";
            //trophyMgr.TrophyIndexUp(3);
            InitAtk();
            SyncDataMgr();
        }
        else
        {
            //�ڷ�ƾ �־ ��ȭ���� Ȥ�� �÷��̾����� ������

            beforeAtkF.text = playerAttackPower.ToString();
            afterAtkF.text = playerAttackPower.ToString();
            Debug.Log("��ȭ ����!");
            failweaponNowTxt.text = $"{jsonData["Enforce"][playerWeaponLv]["ForceLv"]} �ܰ�";
            failedPanel.SetActive(true);
            //failEnforceCount++;
            //trophyMgr.TrophyIndexUp(4);
            InitAtk();
            SyncDataMgr();
        }
        //trophyMgr.TrophyIndexUp(2);
        yield return null;
    }

    public void InitAtk()
    {
        string json = forcetxtFile.text;
        var jsonData = JSON.Parse(json);
        Debug.Log(playerWeaponLevel);
        //playerWeaponLevel�� �̹� �˰�����
        //playerWeaponLv = inventoryMgr.weaponLv;

        wantEnforceTxt.text = $"��ȭ �Ͻðڽ��ϱ�? \n ��ȭ Ȯ���� {(jsonData["Enforce"][playerWeaponLevel - 1]["Rate"])}% �Դϴ�.";
        weaponNowTxt.text = $"���� ��� ��ġ{playerWeaponLevel} �ܰ�";
        weaponAftTxt.text = $"���� ��� ��ġ{playerWeaponLevel + 1} �ܰ�";
        
        beforeAtkF.text = playerAttackPower.ToString();
        afterAtk.text = playerAttackPower.ToString();

        needMaterial.text = $"{playerMaterial}  /  {(jsonData["Enforce"][playerWeaponLevel - 1]["Material"])}";
        needGold.text = $"{playerGold}  /  { (jsonData["Enforce"][playerWeaponLevel - 1]["Gold"])}";
    }

    public void SyncDataMgr()
    {
        dataMgrDontDestroy.WeaponLevel = playerWeaponLevel;
        dataMgrDontDestroy.UserMaterial = playerMaterial;
        dataMgrDontDestroy.userGold = playerGold;
        dataMgrDontDestroy.AttackPower = playerAttackPower;
    }

    public void SuccesPanelOff()
    {
        successPanel.SetActive(false);
    }
    public void FailedPanelOff()
    {
        failedPanel.SetActive(false);
    }
}
