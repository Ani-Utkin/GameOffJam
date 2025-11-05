
using RTSI.Services;
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
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(sceneLoaderService);
            builder.RegisterInstance(mainCamera);
            
            builder.RegisterVitalRouter(routing =>
            {
                routing.Map<RootMessageRouter>();
            });
            
            builder.UseEntryPoints(cfg =>
            {
                cfg.Add<RootEntrypoint>();
                cfg.OnException(Debug.LogException);
            });
        }
    }
}