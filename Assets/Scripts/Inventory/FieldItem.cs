using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    // 실제 적용할 Item 적용
    public Item m_item; // (클래스 안에 있는 멤버 변수에 m 을 붙인다)

    // 실제 적용할 Image 적용 Renderer
    public SpriteRenderer image;

    // 아이템 데이터 생성 메서드
    public void SetItem(Item item)
    {
        m_item.itemName = item.itemName;
        m_item.itemImage = item.itemImage;
        m_item.itemType = item.itemType;

        image.sprite = m_item.itemImage;
    }

    // 아이템 획득 시 실행될 메소드
    public Item GetItem()
    {
        return m_item;
    }

    // 아이템 획득 후 실행될 메소드
    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
