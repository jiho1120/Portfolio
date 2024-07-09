using System.IO;
using UnityEngine;

public class Select : MonoBehaviour
{
    public void Init()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.Instance.path + $"{i}"))    // 데이터가 있는 경우
            {
                DataManager.Instance.savefile[i] = true;            // 해당 슬롯 번호의 bool배열 true로 변환
                DataManager.Instance.nowSlot = i;    // 선택한 슬롯 번호 저장
                //DataManager.Instance.LoadData();    // 해당 슬롯 데이터 불러옴
                DataManager.Instance.Load();

                UIManager.Instance.slotText[i].text = DataManager.Instance.gameData.playerData.name;    // 버튼에 닉네임 표시
            }
            else    // 데이터가 없는 경우
            {
                UIManager.Instance.slotText[i].text = "비어있음";
            }
        }
        // 불러온 데이터를 초기화시킴.(버튼에 닉네임을 표현하기위함이었기 때문)
        DataManager.Instance.DataClear();
    }


    public void Slot(int number)    // 슬롯의 기능 구현
    {
        DataManager.Instance.nowSlot = number;    // 슬롯의 번호를 슬롯번호로 입력함.

        if (DataManager.Instance.savefile[number])    // bool 배열에서 현재 슬롯번호가 true라면 = 데이터 존재한다는 뜻
        {
            DataManager.Instance.Load();
            SceneLoadController.Instance.GoGame();    // 게임씬으로 이동
        }
        else    // bool 배열에서 현재 슬롯번호가 false라면 데이터가 없다는 뜻
        {
            UIManager.Instance.OnNamePanel();    // 플레이어 닉네임 입력 UI 활성화
        }
    }
}
