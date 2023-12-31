using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Monster monster;

    private void Start()
    {
        player = GetComponent<Player>();
        monster = GetComponent<Monster>();
    }

    private void Update()
    {
        
    }
}