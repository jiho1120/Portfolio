using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 어차피 한개 무조건 이게 태어나는데 태어날때 정보 세팅하면됨
// 기본 아이템 갯수 몇개 모으면 강화창에서 강화 갯수 모아도 그냥은 강화 못함
// 강화 창에서만 강화 가능, 물약도 강화로 오르게 하기
// 물약만 겹치기 가능 장착아이템은 갯수 겹치기 불가
// 등장하는 아이템이랑 장착한 아이템은 능력치가 다름
// 아이템 자체를 강화하는게 아니라 장착한 슬롯을 강화한다는 개념이면 좋음


public class ItemManager : Singleton<ItemManager>
{
    public Item itemDrop;

    public void Init()
    {

    }
    public void DropItem(int val, Vector3 tr)
    {
        if (itemDrop != null)
        {
            Item itemObject = Instantiate(itemDrop);
            itemObject.SetItemData(ResourceManager.Instance.itemDataAll[val]);
            itemObject.transform.position = tr;
        }
    }
    
}
