using UnityEngine.UI;
using UnityEngine;

public class SlotManager : Singleton<SlotManager>
{
    public int slotCount { get; private set; } = 3;
    public Slot[] slots;
    public SlotData[] slotDatas { get; private set; }
    public GameObject inputNamePanel;
    public InputField inputField;

    public void Init()
    {
        if (slotDatas == null)
        {
            slotDatas = new SlotData[slotCount];
        }

        for (int i = 0; i < slotCount; i++)
        {
            SaveLoadManager.Instance.Load(i);
            if (slotDatas[i] != null) // Null üũ �߰�
            {
                slots[i].UIUpdate(i);
            }
        }
    }

    public void CheckSlot(int slotIndex)
    {
        if (slotDatas[slotIndex] == null)
        {
            // �����Ͱ� ���� ���
            // �̸� �Է� UI�� ���� �̸��� �Է� ���� �� SaveData �Լ��� ȣ���ϼ���.
            inputNamePanel.SetActive(true);
            NewGameManager.Instance.nowGameIdx = slotIndex;
        }
        else
        {
            SceneLoadController.Instance.GoGameScene();
        }
    }
}
