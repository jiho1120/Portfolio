using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerConditionUI : MonoBehaviour
{
    public float hp;
    public float mp;
    public float exp;
    public float super;
    public float skill;
    public float item;

    [Range(0, 1)] public float HP = 0;
    private Slider slider;
    private void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    public void SetHp()
    {
        slider.value = HP;
    }
}