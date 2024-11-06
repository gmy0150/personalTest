using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : IController
{
    Character controllerableCharacter = null;
    public void OnPosessed(Character controllerableCharacter)
    {
        this.controllerableCharacter = controllerableCharacter;
    }

    public void Tick(float deltaTime)
    {
        Transform tr = controllerableCharacter.transform;
        Vector3 direction = Vector3.zero;
        Vector3 up = Vector3.up;
        Vector3 down = Vector3.down;
        Vector3 right = Vector3.right;
        Vector3 left = Vector3.left;
        bool bMoveKeyDown = false;
        if (Input.GetKey(KeyCode.W))
        {
            direction += up;
            bMoveKeyDown = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += down;
            bMoveKeyDown = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += left;
            bMoveKeyDown = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += right;
            bMoveKeyDown = true;
        }
        bool isAttack = false;
        Character.State state = controllerableCharacter.GetState();
        if (state == Character.State.IDLE || state == Character.State.WALKING)
        {
            if (Input.GetMouseButtonUp(0))
            {
                controllerableCharacter.OnPlayAttack();
                isAttack = true;
            }
        }
        direction.Normalize();
        if (bMoveKeyDown)
        {
            if (!controllerableCharacter.IsAttacking())
            {
                controllerableCharacter.SetState(isAttack ? Character.State.ATTACK : Character.State.WALKING);
            }
            tr.localPosition = tr.localPosition + (direction * deltaTime * controllerableCharacter.MoveSpeed);
        }
        else
        {
            if (!controllerableCharacter.IsAttacking())
            {
                controllerableCharacter.SetState(isAttack ? Character.State.ATTACK:Character.State.IDLE);
            }
        }
    }


}
