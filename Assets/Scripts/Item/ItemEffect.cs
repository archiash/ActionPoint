using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType {Heal, }
[CreateAssetMenu(menuName = "Create/Item/ItemEffect/Heal", fileName = "New Effect")]
public class ItemEffect : ScriptableObject
{
    public EffectType effectType;
    public int amountEffect;
    /*
    public virtual void DoEffect()
    {
        switch(effectType)
        {
            case EffectType.Heal :
                Character.character.currentHealth += amountEffect;
                if (Character.character.currentHealth > Character.character.Health.finalValue())
                    Character.character.currentHealth = Character.character.Health.finalValue();
                break;
        }
    }
    */
}

