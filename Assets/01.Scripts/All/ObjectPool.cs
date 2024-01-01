using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour //where T : MonoBehaviour �� �Ⱦ��� MonoBehaviour�� ����� ����?
{
    //T tInfo;
    Queue<T> objectPool = new Queue<T>();
    List<T> tInfoList = new List<T>();
    Vector3 spawnPos = Vector3.zero;


    public int ranNum { get; private set; }
    int num;
    int mapSize = 10;

    public void SetRandomPosition(T obj)
    {
        int posX = Random.Range(0, mapSize);
        int posZ = Random.Range(0, mapSize);
        spawnPos.x = posX;
        spawnPos.z = posZ;
        obj.transform.position = spawnPos;
        obj.transform.rotation = Quaternion.identity;
    }

    public void InitializeObjectPool(T[] prefabArray)
    {
        num = prefabArray.Length;
        ranNum = Random.Range(0, num);
        T tInfo = Instantiate(prefabArray[ranNum]).GetComponent<T>();
        objectPool.Enqueue(tInfo);
        tInfoList.Add(tInfo);
        tInfo.gameObject.SetActive(false);
    }

    // ������Ʈ�� Ǯ���� �������� �Լ�
    public T GetObjectFromPool(T[] prefabArray)
    {
        if (objectPool.Count == 0)
        {
            InitializeObjectPool(prefabArray);
        }

        T obj = objectPool.Dequeue();
        obj.gameObject.SetActive(true);
        SetRandomPosition(obj);
        return obj;
    }


    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �Լ�
    public void ReturnObjectToPool(T tInfo)
    {
        tInfo.gameObject.SetActive(false);
        objectPool.Enqueue(tInfo);
    }
}