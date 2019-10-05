using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum ETileType
    {
        TypeOne,
        TypeTwo,
        TypeThree,
        Neutral = 3
    };
    public class TileCase
    {
        public GameObject m_Owner = null;
        public Tool m_Weapon = null;
        public ETileType m_TileType = ETileType.Neutral;

        public float m_XCoord = 0.0f;
        public float m_YCoord = 0.0f;

        public int m_X = 0;
        public int m_Y = 0;
    }
    public static MapManager Instance;

    [SerializeField] private int m_SizeX = 10;
    [SerializeField] private int m_SizeY = 10;

    [SerializeField] private float m_TileSizeX = 1.0f;
    [SerializeField] private float m_TileSizeY = 1.0f;

    public TileCase[][] m_MapCoordonate;
    private List<TileCase> m_ShuffleCoordonate = new List<TileCase>();

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
        m_MapCoordonate = new TileCase[m_SizeX][];
        for (int i = 0; i < m_MapCoordonate.Length; i++)
        {
            m_MapCoordonate[i] = new TileCase[m_SizeY];
            for (int j = 0; j < m_MapCoordonate[i].Length; j++)
            {
                m_MapCoordonate[i][j] = new TileCase();

                m_MapCoordonate[i][j].m_X = i;
                m_MapCoordonate[i][j].m_Y = j;

                m_MapCoordonate[i][j].m_XCoord = i * m_TileSizeX - ((m_SizeX - 1) * m_TileSizeX * 0.5f);
                m_MapCoordonate[i][j].m_YCoord = j * m_TileSizeY - ((m_SizeY - 1) * m_TileSizeY * 0.5f);

                m_ShuffleCoordonate.Add(m_MapCoordonate[i][j]);
            }
        }
        Shuffle(m_ShuffleCoordonate);
    }

    public bool GetAvailableCase( ref TileCase result)
    {
        for (int j = 0; j < m_ShuffleCoordonate.Count; j++)
        {
            if (m_ShuffleCoordonate[j].m_Owner == null)
            {
                result = m_ShuffleCoordonate[j];
                return true;
            }
        }
        return false;
    }


    public void Shuffle(List<TileCase> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            TileCase temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void OnDrawGizmos()
    {
        if (m_MapCoordonate == null)
            IniteMap();

        for (int i = 0; i < m_MapCoordonate.Length; i++)
        {
            for (int j = 0; j < m_MapCoordonate[i].Length; j++)
            {
                Vector3 pos = new Vector3(m_MapCoordonate[i][j].m_XCoord, m_MapCoordonate[i][j].m_YCoord, 0.0f);
                switch (m_MapCoordonate[i][j].m_TileType)
                {
                    case ETileType.Neutral:
                        Gizmos.color = Color.black;
                        break;
                    case ETileType.TypeOne:
                        Gizmos.color = Color.green;
                        break;
                    case ETileType.TypeTwo:
                        Gizmos.color = Color.red;
                        break;
                    case ETileType.TypeThree:
                        Gizmos.color = Color.blue;
                        break;
                    default:
                        break;
                }
                Gizmos.DrawWireSphere(pos, 0.03f);
            }
        }
    }
}
