using GamePlay.AdventureEvents.Views;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;
using VitalRouter.VContainer;

namespace GamePlay.AdventureEvents
{
    public class AdventureEventsLifetimeScope : LifetimeScope
    {
        [SerializeField] EventCanvas eventCanvas;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(eventCanvas);
            builder.RegisterInstance(eventCanvas.SimpleEventPanel);
            builder.RegisterInstance(eventCanvas.CombatEventPanel);
            
            builder.RegisterVitalRouter(routing =>
            {
                routing.Map<AdventureEventsRoutes>();
            });
            
            builder.UseEntryPoints(cfg =>
            {
                cfg.Add<AdventureSimpleEventsEntrypoint>();
                cfg.Add<AdventureCombatEventsEntrypoint>();
                cfg.OnException(Debug.LogException);
            });
        }
    }
}