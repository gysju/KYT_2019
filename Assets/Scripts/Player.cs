using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int m_ID;
    public int m_StartPosX = 0;
    public int m_StartPosY = 0;

    public float m_AxisSensitiveValue = 0.2f;
    public Transform m_WeaponsAnchor;

    private int m_Xpos;
    private int m_Ypos;

    private bool m_bHorizontalAxisHasChanged = true;
    private bool m_bVerticalAxisHasChanged = true;

    private Vector3 m_PreviousPos = Vector3.zero;
    private List<MapManager.TileCase> Paths = new List<MapManager.TileCase>();

    private bool HasMoved {
        get { return m_PreviousPos != transform.position;}
    }

    private MapManager.TileCase[] TileCaseSelected;

    private Tool EquipedTool = null;

    private void Awake()
    {

    }

    void Start()
    {
        GameManager.Instance.m_Players.Add(this);

        MapManager.TileCase tileCase = new MapManager.TileCase();
        if (MapManager.Instance.GetAvailableCase(ref tileCase))
        {
            tileCase.m_Owner = gameObject;
            m_Xpos = tileCase.m_X;
            m_Xpos = tileCase.m_Y;
        }
    }

    void Update()
    {
        Movement();
        Action();

        CheckPos();
    }

    void Movement()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal" + m_ID);
        float verticalAxis = Input.GetAxisRaw("Vertical" + m_ID);

        if (horizontalAxis != 0.0f && m_bHorizontalAxisHasChanged == true) // horizontal
        {
            if (horizontalAxis > 0.1f)
            {
                m_Xpos = Mathf.Clamp(m_Xpos + 1, 0, MapManager.Instance.GetMapSizeX() - 1);
                m_bHorizontalAxisHasChanged = false;
            }
            else if(horizontalAxis < -0.1f)
            {
                m_Xpos = Mathf.Clamp(m_Xpos - 1, 0, MapManager.Instance.GetMapSizeX() - 1);
                m_bHorizontalAxisHasChanged = false;
            }
        }
        else if(horizontalAxis == 0.0f)
        {
            m_bHorizontalAxisHasChanged = true;
        }

        if (verticalAxis != 0.0f && m_bVerticalAxisHasChanged == true) // horizontal
        {
            if (verticalAxis > 0.1f)
            {
                m_Ypos = Mathf.Clamp(m_Ypos + 1, 0, MapManager.Instance.GetMapSizeY() - 1);
                m_bVerticalAxisHasChanged = false;
            }
            else if (verticalAxis < -0.1f)
            {
                m_Ypos = Mathf.Clamp(m_Ypos - 1, 0, MapManager.Instance.GetMapSizeY() - 1);
                m_bVerticalAxisHasChanged = false;
            }
        }
        else if (verticalAxis == 0.0f)
        {
            m_bVerticalAxisHasChanged = true;
        }

        Vector3 pos = new Vector3(MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_XCoord,
                                  MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_YCoord, 
                                  0.0f); 
        transform.position = pos;
    }

    void Action()
    {
        if (EquipedTool != null)
        {
            if (Input.GetButton("Action" + m_ID))
            {
                DrawMap();
            }
            else if (Input.GetButtonUp("Action" + m_ID))
            {
                CleanDrawMap();
            }
        }
        if (Input.GetButtonDown("Action" + m_ID) && MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_Weapon != null)
        {
            SwitchWeapon();
        }
    }

    void DrawMap()
    {
        MapManager.TileCase tile = MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos];

        if (Paths.Count > 1 && Paths[0] == tile)
        {
            FillMap();

            return;
        }
        else if (tile.m_Owner == gameObject || Paths.Contains(tile))
        {
            return;
        }

        Paths.Add(tile);
        MapManager.Instance.ChangeTileType(0, m_ID, tile);
    }

    void CleanDrawMap()
    {
        for (int i = 0; i < Paths.Count; i++)
        {
            Vector3Int pos = new Vector3Int(Paths[i].m_X - 5, Paths[i].m_Y - 5, 0);
            MapManager.Instance.m_TileMapLevel0.SetTile(pos, Paths[i].m_originalTile);
        }

        Paths.Clear();
    }

    void FillMap()
    {
        MapManager.ETileType t = (MapManager.ETileType)Random.Range(0, (int)(MapManager.ETileType.Neutral)); 
        for (int i = 0; i < Paths.Count; i++)
        {
            if (CheckTileType(Paths[i]))
            {
              Paths[i].m_TileType = t;
              Paths[i].m_Owner = gameObject;
              MapManager.Instance.ChangeTileType(1, m_ID, Paths[i]);
            }
        }

        CleanDrawMap();
        Paths.Clear();
    }

    bool CheckTileType( MapManager.TileCase tileCase)
    {
        if (tileCase.m_TileType == MapManager.ETileType.Neutral)
        {
            return true;
        }
        else 
        {
            switch (EquipedTool.m_ToolType)
            {
                case MapManager.ETileType.TypeOne:
                    if (tileCase.m_TileType == MapManager.ETileType.TypeOne)
                        return true;
                    break;

                case MapManager.ETileType.TypeTwo:
                    if (tileCase.m_TileType == MapManager.ETileType.TypeTwo)
                        return true;
                    break;

                case MapManager.ETileType.TypeThree:
                    if (tileCase.m_TileType == MapManager.ETileType.TypeThree)
                        return true;
                    break;
            }
        }

        return false;
    }

    void CheckPos()
    {
        
    }

    public Vector2 GetPos()
    {
        return new Vector2( m_Xpos, m_Ypos);
    }

    void SwitchWeapon()
    {
        Tool previousTool = EquipedTool;

        EquipedTool = MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_Weapon;
        EquipedTool.SetOwner(this, true);

        if (previousTool != null)
        {
            previousTool.SetOwner(this, false);
            MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_Weapon = previousTool;
        }
        else
        {
            MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_Weapon = null;
        }
    }
}
