using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();

    public override bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (canTakeDamage == false)
            return false;

        bool wasHit = base.TakeDamage(damage, elementalDamage, element,damageDealer);

        if (!wasHit)
            return false;
        
        
        //if damageDealer == player 
        //enemy.player = damageDealer;
        if (damageDealer.GetComponent<Player>() != null)
            enemy.TryEnterBattleState(damageDealer); //enemy try to battle state

        return true;
    }
}
