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


    // ������ �߰��� ����UI���� �˷��� ��������Ʈ(�븮��) ����
    public delegate void OnChangeItem();
    // ������ �߰��� ����UI���� �˷��� ��������Ʈ(�븮��) �ν��Ͻ�ȭ
    public OnChangeItem onChangeItem;


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


    // ȹ���� �������� ������(����) List
    public List<Item> items = new List<Item>();


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
        // SlotCount�� 4�� �ʱ�ȭ
        SlotCount = 4;
    }


    // items ����Ʈ�� �������� �߰��� �� �ִ� �޼���
    public bool AddItem(Item item)
    {
        // ���࿡ items(����Ʈ)�� ������ ���� ��밡���� slotCount(���� Ȱ���� ����)���� �۴ٸ�
        if (items.Count < SlotCount)
        {
            // items ����Ʈ�� ������ �߰�
            items.Add(item);
            // ���࿡ onChangeItem�� ������� �ʴٸ�,
            if (onChangeItem != null) onChangeItem.Invoke();

            return true; // ������ �߰� ���� ��ȯ
        }

        return false;
    }

    // �÷��̾�� �ʵ� ������ �浹ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���࿡ �浹�� ����� �±װ� FieldItem�� ���ٸ�,
        if (collision.CompareTag("FieldItem"))
            // (collision.tag == "FieldItem") �̰Ŷ� ����
        {
            // �浹�� �����ۿ��� FieldItems ������Ʈ�� �����(��������)
            // 1. FieldItems���� �������� ����(������)�� ����ִ�.
            FieldItem fieldItem = collision.GetComponent<FieldItem>();

            // ���࿡ AddItem �޼��忡 �������� ������ ������ ������ �߰��� �����Ѵٸ�
            // �浹�� �������� ��������
            if (AddItem(fieldItem.GetItem())) fieldItem.DestroyItem();
        }
    }
}
