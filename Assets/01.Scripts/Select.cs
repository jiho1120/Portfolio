using System.IO;
using UnityEngine;

public class Select : MonoBehaviour
{
    public void Init()
    {
        // ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.Instance.path + $"{i}"))    // �����Ͱ� �ִ� ���
            {
                DataManager.Instance.savefile[i] = true;            // �ش� ���� ��ȣ�� bool�迭 true�� ��ȯ
                DataManager.Instance.nowSlot = i;    // ������ ���� ��ȣ ����
                //DataManager.Instance.LoadData();    // �ش� ���� ������ �ҷ���
                DataManager.Instance.Load();

                UIManager.Instance.slotText[i].text = DataManager.Instance.gameData.playerData.name;    // ��ư�� �г��� ǥ��
            }
            else    // �����Ͱ� ���� ���
            {
                UIManager.Instance.slotText[i].text = "�������";
            }
        }
        // �ҷ��� �����͸� �ʱ�ȭ��Ŵ.(��ư�� �г����� ǥ���ϱ������̾��� ����)
        DataManager.Instance.DataClear();
    }


    public void Slot(int number)    // ������ ��� ����
    {
        DataManager.Instance.nowSlot = number;    // ������ ��ȣ�� ���Թ�ȣ�� �Է���.

        if (DataManager.Instance.savefile[number])    // bool �迭���� ���� ���Թ�ȣ�� true��� = ������ �����Ѵٴ� ��
        {
            DataManager.Instance.Load();
            SceneLoadController.Instance.GoGame();    // ���Ӿ����� �̵�
        }
        else    // bool �迭���� ���� ���Թ�ȣ�� false��� �����Ͱ� ���ٴ� ��
        {
            UIManager.Instance.OnNamePanel();    // �÷��̾� �г��� �Է� UI Ȱ��ȭ
        }
    }
}
