using System.Linq;
using UnityEngine;

namespace Game.Abilities
{
    public class Skill : Filter
    {
        public Ability ability
        {
            get;
            private set;
        }

        public Filter[] filters
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public int MaxUses
        {
            get;
            set;
        }

        public Skill(Ability ability, Filter[] filters, string description, int maxUses = 0)
        {
            this.ability = ability;
            this.filters = filters;
            Description = description;
            MaxUses = maxUses;
        }

        public bool filter(RoomVM room, Vector2Int relativePos)
        {
            return filters.All(filter => filter.filter(room, relativePos));
        }
    }
}