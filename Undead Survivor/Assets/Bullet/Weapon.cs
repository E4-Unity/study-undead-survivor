using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 에디터 설정
    [SerializeField] int m_WeaponID;
    [SerializeField] GameObject m_Prefab;
    [SerializeField] int m_Damage;
    [SerializeField] int m_Count;
    [SerializeField] float m_Speed;

    // 상태
    float m_Timer;
    
    // 컴포넌트
    Player m_Player;
    Scanner m_Scanner;

    void Awake()
    {
        m_Scanner = GetComponentInParent<Scanner>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (m_WeaponID)
        {
            case 0:
                transform.Rotate(m_Speed * Time.deltaTime * Vector3.back);
                break;
            default:
                m_Timer += Time.deltaTime;
                if (m_Timer > m_Speed)
                {
                    m_Timer = 0;
                    Fire();
                }
                break;
        }
    }

    public void LevelUp(int _damage, int _count)
    {
        m_Damage = _damage;
        m_Count += _count;
        
        if(m_WeaponID == 0)
            Arrange();
    }

    public void Init()
    {
        switch (m_WeaponID)
        {
            case 0:
                m_Speed = 150;
                Arrange();
                break;
            default:
                m_Speed = 0.3f;
                break;
        }
    }

    void Arrange()
    {
        for (int i = 0; i < m_Count; i++)
        {
            GameObject bullet;
            if (i < transform.childCount)
                bullet = transform.GetChild(i).gameObject;
            else
            {
                // Bullet 스폰 및 배치
                bullet = GameManager.Get().GetPoolManager().GetPool(m_Prefab).Get();
                bullet.transform.parent = transform;
            }
            
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.identity;

            Vector3 rot = 360f * i / m_Count * Vector3.forward;
            bullet.transform.Rotate(rot);
            bullet.transform.Translate(bullet.transform.up * 1.5f, Space.World);
            
            // Bullet 설정
            bullet.GetComponent<Bullet>().Init(m_Damage, -1, Vector3.zero); // -1은 무한 관통
            
            // Bullet 활성화
            bullet.SetActive(true); // 나중에 IObjectPool에 Finish 추가 예정
        }
    }

    void Fire()
    {
        // 목표물 확인
        if (!m_Scanner.MainTarget) return;
        
        // Bullet 스폰
        GameObject bullet = GameManager.Get().GetPoolManager().GetPool(m_Prefab).Get();
        
        // Bullet 방향 및 속도 설정
        Vector3 position = transform.position;
        Vector3 dir = (m_Scanner.MainTarget.position - position).normalized;
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(m_Damage, m_Count, dir);

        // Bullet 활성화
        bullet.SetActive(true);
    }
}
