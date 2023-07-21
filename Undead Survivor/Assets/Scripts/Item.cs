using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData m_Data;
    [SerializeField] int m_Level;
    [SerializeField, ReadOnly] Weapon m_Weapon;
    [SerializeField, ReadOnly] Gear m_Gear;

    Image m_Icon;
    Text m_TextLevel;

    void Awake()
    {
        m_Icon = GetComponentsInChildren<Image>()[1];
        m_Icon.sprite = m_Data.ItemIcon;
        
        m_TextLevel = GetComponentsInChildren<Text>()[0];
    }

    void LateUpdate()
    {
        m_TextLevel.text = "Lv." + m_Level;
    }

    public void OnClick()
    {
        switch (m_Data.Type)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (m_Level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    m_Weapon = newWeapon.AddComponent<Weapon>();
                    m_Weapon.Init(m_Data);
                }
                else
                {
                    float nextDamage = m_Data.BaseDamage * (1 + m_Data.Damages[m_Level]);
                    int nextCount = m_Data.Counts[m_Level];
                    
                    m_Weapon.LevelUp(Mathf.FloorToInt(nextDamage), nextCount);
                }
                m_Level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (m_Level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    m_Gear = newWeapon.AddComponent<Gear>();
                    m_Gear.Init(m_Data);
                }
                else
                {
                    float nextRate = m_Data.Damages[m_Level];
                    m_Gear.LevelUp(nextRate);
                }
                m_Level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.Get().Health = GameManager.Get().MaxHealth;
                break;
        }

        if (m_Level == m_Data.Damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
