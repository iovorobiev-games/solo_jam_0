using Game.data;
using UnityEngine;

namespace Game.Abilities
{
    public interface Ability
    {
        void use(RoomVM room);
    }
}