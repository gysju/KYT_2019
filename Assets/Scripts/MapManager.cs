using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        public Tool m_OriginalWeapon = null;
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

    [Space(5)]

    public TileBase NeutralTile;

    [Space(5)]
    public List<TileBase> TileOne0 = new List<TileBase>();
    public List<TileBase> TileOne1 = new List<TileBase>();

    [Space(5)]
    public List<TileBase> TileTwo0 = new List<TileBase>();
    public List<TileBase> TileTwo1 = new List<TileBase>();

    [Space(5)]
    public List<TileBase> TileThree0 = new List<TileBase>();
    public List<TileBase> TileThree1 = new List<TileBase>();

    private List<TileCase> m_ShuffleCoordonate = new List<TileCase>();

    public Tilemap m_TileMapLevel0;
    public Tilemap m_TileMapLevel1;

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

    public void CleanMap()
    {
        for (int i = 0; i < m_ShuffleCoordonate.Count; i++)
        {
            m_ShuffleCoordonate[i].m_Owner = null;
            m_ShuffleCoordonate[i].m_TileType = ETileType.Neutral;
            m_ShuffleCoordonate[i].m_Weapon = m_ShuffleCoordonate[i].m_OriginalWeapon;

            Vector3Int pos = new Vector3Int(m_ShuffleCoordonate[i].m_X - 5, m_ShuffleCoordonate[i].m_Y - 5, 0);
            m_TileMapLevel1.SetTile(pos, null);
        }

        Shuffle(m_ShuffleCoordonate);
    }

    public bool GetAvailableCase( ref TileCase result)
    {
        for (int j = 0; j < m_ShuffleCoordonate.Count; j++)
        {
            if (   m_ShuffleCoordonate[j].m_Owner == null 
                && m_ShuffleCoordonate[j].m_Weapon == null)
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

    public void ChangeTileType( int level, int playerID, TileCase tileCase)
    {
        Vector3Int pos = new Vector3Int( tileCase.m_X - 5, tileCase.m_Y - 5, 0);

        if ( level == 0)
        {
            switch (playerID)
            {
                case 0:
                    m_TileMapLevel0.SetTile(pos, TileOne0[GetTileIdByPos( tileCase.m_X, tileCase.m_Y)]);
                    break;
                case 1:
                    m_TileMapLevel0.SetTile(pos, TileTwo0[0]);
                    break;
                case 2:
                    m_TileMapLevel0.SetTile(pos, TileThree0[0]);
                    break;
                case 3:
                    m_TileMapLevel0.SetTile(pos, NeutralTile);
                    break;
            }
        }
        else
        {
            switch (playerID)
            {
                case 0:
                    m_TileMapLevel1.SetTile(pos, TileOne1[(int)tileCase.m_TileType]);
                    break;
                case 1:
                    m_TileMapLevel1.SetTile(pos, TileTwo1[(int)tileCase.m_TileType]);
                    break;
                case 2:
                    m_TileMapLevel1.SetTile(pos, TileThree1[(int)tileCase.m_TileType]);
                    break;
            }
        }
    }

    private int GetTileIdByPos(int x, int y)
    {
        if (x == 0 && y == 0) //corner
        {
            return 12;
        }
        else if (x == 0 && y == 9) // corner
        {
            return 0;
        }
        else if (x == 9 && y == 0) // corner
        {
            return 15;
        }
        else if (x == 9 && y == 9) // corner
        {
            return 3;
        }
        else if (x > 0 && x < 9 && y == 0) // up
        {
            return 14;
        }
        else if (x > 0 && x < 9 && y == 9) // down
        {
            return 2;
        }
        else if (y > 0 && y < 9 && x == 0) // left
        {
            return 4;
        }
        else if (y > 0 && y < 9 && x == 9) // right
        {
            return 7;
        }
        else
        {
            return 6;
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
