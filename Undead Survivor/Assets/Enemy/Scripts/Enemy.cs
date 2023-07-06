using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 에디터 할당 변수
    [SerializeField] float m_Speed = 2.5f;
    [SerializeField] Rigidbody2D m_Target;

    // 컴포넌트
    Rigidbody2D m_Rigidbody;
    SpriteRenderer m_SpriteRenderer;

    // 상태
    bool bIsDead;
    
    // 버퍼
    Vector2 position;
    Vector2 dir;
    
    // MonoBehaviour
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {
        if (bIsDead) return;
        
        position = m_Rigidbody.position;
        dir = m_Target.position - position;
        Vector2 next = m_Speed * Time.fixedDeltaTime * dir.normalized;
        m_Rigidbody.MovePosition(position + next);
        m_Rigidbody.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        m_SpriteRenderer.flipX = dir.x < 0;
    }
}
