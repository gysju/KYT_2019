using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public MapManager.TileCase CurrentTileCase;
    public MapManager.ETileType m_ToolType;

    public SpriteRenderer m_SpriteRenderer;
    public Animator m_Animator;

    public Transform m_OriginalParent;
    private GameObject Owner;

    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    public void SetOwner( GameObject NewOwner)
    {
        Owner = NewOwner;

        if (Owner == null)
        {
            m_Animator.SetBool("IsUsed", false);
            transform.parent = m_OriginalParent;
        }
        else
        {
            m_Animator.SetBool("IsUsed", true);
            transform.parent = Owner.transform;
        }
    }
}
