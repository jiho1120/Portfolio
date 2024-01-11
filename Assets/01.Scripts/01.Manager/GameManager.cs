using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문제점
// 또 같은 팔로 공격

//오늘 할일
// 중력 효과 고치기
// 그라운드 쓰면 밀치기
// 아이템 ui에 등록

public class GameManager : Singleton<GameManager>
{
    public Player player;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ResourceManager.Instance.LoadResources();        
        SkillManager.Instance.Init();
        MonsterManager.Instance.Init();
        MonsterManager.Instance.SpawnMonster();
        ItemManager.Instance.Init();
        UiManager.Instance.Init();

    }
    private void Update()
    {
        UiManager.Instance.SetUI();
    }
}