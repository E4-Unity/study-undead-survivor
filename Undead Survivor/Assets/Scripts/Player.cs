using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 에디터 설정
    [SerializeField] float m_Speed = 3f; 

    // 컴포넌트
    Rigidbody2D m_Rigidbody;
    
    // 버퍼
    Vector2 m_InputValue;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // 사용자 입력
        m_InputValue.x = Input.GetAxisRaw("Horizontal");
        m_InputValue.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // 움직임
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Speed * Time.fixedDeltaTime * m_InputValue.normalized);
    }
}
