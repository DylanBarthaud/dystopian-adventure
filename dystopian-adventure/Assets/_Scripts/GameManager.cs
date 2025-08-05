using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { 
        get { return instance; }
    }

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }

    [SerializeField] private GameObject player;

    public GameObject GetPlayer()
    {
        return player;
    }
}
