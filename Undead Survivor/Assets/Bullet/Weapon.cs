using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int m_WeaponID;
    [SerializeField] GameObject m_Prefab;
    [SerializeField] int m_Damage;
    [SerializeField] int m_Count;
    [SerializeField] float m_Speed;

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
            bullet.GetComponent<Bullet>().Init(m_Damage, -1); // -1은 무한 관통
            
            // Bullet 활성화
            bullet.SetActive(true); // 나중에 IObjectPool에 Finish 추가 예정
        }
    }
}
