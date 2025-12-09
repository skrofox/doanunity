using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();

    public override void TakeDamage(float damage, Transform damageDealer)
    {
        //if damageDealer == player 
        //enemy.player = damageDealer;
        if (damageDealer.GetComponent<Player>() != null)
            enemy.TryEnterBattleState(damageDealer); //enemy try to battle state

        base.TakeDamage(damage, damageDealer);
    }
}
