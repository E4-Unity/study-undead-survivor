using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 에디터 설정
    [SerializeField] float m_Speed = 3f; 

    // 컴포넌트
    Rigidbody2D m_Rigidbody;
    
    // 버퍼
    Vector2 m_InputValue;
    
    /* Input System */
    void OnMove(InputValue _inputValue)
    {
        m_InputValue = _inputValue.Get<Vector2>();
    }

    /* MonoBehaviour */
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 움직임
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Speed * Time.fixedDeltaTime * m_InputValue.normalized);
    }
}
