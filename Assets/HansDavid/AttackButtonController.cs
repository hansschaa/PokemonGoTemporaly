using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButtonController : MonoBehaviour, IPointerDownHandler
{
    public Attack attack;
    public UIManager uiManager;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        var typeAttack = attack.typeAttack;

        switch (typeAttack)
        {
            case TypeAttack.DAMAGE:
                uiManager.DoDamage(attack, Entitie.ENEMY);
                break;
            case TypeAttack.HEAL:
                break;
            case TypeAttack.ATTACKUP:
                break;
            case TypeAttack.DEFENSEUP:
                break;
            case TypeAttack.VELOCITYUP:
                break;
        }
    }
}
