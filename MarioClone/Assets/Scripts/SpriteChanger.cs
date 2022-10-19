using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;
    public SpriteRenderer render;
    private float wait;
    private int m_IndexSprite = 0;

    void Start()
    {
        wait = m_Speed;
    }

    void Update()
    {
        wait -= Time.deltaTime;
        if (wait <= 0)
        {
            if (m_IndexSprite >= m_SpriteArray.Length)
            {
                m_IndexSprite = 0;
            }
            render.sprite = m_SpriteArray[m_IndexSprite];
            m_IndexSprite += 1;
            wait = m_Speed;
        }
    }
}
