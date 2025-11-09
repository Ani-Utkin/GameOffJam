using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.Combat
{
    public class CombatLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.UseEntryPoints(cfg =>
            {
                cfg.Add<CombatEntryPoint>();
                
                cfg.OnException(UnityEngine.Debug.LogException);
            });
        }
    }
}
