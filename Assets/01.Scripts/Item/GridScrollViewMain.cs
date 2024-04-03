using UnityEngine;
// main 초기화 요청 -> dic  -> 스크롤뷰 초기화 -> 동적 ui셀뷰 컨텐트안에넣음
public class GridScrollViewMain : Singleton<GridScrollViewMain>
{
    public UIGridScrollViewDic director;
    public void Init()
    {
        this.director.Init();
    }
}
