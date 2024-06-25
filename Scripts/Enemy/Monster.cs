using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float currentHp;
    public float maxHp;
    public float attackPower;
    public float defensePower;
    public float speed;


    public virtual void DoAction()
    {

    }
}