using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Enemy : MonoBehaviour
{
    // 상태
    [SerializeField, ReadOnly] Rigidbody2D m_Target;
    [SerializeField, ReadOnly] int m_Type = 0;
    [SerializeField, ReadOnly] bool bIsDead;
    [SerializeField, ReadOnly] int m_Health;
    
    // 에디터 할당
    [SerializeField] EnemyData[] m_EnemyData;

    // 컴포넌트
    Rigidbody2D m_Rigidbody;
    SpriteRenderer m_SpriteRenderer;
    SpriteLibrary m_SpriteLibrary;
    Animator m_Animator;
    Collider2D m_Collider;
    
    // 프로퍼티
    float Speed => m_EnemyData[m_Type].Speed;

    int Health
    {
        get => m_Health;
        set
        {
            if(value > MaxHealth)
                m_Health = MaxHealth;
            else if (value <= 0)
            {
                m_Health = 0;
                bIsDead = true;
                Dead();
            }
            else
                m_Health = value;
        }
    }
    int MaxHealth => m_EnemyData[m_Type].MaxHealth;
    SpriteLibraryAsset GetSpriteLibraryAsset() => m_EnemyData[m_Type].GetSpriteLibraryAsset();
    
    // 버퍼
    Vector2 position;
    Vector2 dir;
    
    // MonoBehaviour
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteLibrary = GetComponent<SpriteLibrary>();
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
    }
    
    void FixedUpdate()
    {
        if (bIsDead) return;
        
        position = m_Rigidbody.position;
        dir = m_Target.position - position;
        Vector2 next = Speed * Time.fixedDeltaTime * dir.normalized;
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
        Revive();

        // 목표물을 플레이어로 설정
        m_Target = GameManager.Get().GetPlayer().GetComponent<Rigidbody2D>();
    }

    public void Init(SpawnData _spawnData)
    {
        m_Type = Mathf.Min(_spawnData.Type, m_EnemyData.Length - 1);
        m_SpriteLibrary.spriteLibraryAsset = GetSpriteLibraryAsset();
    }
    
    void OnTriggerEnter2D(Collider2D _other)
    {
        if (bIsDead) return;
        if (!_other.CompareTag("Bullet")) return;

        Health -= _other.GetComponent<Bullet>().Damage;
        m_Animator.SetTrigger("Hit");
    }

    void Dead()
    {
        bIsDead = true;
        m_Animator.SetBool("Dead", bIsDead);
        m_Collider.enabled = false;

        // TODO 나중에 그림자는 지우거나 위치 조정이 필요해 보임
        foreach(var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            spriteRenderer.sortingLayerName = "Dead";
    }

    void Revive()
    {
        bIsDead = false;
        //m_Animator.SetBool("Dead", bIsDead);
        m_Collider.enabled = true;
        
        foreach(var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            spriteRenderer.sortingLayerName = "Enemy";

        m_Health = MaxHealth;
    }
}
