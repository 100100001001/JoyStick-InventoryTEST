using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ItemEffect는 추상 클래스이면서 ScriptableObject를 상속 받음
// 추상화 : 자세하게 묘사하는 것이 아니라 기본 형태만 보여준다.
// 위키백과 : 복잡한 자료, 모듈, 시스템 등으로부터 핵심적인 개념 또는 기능을 간추려 내는 것을 말함
// 예를 들어 그림을 그릴 때 간단한 러프안을 제시하는 형태


// abstract(추상 클래스) 지정 한정하는 키워드
public abstract class ItemEffect : ScriptableObject
{
    // abstract(추상 메서드)
    public abstract bool ExecuteRole();

}
