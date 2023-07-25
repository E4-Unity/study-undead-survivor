using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    /* 컴포넌트 */
    RectTransform m_RectTransform;
    Item[] m_Items;

    protected RectTransform GetRectTransform() => m_RectTransform;

    /* 메서드 */
    public void Show()
    {
        m_RectTransform.localScale = Vector3.one;
        GameManager.Get().PauseGame();
    }

    public void Hide()
    {
        m_RectTransform.localScale = Vector3.zero;
        GameManager.Get().ResumeGame();
    }

    public void Select(int _index)
    {
        m_Items[_index].OnClick();
    }

    /* MonoBehaviour */
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Items = GetComponentsInChildren<Item>(true);
    }
}
