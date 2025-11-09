using System.Collections.Generic;
using Game.Abilities;
using Game.ui;
using UnityEngine;

namespace Game.data
{
    public class SkillDB
    {
        public static Dictionary<string, Skill> skills = new()
        {
            {
                "damage_boost_all_1",
                new Skill(
                    new BoostAbility(1, BoostAbility.BoostType.DAMAGE),
                    new Filter[]
                    {
                        PositionFiltersDB.filters["all"]
                    },
                    "+1" + RTHelper.SWORD + " to " + RTHelper.ALL
                )
            },
            {
                "damage_boost_top_down_1",
                new Skill(
                    new BoostAbility(1, BoostAbility.BoostType.DAMAGE),
                    new Filter[]
                    {
                        PositionFiltersDB.filters["top_down"]
                    },
                    "+1" + RTHelper.SWORD + " to " + RTHelper.TOP_DOWN
                )
            },
            {
                "damage_boost_left_right_1",
                new Skill(
                    new BoostAbility(1, BoostAbility.BoostType.DAMAGE),
                    new Filter[]
                    {
                        PositionFiltersDB.filters["left_right"]
                    },
                    "+1" + RTHelper.SWORD + " to " + RTHelper.LEFT_RIGHT
                )
            },
            {
                "cooldown_reduce_all_1",
                new Skill(
                    new BoostAbility(1, BoostAbility.BoostType.COOLDOWN),
                    new Filter[]
                    {
                        PositionFiltersDB.filters["all"]
                    },
                    "+1" + RTHelper.RESP + " to " + RTHelper.ALL
                )
            },
            {
                "cooldown_reduce_top_down_1",
                new Skill(
                    new BoostAbility(1, BoostAbility.BoostType.COOLDOWN),
                    new Filter[]
                    {
                        PositionFiltersDB.filters["top_down"]
                    },
                    "+1" + RTHelper.RESP + " to " + RTHelper.TOP_DOWN
                )
            },
            {
                "cooldown_reduce_left_right_1",
                new Skill(
                    new BoostAbility(1, BoostAbility.BoostType.COOLDOWN),
                    new Filter[]
                    {
                        PositionFiltersDB.filters["left_right"]
                    },
                    "+1" + RTHelper.RESP + " to " + RTHelper.LEFT_RIGHT
                )
            },
            {
                "cost_reduce_1",
                new Skill(
                    new BoostAbility(1, BoostAbility.BoostType.COST),
                    new Filter[]
                    {
                        NewCardsFilter.instance
                    },
                    "+1" + RTHelper.RESP + " to " + RTHelper.TOP_DOWN
                )
            },
            {
                "rats",
                new Skill(
                    UselessAbility.instance,
                    new Filter[]
                    {
                    },
                    "Rats. Classic"
                )
            },
            {
                "cost_reduce_one_shot",
                new Skill(
                    new BoostAbility(1, BoostAbility.BoostType.COST),
                    new Filter[]
                    {
                        NewCardsFilter.instance
                    },
                    "-1 " + RTHelper.TIME + " for next room.",
                    1
                )
            },
            {
                "fungi_bonus",
                new Skill(
                    new TriggerAbility(),
                    new Filter[]
                    {
                        new RoomTypeFilter("Alchemist Lab"),
                        PositionFiltersDB.filters["all"],
                    },
                    "Each lab around triggers one more time"
                )
            }
        };
    }
}