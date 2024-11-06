using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADCharacter : Character
{
    public override void OnPlayAttack()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        mousePos.z = 0;

        Vector3 attackdir = (mousePos - transform.position).normalized;
        GameObject attack = GameObject.Instantiate( Resources.Load("AttackBall"),transform.position,Quaternion.identity)as GameObject;
        Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
        rb.velocity = attackdir * 8;
    }

    protected override void Start()
    {
        base.Start();
    }
}
