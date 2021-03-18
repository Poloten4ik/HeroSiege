using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Enums
{
    public enum EnemyState
    {
        MOVE_TO_CASTLE,
        ATTACK_CASTLE,
        MOVE_TO_PLAYER,
        ATTACK_PLAYER,
        SPELL_CAST,
        DEATH
    }
}