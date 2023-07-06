using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reposition : MonoBehaviour
{
    Collider2D m_Collider;

    void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (!_other.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.Get().GetPlayer().transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.Get().GetPlayer().InputValue;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if(diffX > diffY)
                    transform.Translate(dirX * 40 * Vector3.right);
                else if(diffX < diffY)
                    transform.Translate(dirY * 40 * Vector3.up);
                break;
            case "Enemy":
                if(m_Collider.enabled)
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                break;
        }
    }
}
