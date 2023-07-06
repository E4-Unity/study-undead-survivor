using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Enemy : MonoBehaviour
{
    // 에디터 할당
    [SerializeField] float m_Speed = 2.5f;
    [SerializeField] Rigidbody2D m_Target;
    [SerializeField] int m_MaxHealth;
    [SerializeField] SpriteLibraryAsset[] m_SpriteLibraryAssets;

    // 상태
    bool bIsDead;
    int m_Health;
    
    // 컴포넌트
    Rigidbody2D m_Rigidbody;
    SpriteRenderer m_SpriteRenderer;
    SpriteLibrary m_SpriteLibrary;
    
    // 버퍼
    Vector2 position;
    Vector2 dir;
    
    // MonoBehaviour
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteLibrary = GetComponent<SpriteLibrary>();
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

    void OnEnable()
    {
        // 부활
        bIsDead = false;
        m_Health = m_MaxHealth;
        
        // 목표물을 플레이어로 설정
        m_Target = GameManager.Get().GetPlayer().GetComponent<Rigidbody2D>();
    }

    public void Init(SpawnData _spawnData)
    {
        m_SpriteLibrary.spriteLibraryAsset = m_SpriteLibraryAssets[_spawnData.Type];
        // Sprite Renderer도 변경하면 좋을듯
        m_Speed = _spawnData.Speed;
        m_MaxHealth = _spawnData.Health;
        m_Health = m_MaxHealth; // TODO 중복 처리 안 하는 방법?
    }
    
    [Serializable]
    public class SpawnData
    {
        [SerializeField] int m_Type;
        [SerializeField] float m_SpawnTime;
        [SerializeField] int m_Health;
        [SerializeField] float m_Speed;

        public int Type => m_Type;
        public float SpawnTime => m_SpawnTime;
        public int Health => m_Health;
        public float Speed => m_Speed;
    }
}
