using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GamePlay.PauseMenu
{
    public class PauseMenuLifetimeScope : LifetimeScope
    {
        [SerializeField] PauseMenuView pauseMenuView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(pauseMenuView);
            
            builder.UseEntryPoints(cfg =>
            {
                cfg.Add<PauseMenuViewEntrypoint>();
                
                cfg.OnException(Debug.LogException);
            });
        }
    }
}