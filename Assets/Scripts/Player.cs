using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int m_ID;
    public int m_StartPosX = 0;
    public int m_StartPosY = 0;

    public float m_AxisSensitiveValue = 0.2f;

    private int m_Xpos;
    private int m_Ypos;

    private float m_previousHorizontalValue = 0.0f;
    private float m_previousVerticalValue = 0.0f;

    private bool m_bHorizontalAxisHasChanged = true;
    private bool m_bVerticalAxisHasChanged = true;

    private void Awake()
    {

    }

    void Start()
    {
        m_Xpos = m_StartPosX;
        m_Ypos = m_StartPosY;
    }

    void Update()
    {
        Movement();    
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

        transform.position = MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos];
    }
}
