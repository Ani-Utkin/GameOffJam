using GamePlay.AdventureEvents.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.AdventureEvents
{
    public class AdventureEventsLifetimeScope : LifetimeScope
    {
        [SerializeField] AdventureEventCanvas eventCanvas;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(eventCanvas);
            
            builder.UseEntryPoints(cfg =>
            {
                cfg.Add<AdventureEventsEntrypoint>();
                cfg.OnException(Debug.LogException);
            });
        }
    }
}