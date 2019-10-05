using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int m_NumberOfPlayer = 2;

    [Space(5)]
    public Tool m_AxePrefab;
    public Tool m_ChiselPrefab;
    public Tool m_ShovelPrefab;

    private Transform m_ToolParent;

    void Start()
    {
        SpawnTools();
    }

    void SpawnTools()
    {
        m_ToolParent = new GameObject("[TOOLS]").transform;

        SpawnToolByType(m_AxePrefab);
        SpawnToolByType(m_ChiselPrefab);
        SpawnToolByType(m_ShovelPrefab);
    }

    void SpawnToolByType(Tool toolPrefab)
    {
        for (int i = 0; i < m_NumberOfPlayer; i++)
        {
            MapManager.TileCase tileCase = new MapManager.TileCase();
            if (!MapManager.Instance.GetAvailableCase(ref tileCase))
                continue;

            Tool tool = Instantiate(toolPrefab,
                        new Vector3(tileCase.m_XCoord, tileCase.m_YCoord, 0.0f),
                        Quaternion.identity,
                        m_ToolParent);

            tool.CurrentTileCase = tileCase;
            tileCase.m_Owner = null;
            tileCase.m_Weapon = tool;
        }
    }
}
