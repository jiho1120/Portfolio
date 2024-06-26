using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 하나의 팩토리 클래스: 간단하고 유지보수하기 쉬움. 단일 책임 원칙 위반 가능성 있음.
// 각 아이템당 하나의 팩토리: 명확하고 확장성 좋음. 코드 중복 및 클래스 수 증가 가능성 있음.

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
