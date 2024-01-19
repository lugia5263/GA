
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagePopUpGenerator : MonoBehaviour
{

    public static DamagePopUpGenerator current;
    public GameObject prefab;


    private void Awake()
    {
        current = this;
    }

    public void CreatePopup(Vector3 position, string text, Color color)
    {
        var popup = Instantiate(prefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        //±Û¾¾»ö
        temp.faceColor = color;

        Destroy(popup, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            CreatePopup(Vector3.one, Random.Range(0, 1000).ToString(), Color.yellow);
        }
    }
}
