using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu : ��Ʈ����Ʈ(�Ӽ�) �߰�
[CreateAssetMenu(menuName ="ItemEft/Cosumable/Health")]

// ItemEffect(�߻� Ŭ����) ���
public class ItemHealingEffect : ItemEffect
{
    public int heallingPoint = 0;

    public override bool ExecuteRole()
    {
        // �����ϰ� �α� ���
        Debug.Log("PlayerHP Add : " + heallingPoint);
        return true;
    }
}
