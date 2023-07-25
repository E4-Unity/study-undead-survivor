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
    
    /* 레퍼런스 */
    [Header("# Game Object")]
    [SerializeField] Player m_Player;
    [SerializeField] PoolManager m_PoolManager;
    [SerializeField] LevelUp m_LevelUp_UI;

    public Player GetPlayer() => m_Player;
    public PoolManager GetPoolManager() => m_PoolManager;
    protected LevelUp GetLevelUp() => m_LevelUp_UI;

    /* 필드 */
    [Header("# Game State")]
    [SerializeField, ReadOnly] bool m_IsPaused = false;
    [SerializeField, ReadOnly] float m_PlayTime;
    [SerializeField, ReadOnly] float m_MaxPlayTime = 20f;
    
    [Header("# Player State")]
    [SerializeField] int m_MaxHealth = 100;
    [SerializeField, ReadOnly] int m_Health;
    [SerializeField, ReadOnly] int m_Level;
    [SerializeField, ReadOnly] int m_Kill;
    [SerializeField, ReadOnly] int m_Exp;
    [SerializeField] int[] m_NextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    /* 프로퍼티 */
    public bool IsPaused => m_IsPaused;
    public float PlayTime => m_PlayTime;
    public float MaxPlayTime => m_MaxPlayTime;
    public int Health
    {
        get => m_Health;
        set => m_Health = value;
    }
    public int MaxHealth => m_MaxHealth;
    public int Level => m_Level;
    public int Kill => m_Kill;
    public int Exp => m_Exp;
    public int NextExp => m_NextExp[m_Level];

    /* 메서드 */
    public void PauseGame()
    {
        m_IsPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        m_IsPaused = false;
        Time.timeScale = 1;
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
            m_LevelUp_UI.Show();
        }
    }

    /* MonoBehaviour */
    void Awake()
    {
        Instance = this; 
    }

    void Start()
    {
        m_Health = m_MaxHealth;

        //TODO 임시로 캐릭터에게 무기를 쥐어줌
        m_LevelUp_UI.Select(0);
    }

    void Update()
    {
        // 게임 정지
        if (IsPaused) return;

        m_PlayTime += Time.deltaTime;

        if (m_PlayTime > m_MaxPlayTime)
        {
            m_PlayTime = m_MaxPlayTime;
        }
    }
}
