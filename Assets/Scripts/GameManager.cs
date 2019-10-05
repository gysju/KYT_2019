using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Tool m_toolPrefab;
    public int m_NumberOfPlayer = 2;

    [Space(5)]
    public Sprite m_ToolOne;
    public Sprite m_ToolTwo;
    public Sprite m_ToolThree;

    private Transform m_ToolParent;

    void Start()
    {
        SpawnTools();
    }

    void SpawnTools()
    {
        m_ToolParent = new GameObject("[TOOLS]").transform;

        for (int j = 0; j < (int)MapManager.ETileType.Neutral; j++)
        {
            for (int i = 0; i < m_NumberOfPlayer; i++)
            {
                MapManager.TileCase tileCase = new MapManager.TileCase();
                if (!MapManager.Instance.GetAvailableCase( ref tileCase))
                    continue;

                Tool tool = Instantiate(m_toolPrefab, 
                            new Vector3( tileCase.m_XCoord, tileCase.m_YCoord, 0.0f), 
                            Quaternion.identity,
                            m_ToolParent);

                tileCase.m_Owner = tool.gameObject;

                tool.m_ToolType = ((MapManager.ETileType)j);
                switch (tool.m_ToolType)
                {
                    case MapManager.ETileType.TypeOne:
                        tool.m_SpriteRenderer.sprite = m_ToolOne;
                        tool.m_Animator.SetTrigger("Axe");
                        break;
                    case MapManager.ETileType.TypeTwo:
                        tool.m_SpriteRenderer.sprite = m_ToolTwo;
                        tool.m_Animator.SetTrigger("Chisel");
                        break;
                    case MapManager.ETileType.TypeThree:
                        tool.m_SpriteRenderer.sprite = m_ToolThree;
                        tool.m_Animator.SetTrigger("Shovel");
                        break;
                    default:
                        break;
                }               
            }
        }
    }
}
