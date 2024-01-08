using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ������ �Ѱ� ������ �̰� �¾�µ� �¾�� ���� �����ϸ��
// �⺻ ������ ���� � ������ ��ȭâ���� ��ȭ ���� ��Ƶ� �׳��� ��ȭ ����
// ��ȭ â������ ��ȭ ����, ���൵ ��ȭ�� ������ �ϱ�
// ���ุ ��ġ�� ���� ������������ ���� ��ġ�� �Ұ�
// �����ϴ� �������̶� ������ �������� �ɷ�ġ�� �ٸ�
// ������ ��ü�� ��ȭ�ϴ°� �ƴ϶� ������ ������ ��ȭ�Ѵٴ� �����̸� ����


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
            for (int i = 0; i < ResourceManager.Instance.itemDataAll.Length; i++)
            {
                int index = ResourceManager.Instance.itemDataAll[i].index;
                if (index == val)
                {
                    itemObject.SetItemData(ResourceManager.Instance.itemDataAll[i]);
                }
            }
            itemObject.transform.position = tr;
        }
    }
    
}
