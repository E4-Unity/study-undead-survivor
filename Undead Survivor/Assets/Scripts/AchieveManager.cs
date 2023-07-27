using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    enum Achieve
    {
        UnlockPotato,
        UnlockBean
    }
    
    [Header("[Reference]")]
    [SerializeField] GameObject[] m_LockCharacters;
    [SerializeField] GameObject[] m_UnlockCharacters;

    Achieve[] m_Achieves;

    /* 메서드 */
    void Init()
    {
        // 저장 여부 확인
        PlayerPrefs.SetInt("MyData", 1);
        
        // 실제 저장 데이터
        foreach (var achieve in m_Achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    void UnlockCharacter()
    {
        for (int i = 0; i < m_LockCharacters.Length; i++)
        {
            string achieveName = m_Achieves[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achieveName) == 1;
            m_LockCharacters[i].SetActive(!isUnlock);
            m_UnlockCharacters[i].SetActive(isUnlock);
        }
    }

    void CheckAchieve(Achieve _achieve)
    {
        bool isAchieve = false;

        switch (_achieve)
        {
            case Achieve.UnlockPotato:
                isAchieve = GameManager.Get().Kill >= 10;
                break;
            case Achieve.UnlockBean:
                isAchieve = Mathf.Approximately(GameManager.Get().PlayTime, GameManager.Get().MaxPlayTime); // TODO 클리어 정보 따로 만들 예정
                break;
        }

        // 처음 달성한 경우에만 실행
        if (isAchieve && PlayerPrefs.GetInt(_achieve.ToString()) == 0)
        {
            PlayerPrefs.SetInt(_achieve.ToString(), 1);
        }
    }
    
    /* MonoBehaviour */
    void Awake()
    {
        m_Achieves = (Achieve[])Enum.GetValues(typeof(Achieve));
        
        if (!PlayerPrefs.HasKey("MyData"))
            Init();
    }

    void Start()
    {
        UnlockCharacter();
    }

    void LateUpdate()
    {
        foreach (var achieve in m_Achieves)
        {
            CheckAchieve(achieve);
        }
    }
}
