using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : Singleton<NewGameManager>
{
    public int nowGameIdx; // 저장된 인덱스 번호

    // Start is called before the first frame update
    void Start()
    {
        SlotManager.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
