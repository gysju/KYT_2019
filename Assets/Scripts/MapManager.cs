using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [SerializeField] private int m_SizeX = 10;
    [SerializeField] private int m_SizeY = 10;

    [SerializeField] private float m_TileSizeX = 1.0f;
    [SerializeField] private float m_TileSizeY = 1.0f;

    public Vector3[][] m_MapCoordonate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            IniteMap();
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public int GetMapSizeX()
    {
        return m_SizeX;
    }

    public int GetMapSizeY()
    {
        return m_SizeY;
    }

    private void IniteMap()
    {
        m_MapCoordonate = new Vector3[m_SizeX][];
        for (int i = 0; i < m_MapCoordonate.Length; i++)
        {
            m_MapCoordonate[i] = new Vector3[m_SizeY];
            for (int j = 0; j < m_MapCoordonate[i].Length; j++)
            {
                m_MapCoordonate[i][j] = new Vector3( (i * m_TileSizeX - ((m_SizeX - 1) * m_TileSizeX * 0.5f )),
                                                     (j * m_TileSizeY - ((m_SizeY - 1) * m_TileSizeY * 0.5f)), 
                                                     0.0f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_MapCoordonate == null)
            return;

        Gizmos.color = Color.red;

        for (int i = 0; i < m_MapCoordonate.Length; i++)
        {
            for (int j = 0; j < m_MapCoordonate[i].Length; j++)
            {
                Gizmos.DrawWireSphere(m_MapCoordonate[i][j], 1.0f);
            }
        }
    }
}
