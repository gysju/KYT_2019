using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public MapManager.ETileType m_ToolType;
    public SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();    
    }
}
