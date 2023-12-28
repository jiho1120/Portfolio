using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class PlayerStat : Stat
//{
//    public float mp { get; private set; }
//    public float maxMp { get; private set; }
//    public float luck { get; private set; }

//    public PlayerStat(Stat stat) : base(stat)
//    {
//        this.mp = stat.mp;
//        maxMp = stat.maxMp;
//        this.luck = stat.luck;
//    }

//    public PlayerStat(float hp, float mp, float att, float armor, float critical, float speed, float luck)
//        : base(hp, att, armor, critical, speed)
//    {
//        this.mp = mp;
//        maxMp = this.mp;
//        this.luck = luck;
//    }

//    public override void SetStat(float hp, float att, float armor, float critical, float speed)
//    {
//        base.SetStat(hp, att, armor, critical, speed);
//        this.mp = hp; // Assuming the same property is used for both hp and mp
//        maxMp = hp; // Assuming the same property is used for both maxMp and maxHp
//        this.luck = luck;
//    }
//}
