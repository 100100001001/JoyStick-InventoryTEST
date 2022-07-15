using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu : 애트리뷰트(속성) 추가
[CreateAssetMenu(menuName ="ItemEft/Cosumable/Health")]

// ItemEffect(추상 클래스) 상속
public class ItemHealingEffect : ItemEffect
{
    public int heallingPoint = 0;

    public override bool ExecuteRole()
    {
        // 간단하게 로그 출력
        Debug.Log("PlayerHP Add : " + heallingPoint);
        return true;
    }
}
