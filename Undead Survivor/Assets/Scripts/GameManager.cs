using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // static
    // 싱글톤
    static GameManager Instance;
    public static GameManager Get() => Instance;
    
    // 에디터 설정
    [Header("# Editor Config")]
    [SerializeField] Player m_Player;
    [SerializeField] PoolManager m_PoolManager;
    
    // 게임 상태
    [Header("# Game State")]
    [SerializeField, ReadOnly] float m_PlayTime;
    [SerializeField, ReadOnly] float m_MaxPlayTime = 20f;
    
    // 플레이어 상태
    [Header("# Player State")]
    [SerializeField] int m_MaxHealth = 100;
    [SerializeField, ReadOnly] int m_Health;
    [SerializeField, ReadOnly] int m_Level;
    [SerializeField, ReadOnly] int m_Kill;
    [SerializeField, ReadOnly] int m_Exp;
    [SerializeField] int[] m_NextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    
    // 프로퍼티
    public int Exp => m_Exp;
    public int NextExp => m_NextExp[m_Level];
    public int Level => m_Level;
    public int Kill => m_Kill;
    public float PlayTime => m_PlayTime;
    public float MaxPlayTime => m_MaxPlayTime;
    public int Health => m_Health;
    public int MaxHealth => m_MaxHealth;

    // Getter
    public Player GetPlayer() => m_Player;
    public PoolManager GetPoolManager() => m_PoolManager;
    public float GetPlayTime() => m_PlayTime;
    public float GetMaxPlayTime() => m_MaxPlayTime;
    
    // MonoBehaviour
    void Awake()
    {
        Instance = this; 
    }

    void Start()
    {
        m_Health = m_MaxHealth;
    }

    void Update()
    {
        m_PlayTime += Time.deltaTime;

        if (m_PlayTime > m_MaxPlayTime)
        {
            m_PlayTime = m_MaxPlayTime;
        }
    }
    
    // Player
    public void GetExp()
    {
        m_Exp++;
        m_Kill++;

        if (m_Exp == m_NextExp[m_Level])
        {
            m_Exp = 0;
            m_Level++;
        }
    }
}
