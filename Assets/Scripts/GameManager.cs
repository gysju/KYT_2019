using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Tool m_toolPrefab;
    public int m_NumberOfPlayer = 2;

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
                        break;
                    case MapManager.ETileType.TypeTwo:
                        tool.m_SpriteRenderer.sprite = m_ToolTwo;
                        break;
                    case MapManager.ETileType.TypeThree:
                        tool.m_SpriteRenderer.sprite = m_ToolThree;
                        break;
                    default:
                        break;
                }               
            }
        }
    }
}
