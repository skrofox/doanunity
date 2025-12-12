using UnityEngine;

public interface IDamagable
{
    public bool TakeDamage(float damage, float ElementalDamage, Transform damageDealer);

}
