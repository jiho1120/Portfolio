using System.Collections.Generic;
using UnityEngine;
using static AllEnum;

public class MonsterFactory : Factory
{
    private Monster newProduct;

    // �׸� ������ �ش� �׸� �������� �����ϴ� ����
    private Dictionary<string, MonsterType> monsterEnumMap;

    // �׸� ������ ���� �ʱ�ȭ�ϴ� ������
    public MonsterFactory()
    {
        monsterEnumMap = new Dictionary<string, MonsterType>()
        {
            { "NormalMonster", MonsterType.NormalMonster },
            { "ExplosionMonster", MonsterType.ExplosionMonster }
        };
    }

    public override IProduct GetProduct(string type)
    {
        if (monsterEnumMap.TryGetValue(type, out var monsterEnum))
        {
            return CreateMonster(monsterEnum);
        }

        Debug.LogError($"Monster type {type} not recognized.");
        return null;
    }

    private Monster CreateMonster(MonsterType monsterEnum)
    {
        obj = Instantiate(ResourceManager.Instance.GetPrefab(DictName.MonsterDict, monsterEnum.ToString()));
        switch (monsterEnum)
        {
            case MonsterType.NormalMonster:
                newProduct = obj.GetComponent<Monster>();
                break;
            case MonsterType.ExplosionMonster:
                newProduct = obj.GetComponent<ExplosionMonster>();
                break;
            default:
                Debug.LogError($"Monster enum {monsterEnum} not recognized.");
                return null;
        }
        newProduct.Init();
        return newProduct;
    }
}
