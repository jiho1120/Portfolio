using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour //where T : MonoBehaviour 를 안쓰면 MonoBehaviour의 기능을 못씀
{
    //T tInfo;
    public Queue<T> objectPool = new Queue<T>();
    public List<T> InfoList = new List<T>();
    
    Vector3 spawnPos = Vector3.zero;
    int ranNum;
    int num;
    int mapSize = 10;
    int nameNum = 0;


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
        for (int i = 0; i < prefabArray.Length; i++)
        {
            T tInfo = Instantiate(prefabArray[i]).GetComponent<T>();            
            objectPool.Enqueue(tInfo);
            InfoList.Add(tInfo);
            tInfo.gameObject.SetActive(false);
        }
    }
    public void RandomInitializeObjectPool(T[] prefabArray, Transform pos)
    {
        num = prefabArray.Length;
        ranNum = Random.Range(0, num);
        //ranNum = 0;//########################
        T tInfo = Instantiate(prefabArray[ranNum]).GetComponent<T>();
        tInfo.transform.parent = pos;
        objectPool.Enqueue(tInfo);
        InfoList.Add(tInfo);

        tInfo.gameObject.name = tInfo.gameObject.name + nameNum;
        nameNum++;
        tInfo.gameObject.SetActive(false);
    }

    // 오브젝트를 풀에서 가져오는 함수
    public T GetObjectFromPool(T[] prefabArray, Transform pos)
    {
        if (objectPool.Count == 0)
        {
            //RandomInitializeObjectPool(prefabArray, pos); //################
            return null;
        }

        T obj = objectPool.Dequeue();
        obj.gameObject.SetActive(true);
        SetRandomPosition(obj);

        return obj;
    }


    // 오브젝트를 풀로 반환하는 함수
    public void ReturnObjectToPool(T tInfo)
    {
        tInfo.gameObject.SetActive(false);
        if (objectPool.Contains(tInfo) == false)
        {
            objectPool.Enqueue(tInfo);
        }        
    }

    public void DeleteActive()
    {
        IDead dead;
        for (int i = 0; i < InfoList.Count; i++)
        {
            dead = InfoList[i].GetComponent<IDead>();            
            if (dead != null)
            {
                dead.Dead(true);                
            }
        }
    }

}