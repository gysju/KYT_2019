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

    public void SetOwner( Player player, bool attach)
    {
        if (!attach)
        {
            m_Animator.SetBool("IsUsed", false);
            transform.parent = m_OriginalParent;
            MapManager.TileCase tile = MapManager.Instance.m_MapCoordonate[(int)player.GetPos().x][(int)player.GetPos().y];
            transform.position =  new Vector3( tile.m_XCoord, tile.m_YCoord);
            m_SpriteRenderer.sortingOrder = 1;
        }
        else
        {
            Owner = player.gameObject;
            m_Animator.SetBool("IsUsed", true);
            transform.parent = player.m_WeaponsAnchor;
            transform.localPosition = Vector3.zero;

            m_SpriteRenderer.sortingOrder = 2;
        }
    }
}
