using UnityEngine;

public enum TypeAttack
{
    DAMAGE, HEAL, ATTACKUP, DEFENSEUP, VELOCITYUP
}

public enum Entitie
{
    PLAYER, ENEMY
}

[CreateAssetMenu(fileName = "Data", menuName = "Pokemon/Attack", order = 1)]
public class Attack : ScriptableObject
{
    public string attackName;
    public int value;
    public TypeAttack typeAttack;
}