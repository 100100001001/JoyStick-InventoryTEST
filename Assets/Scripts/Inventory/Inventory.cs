using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 싱글톤패턴 적용
    // 1. 누구나 손쉽게 접근
    // 2. 단, 하나만 존재
    public static Inventory instance;


    // slotCount의 변화를 알려줄 델리게이트(대리자) 정의
    public delegate void OnSlotCountChange(int value);

    // slotCount의 변화를 알려줄 델리게이트(대리자) 인스턴스화
    public OnSlotCountChange onSlotCountChange;


    // 아이템 추가시 슬롯UI에도 알려줄 델리게이트(대리자) 정의
    public delegate void OnChangeItem();
    // 아이템 추가시 슬롯UI에도 알려줄 델리게이트(대리자) 인스턴스화
    public OnChangeItem onChangeItem;


    // Slot의 개수를 지정할 변수
    private int slotCount;

    // slotCount 캡슐화 프로퍼티
    public int SlotCount
    {
        // 외부에서 값을 읽어올 경우 slotCount의 값을 넘겨줌
        get => slotCount;
        // 외부에서 값을 적용할 경우
        set
        {
            // slotCount에 입력값을 적용
            slotCount = value;

            // slotCount의 변화를 알려줄 델리게이트(대리자)를 호출
            onSlotCountChange.Invoke(slotCount);
            //onSlotCountChange(slotCount); // 위 아래 둘 중 하나로 실행

        }

    }


    // 획득한 아이템을 보관할(담을) List
    public List<Item> items = new List<Item>();


    private void Awake()
    {
        // 만약에 instance가 비어있지 않다면,
        if (instance != null)
        {
            // 현재 오브젝트를 파괴
            Destroy(gameObject);
            // 아래로 더 이상 내려가지 말고 돌아가
            return;
        }

        // instance에 현재 내 자신을 넣어줘
        instance = this;
    }

    void Start()
    {
        // SlotCount를 4로 초기화
        SlotCount = 4;
    }


    // items 리스트에 아이템을 추가할 수 있는 메서드
    public bool AddItem(Item item)
    {
        // 만약에 items(리스트)의 개수가 실제 사용가능한 slotCount(현재 활성된 슬롯)보다 작다면
        if (items.Count < SlotCount)
        {
            // items 리스트에 아이템 추가
            items.Add(item);
            // 만약에 onChangeItem이 비어있지 않다면,
            if (onChangeItem != null) onChangeItem.Invoke();

            return true; // 아이템 추가 성공 반환
        }

        return false;
    }

    // 플레이어와 필드 아이템 충돌처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 만약에 충돌한 상대의 태그가 FieldItem과 같다면,
        if (collision.CompareTag("FieldItem"))
            // (collision.tag == "FieldItem") 이거랑 같다
        {
            // 충돌한 아이템에서 FieldItems 컴포넌트를 담아줘(참조해줘)
            // 1. FieldItems에서 아이템의 정보(데이터)가 들어있다.
            FieldItem fieldItem = collision.GetComponent<FieldItem>();

            // 만약에 AddItem 메서드에 아이템의 정보를 보내서 아이템 추가에 성공한다면
            // 충돌한 아이템은 삭제해줘
            if (AddItem(fieldItem.GetItem())) fieldItem.DestroyItem();
        }
    }
}
