using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        anim = player.anim;
        rb = player.rb;
        
        input = player.input;
    }

    public virtual void Enter()
    {
        //Everytime state will changed, enter will be called
        anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        //we going to run logic of the state here
        // Debug.Log($"I run update of {animBoolName}");
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    public virtual void Exit()
    {
        //this will be called, everytime we exit state and change to a new oned
        // Debug.Log($"I exit {animBoolName}");
        anim.SetBool(animBoolName, false);
    }
}
