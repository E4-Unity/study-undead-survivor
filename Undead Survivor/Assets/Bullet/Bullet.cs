using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int m_Damage;
    [SerializeField] int m_Penetration;

    public int Damage => m_Damage;

    public void Init(int _damage, int _penetration)
    {
        m_Damage = _damage;
        m_Penetration = _penetration;
    }
    
}
