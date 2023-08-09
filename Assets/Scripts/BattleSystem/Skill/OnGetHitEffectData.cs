using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OnGetHitEffectData : Skill
{
    [System.Serializable]
    public class OnGetHitParamiter
    {
        public string key;
        public string value;
    }

    public List<OnGetHitParamiter> paramiter;

    public override void Activate<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        OnGetHitEffect onGetHitEffect = new OnGetHitEffect();
        onGetHitEffect.effectParamiter.Add("Source", this);
        for (int i = 0; i < paramiter.Count; i++)
        {
            onGetHitEffect.effectParamiter.Add(paramiter[i].key, paramiter[i].value);
        }
        (user as Status).onGetHitEffect += onGetHitEffect.OnActivate;
    }

    public override bool Use<U, T>(U user, T enermy, ArenaType arena = ArenaType.Hunting)
    {
        return base.Use(user, enermy, arena);
    }

    public override bool Condition()
    {
        return base.Condition();
    }
}
