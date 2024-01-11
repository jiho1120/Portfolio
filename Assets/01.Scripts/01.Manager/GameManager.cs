using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문제점
// 또 같은 팔로 공격

//오늘 할일
// 중력 효과 고치기
// 그라운드 쓰면 밀치기
// 레벨업시 화면 꾸미기
// 경험치 0으로 하고 맥스 늘리기
// 능력치 아이템 스킬 강화 선택 // 이 방법 외에는 어떤 경우에도 강화 불가
// 골드 보이는 화면 만들기 강화시때만 보임

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