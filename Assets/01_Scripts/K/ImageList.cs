using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageList : MonoBehaviour
{

    //public List<Sprite> equipList; // ���� ����Ʈ
    public List<Sprite> expPotionImage; // ����ġ ����
    public List<Sprite> meterialsImage; // ���
    public List<Sprite> goldImage;



    private void Awake()
    {
        expPotionImage.Add(null);
        meterialsImage.Add(null);
        goldImage.Add(null);



        for (int i = 1; i < 5; i++)
        {
            string jsonexpContent = $"itemImage/expPotion{i}";
            string jsonmatContent = $"itemImage/material{i}";
            string jsongoldContent = $"itemImage/gold{i}";

            expPotionImage.Add(Resources.Load<Sprite>(jsonexpContent));
            meterialsImage.Add(Resources.Load<Sprite>(jsonmatContent));
            goldImage.Add(Resources.Load<Sprite>(jsongoldContent));
        }
    }


}
