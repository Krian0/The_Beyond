﻿using CheckEnum;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/Transitions/CheckLevel", fileName = "New CheckLevel")]
public class CheckLevel : AITransition
{
    public CheckType variableIs = CheckType.GREATER_THAN;
    public int level;

    public override bool Decide(AIController controller)
    {
        if (variableIs == CheckType.GREATER_THAN)
            return controller.Npc.stats.level.GetLevel() > level;

        if (variableIs == CheckType.LESS_THAN)
            return controller.Npc.stats.level.GetLevel() < level;

        return controller.Npc.stats.level.GetLevel() == level;
    }
}