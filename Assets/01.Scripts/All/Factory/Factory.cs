using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ϳ��� ���丮 Ŭ����: �����ϰ� ���������ϱ� ����. ���� å�� ��Ģ ���� ���ɼ� ����.
// �� �����۴� �ϳ��� ���丮: ��Ȯ�ϰ� Ȯ�强 ����. �ڵ� �ߺ� �� Ŭ���� �� ���� ���ɼ� ����.

public abstract class Factory : MonoBehaviour
{
    protected GameObject obj;

    public abstract IProduct GetProduct(string type);

    public string GetLog(IProduct product)
    {
        string logMessage = "Factory: created product " + product.ProductName;
        return logMessage;
    }
}
