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
    [SerializeField] Player m_Player;
    [SerializeField] PoolManager m_PoolManager;
    
    // 게임 상태
    float m_PlayTime;
    float m_MaxPlayTime = 20f;

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

    void Update()
    {
        m_PlayTime += Time.deltaTime;

        if (m_PlayTime > m_MaxPlayTime)
        {
            m_PlayTime = m_MaxPlayTime;
        }
    }
}
