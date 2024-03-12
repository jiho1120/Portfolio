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
            if (slotDatas[i] != null) // Null 체크 추가
            {
                slots[i].UIUpdate(i);
            }
        }
    }

    public void CheckSlot(int slotIndex)
    {
        if (slotDatas[slotIndex] == null)
        {
            // 데이터가 없는 경우
            // 이름 입력 UI를 띄우고 이름을 입력 받은 후 SaveData 함수를 호출하세요.
            inputNamePanel.SetActive(true);
            NewGameManager.Instance.nowGameIdx = slotIndex;
        }
        else
        {
            SceneLoadController.Instance.GoGameScene();
        }
    }
}
