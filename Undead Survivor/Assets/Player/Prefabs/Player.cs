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
    SpriteRenderer m_Renderer;
    Animator m_Animator;
    
    // 버퍼
    Vector2 m_InputValue;
    
    /* 프로퍼티 */
    public Vector2 InputValue => m_InputValue;
    
    /* Input System */
    void OnMove(InputValue _inputValue)
    {
        m_InputValue = _inputValue.Get<Vector2>();
    }

    /* MonoBehaviour */
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 움직임
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Speed * Time.fixedDeltaTime * m_InputValue.normalized);
    }

    void LateUpdate()
    {
        // 애니메이션 변수 업데이트
        m_Animator.SetFloat("Speed", m_InputValue.magnitude);
        
        // 움직임에 따라 바라보는 방향 전환
        if (m_InputValue.x == 0) return;
        m_Renderer.flipX = m_InputValue.x < 0;
    }
}
