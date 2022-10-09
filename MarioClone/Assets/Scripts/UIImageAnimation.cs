using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageAnimation : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;
    public float delay;
    private float wait;
    private float delayWait = 0;
    private int m_IndexSprite = 1;
    private bool isDone;

    public void Start()
    {
        wait = m_Speed;
    }

    public void Update()
    {
        if (delay > 0)
        {
            delayWait -= Time.unscaledDeltaTime;
            if (delayWait <= 0)
            {
                PlayAnim();
            }
        } else
        {
            PlayAnim();
        }                    
    }

    public void PlayAnim()
    {
        wait -= Time.unscaledDeltaTime;
        if (wait <= 0)
        {
            if (m_IndexSprite >= m_SpriteArray.Length)
            {
                m_IndexSprite = 0;
                delayWait = delay;
            }
            m_Image.sprite = m_SpriteArray[m_IndexSprite];
            m_IndexSprite += 1;
            wait = m_Speed;
        }
    }
}
