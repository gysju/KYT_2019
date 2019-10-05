using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int m_ID;

    private int m_Xpos;
    private int m_Ypos;

    private void Awake()
    {

    }

    void Start()
    {
        m_Xpos = 0;
        m_Ypos = 0;
    }

    void Update()
    {
        Movement();    
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_Ypos = Mathf.Clamp(m_Ypos - 1, 0, MapManager.Instance.GetMapSizeY() - 1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Ypos = Mathf.Clamp(m_Ypos + 1, 0, MapManager.Instance.GetMapSizeY() - 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_Xpos = Mathf.Clamp(m_Xpos - 1, 0, MapManager.Instance.GetMapSizeX() - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_Xpos = Mathf.Clamp(m_Xpos + 1, 0, MapManager.Instance.GetMapSizeX() - 1);
        }

        transform.position = MapManager.Instance.m_MapCoordonate[m_Xpos][m_Ypos];
    }
}
