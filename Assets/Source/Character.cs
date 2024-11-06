using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public enum State
    {
        IDLE,
        WALKING,
        ATTACK,
        MAX
    }
    private const string BaseCharacterPath = "Character/";
    public static CHARACTER_TYPE Create<CHARACTER_TYPE>(Transform parent) where CHARACTER_TYPE : Character
    {
        GameObject prefabObject = GameObject.Instantiate(Resources.Load(BaseCharacterPath + typeof(CHARACTER_TYPE).ToString()))as GameObject;

        CHARACTER_TYPE character = prefabObject.GetComponent<CHARACTER_TYPE>();
        if(character == null)
        {
            character = prefabObject.AddComponent<CHARACTER_TYPE>();
        }
        if(parent != null)
        {
            prefabObject.transform.SetParent(parent);
        }
        prefabObject.transform.localScale = Vector3.one * character.scale;
        prefabObject.transform.localPosition = Vector3.zero;
        return character;
    }
    protected IController controller = null;
    protected Animator animator = null;
    bool bIsAttacking = false;
    [Header("Start")]
    [SerializeField] float scale = 1;
    [Header("Start")]
    [SerializeField] float moveSpeed = 1;
    public float MoveSpeed { get => moveSpeed; }
    protected State currentState = State.IDLE;

    public void SetController(IController controller)
    {
        controller.OnPosessed(this);
        this.controller = controller;
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (controller != null)
        {
            controller.Tick(Time.deltaTime);
        }
    }
    protected void SetAnimationTrigger(string triggerName)
    {
        if(animator != null)
            animator.SetTrigger(triggerName);
    }
    protected void ResetAnimationonTrigger(string triggerName)
    {
        if(animator != null)
            animator.ResetTrigger(triggerName);
    }
    public abstract void OnPlayAttack();


    public void OnAttackEnd()
    {
        bIsAttacking = false;
    }
    public bool IsAttacking()
    {
        return bIsAttacking;
    }
    public void SetState(State newState)
    {
        if (currentState == newState) return;
        for (State state = State.IDLE; state < State.MAX; state++)
        {
            ResetAnimationonTrigger(state.ToString());
        }
        SetAnimationTrigger(newState.ToString());
        currentState = newState;
        if(currentState == State.ATTACK)
        {
            bIsAttacking = true;
        }
    }
    public State GetState()
    {
        return currentState;
    }
}
