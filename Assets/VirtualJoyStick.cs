using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 오브젝트의 터치(상호작용)와 관련된 기능들의 이름 공간(라이브러리)

public class VirtualJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float sensitivity = 1f; // 조작 민감도

    private Image imageBackground; // 조이스틱 UI 중 배경 이미지 변수
    private Image imageController; // 조이스틱 UI 중 컨트롤러(핸들) 이미지 변수
    private Vector2 touchPosition; // 조이스틱의 방향정보

    // 프로퍼티 실제 외부로 보낼 프로퍼티 구축
    public float horizontal { get { return touchPosition.x * sensitivity; } }
    public float vertical { get { return touchPosition.y * sensitivity; } }

    private void Awake()
    {
        imageBackground = GetComponent<Image>();
        imageController = transform.GetChild(0).GetComponent<Image>();
    }

    // IPointerDownHandler 인터페이스를 부모로 상속받을 경우 구현해야되는 메서드
    // 해당 스크립트를 가지고 있는 오브젝트를 터치했을 때 메소드가 1회 실행됨
    // check Point : 터치 시작 시 1회 실행
    public void OnPointerDown(PointerEventData eventData)
    {
        Color a = new Color(1, 1, 1, 0.5f);
        imageBackground.color = a;
        imageController.color = a;
    }

    // IDragHandler 인터페이스를 부모로 상속받을 경우 구현해야되는 메서드
    // 해당 스크립트를 가지고 있는 오브젝트를 터치 상태에서 드래그 했을 때 메소드가
    // 매 프래임마다 실행
    // Check Point : 터치 후 드래그 상태일 때 매 프레임 실행
    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        // 조이스틱의 위치가 어디에 있든 동일한 값을 연산하기 위해
        // 'touchPosition' 의 위치 값은 이미지의 현재 위치를 기준으로
        // 얼마나 떨어져 있는지에 따라 다르게 나옴
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            //--- touchPosition 값의 1차 정규화 [ 0 ~ 1 ]
            // touchPosition을 이미지 크기로 나눔
            touchPosition.x = (touchPosition.x / imageBackground.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / imageBackground.rectTransform.sizeDelta.y);

            // Debug.Log("X : " + touchPosition.x);
            // Debug.Log("Y : " + touchPosition.y);


            //--- touchPosition 값을 2차 정규화 [ -n ~ +n ]
            // 왼쪽(-1), 중심(0), 오른쪽(1)로 변경하기 위해 touchPosition.x * 2 - 1
            // 아래(-1), 중심(0), 위(1)로 변경하기 위해 touchPosition.y * 2 - 1
            // 이 수식은 Pivot에 따라 달라짐(좌하단 Pivot 기준)
            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);
            //touchPosition = new Vector2(touchPosition.x*2+1, touchPosition.y*2+1); // 만약 우측하단에 있다면


            //--- touchPosition 값을 3차 정규화
            // 가상 조이스틱 배경 이미지 밖으로 터치가 나가게 되면 -1 ~ 1 보다 큰 값이
            // 나올 수 없게 normalized를 이용해 -1 ~ 1 사이의 값으로 정규화
            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;


            // 가상 조이스틱 컨트롤러 이미지 이동
            // touchPosition은 -1 ~ 1 사이의 데이터이기 때문에 그대로 사용하게 되면,
            // 컨트롤러의 움직임을 보기 힘들다
            // 그러니 배경 크기를 곱해서 사용하자
            // (단, 중심을 기준으로 왼쪽 -1, 오른쪽 1 이기 대문에 배경 크기의 절반을 곱함)
            // 컨트롤러가 배경 이미지 바깥으로 튀어나가게 하고 싶지 않다면,
            // 나눠주는 값을 더 크게 설정하면 된다
            Vector2 controllerPosition = new Vector2(touchPosition.x * imageBackground.rectTransform.sizeDelta.x / 3,
                                                     touchPosition.y * imageBackground.rectTransform.sizeDelta.y / 3);

            imageController.rectTransform.anchoredPosition = controllerPosition;

        }
    }

    // IPointerUpHandler 인터페이스를 부모로 상속받을 경우 구현해야되는 메서드
    // 해당 스크립트를 가지고 있는 오브젝트를 터치하였다가 떼었을 때 메소드가 1회 실행
    // (터치 시작과 연결성이 있다 !!!)
    // Check Point : 터치 종료 시 1회 실행
    public void OnPointerUp(PointerEventData eventData)
    {
        // 터치 종료 시 컨트롤러의 위치를 중앙으로 다시 옮김
        imageController.rectTransform.anchoredPosition = Vector2.zero;

        // 다른 오브젝트에서 이동 방향으로 사용하기 때문에 실제 적용할 이동 방향도 초기화
        touchPosition = Vector2.zero;
    }
}
