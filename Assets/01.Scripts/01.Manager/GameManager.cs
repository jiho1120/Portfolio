using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ResourceManager.Instance.LoadResources();
        SkillManager.Instance.SetSkillData();
        ObjectPoolManager.Instance.Init();
        SkillManager.instance.Init();
        ObjectPoolManager.Instance.SpawnMonster();
    }
}