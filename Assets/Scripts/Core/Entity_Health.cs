using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entityVfx;
    private Entity entity;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead = false;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private float knockbackDuration = .2f;

    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
    }


    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;
        //
        Vector2 knockback = CalculateKnockback(damageDealer);

        entity?.ReciveKnockback(knockback, knockbackDuration);
        entityVfx?.PlayOnDamageVfx();

        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        maxHp -= damage;

        if (maxHp <= 0)
            Die();

    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died.");
    }

    private Vector2 CalculateKnockback(Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = knockbackPower;

        knockback.x *= direction;

        return knockback;
    }
}
