using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �̱������� ����
    // 1. ������ �ս��� ����
    // 2. ��, �ϳ��� ����
    public static Inventory instance;


    // slotCount�� ��ȭ�� �˷��� ��������Ʈ(�븮��) ����
    public delegate void OnSlotCountChange(int value);

    // slotCount�� ��ȭ�� �˷��� ��������Ʈ(�븮��) �ν��Ͻ�ȭ
    public OnSlotCountChange onSlotCountChange;


    // Slot�� ������ ������ ����
    private int slotCount;

    // slotCount ĸ��ȭ ������Ƽ
    public int SlotCount
    {
        // �ܺο��� ���� �о�� ��� slotCount�� ���� �Ѱ���
        get => slotCount;
        // �ܺο��� ���� ������ ���
        set
        {
            // slotCount�� �Է°��� ����
            slotCount = value;

            // slotCount�� ��ȭ�� �˷��� ��������Ʈ(�븮��)�� ȣ��
            onSlotCountChange.Invoke(slotCount);
            //onSlotCountChange(slotCount); // �� �Ʒ� �� �� �ϳ��� ����

        }

    }

    private void Awake()
    {
        // ���࿡ instance�� ������� �ʴٸ�,
        if (instance != null)
        {
            // ���� ������Ʈ�� �ı�
            Destroy(gameObject);
            // �Ʒ��� �� �̻� �������� ���� ���ư�
            return;
        }

        // instance�� ���� �� �ڽ��� �־���
        instance = this;
    }

    void Start()
    {
        // slotCount�� 4�� �ʱ�ȭ
        SlotCount = 4;
    }

    void Update()
    {
        
    }
}
