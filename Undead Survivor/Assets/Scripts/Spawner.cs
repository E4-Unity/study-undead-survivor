using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    // 에디터 설정
    [SerializeField] GameObject[] m_Prefabs;
    [SerializeField] Enemy.SpawnData[] m_SpawnDataList;
    
    // 할당
    Transform[] m_SpawnPoints;
    IObjectPool<GameObject>[] m_Pools;
    
    // 버퍼
    float timer;
    int level;

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
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Get().GetPlayTime() / 10f), m_SpawnDataList.Length - 1);

        if (timer > m_SpawnDataList[level].SpawnTime)
        {
            Spawn();
            timer = 0;
        }
    }

    void Spawn()
    {
        GameObject enemy = m_Pools[0].Get(); // 나중에 prefab 기준으로 변경
        enemy.transform.position = m_SpawnPoints[Random.Range(1, m_SpawnPoints.Length)].position;
        enemy.GetComponent<Enemy>().Init(m_SpawnDataList[level]);
        enemy.SetActive(true);
    }
}