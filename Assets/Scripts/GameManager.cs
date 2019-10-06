﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum EGameState
    {
        Menu,
        InGame,
        Endgame,
        Paused
    }

    public int m_NumberOfPlayer = 2;

    [Space(5)]
    public Tool m_AxePrefab;
    public Tool m_ChiselPrefab;
    public Tool m_ShovelPrefab;

    public EGameState GameState = EGameState.Menu;

    public float GameDuration = 60.0f;
    private float m_originalGameDuration;

    private Transform m_ToolParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            m_originalGameDuration = GameDuration;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        SpawnTools();
    }

    private void Update()
    {
        UpdateState();
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

    void UpdateState()
    {
        switch (GameState)
        {
            case EGameState.Menu:
                UpdateMenu();
                break;
            case EGameState.InGame:
                UpdateInGame();
                break;
            case EGameState.Endgame:
                UpdateEndGame();
                break;
            case EGameState.Paused:
                UpdatePaused();
                break;
            default:
                break;
        }
    }

    void UpdateMenu()
    {

    }

    void UpdateInGame()
    {
        GameDuration -= Time.deltaTime;
        if(GameDuration <= 0.0f)
        {
            GameState = EGameState.Endgame;
            GameEventMessage.SendEvent("EndGame");
        }
        else if (Input.GetButtonDown("Escape") || Input.GetKey(KeyCode.Escape))
        {                       
            GameState = EGameState.Paused;
            GameEventMessage.SendEvent("IsPaused");                        
        }
    }

    void UpdatePaused()
    {
        if (Input.GetButtonDown("Escape") || Input.GetKey(KeyCode.Escape))
        {
            GameState = EGameState.InGame;
            GameEventMessage.SendEvent("InGame");
        }
    }

    void UpdateEndGame()
    {

    }

    void DisplayMenu()
    {
        GameState = EGameState.Menu;
    }

    public void StartGame()
    {
        GameState = EGameState.InGame;
        GameDuration = m_originalGameDuration;
    }

    public void Resume()
    {
        GameState = EGameState.InGame;
        GameEventMessage.SendEvent("InGame");
    }

    void EndGame()
    {
        GameState = EGameState.Endgame;
    }
}
