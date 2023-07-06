using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player m_Player;
    [SerializeField] PoolManager m_PoolManager;
    
    static GameManager Instance;
    public static GameManager Get() => Instance;
    public Player GetPlayer() => m_Player;
    public PoolManager GetPoolManager() => m_PoolManager;
    
    void Awake()
    {
        Instance = this; 
    }
}
