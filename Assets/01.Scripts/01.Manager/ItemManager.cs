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
            itemObject.SetItemData(ResourceManager.Instance.itemDataAll[val]);
            itemObject.transform.position = tr;
        }
    }
    
}
