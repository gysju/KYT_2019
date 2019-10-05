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

        if (HasMoved)
        {
            CheckPos();
            m_PreviousPos = transform.position;
        }
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

    void CheckPos()
    {
        
    }

    void Action()
    {
        if (Input.GetButtonDown("Action" + m_ID) && MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_Weapon != null)
        {
            Tool previousTool = EquipedTool;

            EquipedTool = MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_Weapon;
            EquipedTool.SetOwner(this, true);

            MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos].m_Weapon = null;

            if (previousTool != null)
            {
                previousTool.SetOwner(this, false);
            }
        }
    }

    public Vector2 GetPos()
    {
        return new Vector2( m_Xpos, m_Ypos);
    }
}
