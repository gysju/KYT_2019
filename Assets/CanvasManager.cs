using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text m_Time;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.GameState == GameManager.EGameState.InGame)
        {            
            m_Time.text = "" + Mathf.RoundToInt(GameManager.Instance.GameDuration);
        }
    }
}
