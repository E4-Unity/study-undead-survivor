using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_Prefabs;
    
    Transform[] m_SpawnPoints;
    IObjectPool<GameObject>[] m_Pools;
    float m_Timer;

    void Awake()
    {
        m_SpawnPoints = GetComponentsInChildren<Transform>();
        m_Pools = new IObjectPool<GameObject>[m_Prefabs.Length];
    }

    void Start()
    {
        for (int i = 0; i < m_Prefabs.Length; i++)
        {
            m_Pools[i] = GameManager.Get().GetPoolManager().GetPool(m_Prefabs[i]);
        }
    }

    void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > 0.2f)
        {
            Spawn();
            m_Timer = 0;
        }
    }

    void Spawn()
    {
        GameObject enemy = m_Pools[Random.Range(0, m_Prefabs.Length)].Get();
        // SetActive(true) 이후에 위치 변경인데, 그 전에 하는 방법은?
        enemy.transform.position = m_SpawnPoints[Random.Range(1, m_SpawnPoints.Length)].position;
    }
}
