using UnityEngine;

public enum SkillUpgradeType
{
    None,

    //----------Dash Tree-----------//
    Dash,
    Dash_CloneOnStart,//Create a clone when dash starts
    Dash_CloneOnStartAndArrival,//Create a clone when dash starts and ends
    Dash_ShardOnStart,//Create a shard when dash starts
    Dash_ShardOnStartAndArrival, //Create a shard when dash starts and ends

    //-------Shard Tree----------//
    Shard, //The shard explodes when toucher by an enemy or time goes up
    Shard_MoveToEnemy, //Shard will move towards nearest enemy
    Shard_MultiCast, //Shard ability can have  up to N charges. You can cast them all in a raw
    Shard_Teleport, //You can swap places with the last shard you created
    Shard_TeleportHpRewind, //When you swap places with shard, your HP % is same as it was when you created shard

    //--- shard tree----
    SwordThrow, //you can throw sword to damage enemies from range
    SwordThrow_Spin, //Your sword will spin at one point and damage enemies. Like a chainsaw
    SwordThrow_Pierce, //Pierce sword will pierce N targets
    SwordThrow_Bounce, //Bounce sword will bounce between enemies

    //-----Time Echo
    TimeEcho, //Create clone player, take damage from enemies
    TimeEcho_SingleAttack, //time echo can perform a single attack
    TimeEcho_MultiAttack, //time echo can perform N attacks
    TimeEcho_ChanceToDuplicate, //time echo has a chance to create another time echo when attacks

    TimeEcho_HealWips, //sdsadsdasdasdadas

    TimeEcho_CleanseWips, //Wisp will now remove negative effects from player
    TimeEcho_CooldownWisp, //Wisp will reduce cooldown of all skills by N second.
}
