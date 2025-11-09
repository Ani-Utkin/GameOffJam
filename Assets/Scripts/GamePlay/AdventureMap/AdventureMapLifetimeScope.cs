using System.Collections.Generic;
using RTSI.GamePlay.AdventureMap.Input;
using TRSI.GamePlay.AdventureMap.Debug;
using TRSI.GamePlay.AdventureMap.Routes;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VitalRouter.VContainer;

namespace TRSI.GamePlay.AdventureMap
{
    public class AdventureMapLifetimeScope : LifetimeScope
    {
        [SerializeField] OceanGrid grid;
        [SerializeField] BoatView boatView;
        [SerializeField] DebugCanvas debugCanvas;
        [SerializeField] List<PirateView> pirateShips;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(grid);
            builder.RegisterInstance(boatView);
            builder.RegisterInstance(debugCanvas);
            
            builder.RegisterInstance(pirateShips);
            
            builder.Register<AdventureMapController>(Lifetime.Scoped);
            
            builder.RegisterVitalRouter(routing =>
            {
                routing.Map<DebugRoutes>();
                routing.Map<InputRoutes>();
                routing.Map<GameEventsRoute>();
            });
            
            builder.UseEntryPoints(cfg =>
            {
                cfg.Add<AdventureMapInputEntrypoint>();
                
                cfg.OnException(UnityEngine.Debug.LogException);
            });
        }
    }
}