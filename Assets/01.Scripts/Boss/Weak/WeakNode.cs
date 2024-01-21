using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakNode : MonoBehaviour
{
    public GameObject node;
    public Boss boss;
    public Transform nodeTr;
    SpriteRenderer innerNode;
    SpriteRenderer outterNode;

    private void Start()
    {
        //Instantiate(node, nodeTr);
        innerNode = node.transform.GetChild(0).GetComponent<SpriteRenderer>();
        outterNode = node.transform.GetChild(1).GetComponent<SpriteRenderer>();
        innerNode.transform.localScale = Vector3.one;
        outterNode.transform.localScale = Vector3.one * 2;
        StartCoroutine(ShrinkCircle());
    }

    IEnumerator ShrinkCircle()
    {
        node.SetActive(true);
        while (true)
        {
            node.transform.position = boss.transform.position + new Vector3(0,1,0);
            outterNode.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            yield return null;
            if (outterNode.transform.localScale == new Vector3(1,1,1))
            {
                Debug.Log("²¨Áü");
                node.SetActive(false);
                break;
            }
        }
        
    }
}
