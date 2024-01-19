using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageList : MonoBehaviour
{

    //public List<Sprite> equipList; // 무기 리스트
    public List<Sprite> expPotionImage; // 경험치 포션
    public List<Sprite> meterialsImage; // 재료
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
