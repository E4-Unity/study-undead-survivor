using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    // 에디터에서 설정
    [SerializeField] GameObject[] m_Prefabs;
    
    // 상태
    PoolInstance[] m_Pools;
    
    // MonoBehaviour
    void Awake()
    {
        m_Pools = new PoolInstance[m_Prefabs.Length];

        for (int i = 0; i < m_Prefabs.Length; i++)
        {
            m_Pools[i] = new PoolInstance(m_Prefabs[i]);
        }
    }

    public IObjectPool<GameObject> GetPool(GameObject _prefab)
    {
        foreach (var pool in m_Pools)
        {
            if (pool.Prefab == _prefab)
                return pool.Get();
        }

        return null;
    }
}

public class PoolInstance
{
    readonly GameObject m_Prefab;
    readonly IObjectPool<GameObject> m_Pool;

    public GameObject Prefab => m_Prefab;
    public IObjectPool<GameObject> Get() => m_Pool;

    public PoolInstance(GameObject _prefab,  bool _collectionCheck = true, int _defaultCapacity = 10, int _maxSize = 10000)
    {
        // 변수 설정
        m_Prefab = _prefab;

        // 오브젝트 풀 생성
        m_Pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy, _collectionCheck, _defaultCapacity,
            _maxSize);
    }
    
    GameObject OnCreate()
    {
        GameObject instance = UnityEngine.Object.Instantiate(m_Prefab);
        instance.SetActive(false);
        
        return instance;
    }

    void OnGet(GameObject _instance)
    {
        //_instance.SetActive(true);
        // 선택 옵션으로 바꿀 예정
    }
    
    void OnRelease(GameObject _instance)
    {
        _instance.SetActive(false);
    }
    
    void OnDestroy(GameObject _instance)
    {
        
    }
}