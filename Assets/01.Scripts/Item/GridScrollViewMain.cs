using UnityEngine;
// main �ʱ�ȭ ��û -> dic  -> ��ũ�Ѻ� �ʱ�ȭ -> ���� ui���� ����Ʈ�ȿ�����
public class GridScrollViewMain : Singleton<GridScrollViewMain>
{
    public UIGridScrollViewDic director;
    public void Init()
    {
        this.director.Init();
    }
}
