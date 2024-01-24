using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : Node
{
    Boss owner;

    public Die(Boss owner)
    {
        this.owner = owner;
    }

    public override AllEnum.NodeState Evaluate()
    {
        if (owner.bossStat.health <= 0)
        {
            owner.Dead(false);
            
            GameManager.Instance.player.playerStat.AddMoney(GameManager.Instance.boss.bossStat.money);
            GameManager.Instance.player.playerStat.AddExp(GameManager.Instance.boss.bossStat.experience);
            GameManager.Instance.boss.agent.isStopped = true;
            GameManager.Instance.boss.gameObject.SetActive(false);
            GameManager.Instance.AddKillMonster(1);
            GameManager.Instance.SetCountGame(GameManager.Instance.countGame + 1);
            GameManager.Instance.SetGameClear();
            UiManager.Instance.ActiveEndPanel();

            owner.NowState = AllEnum.StateEnum.DIe;
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;
    }
    
}
