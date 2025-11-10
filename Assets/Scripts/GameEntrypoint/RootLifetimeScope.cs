
using RTSI.Services;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VitalRouter.VContainer;

namespace RTSI.GameEntrypoint
{


    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] SceneLoaderService  sceneLoaderService;
        [SerializeField] Camera mainCamera;
        [SerializeField] CombatStats playerCombatStats;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(sceneLoaderService);
            builder.RegisterInstance(mainCamera);


            builder.Register<PlayerStatsService>(Lifetime.Singleton);
            
            builder.RegisterVitalRouter(routing =>
            {
                routing.Map<RootMessageRouter>();
            });
            
            builder.UseEntryPoints(cfg =>
            {
                cfg.Add<RootEntrypoint>();
                cfg.OnException(Debug.LogException);
            });
            
            builder.RegisterBuildCallback(container =>
            {
                var playerStatsService = container.Resolve<PlayerStatsService>();
                playerStatsService.Initialize(playerCombatStats);
            });
        }
    }
}