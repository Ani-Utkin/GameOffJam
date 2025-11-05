using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RTSI.MainMenu
{
    public class MainMenuLifetimeScope : LifetimeScope
    {
        [SerializeField] MainMenuView mainMenuView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(mainMenuView);
            
            builder.UseEntryPoints(cfg =>
            {
                cfg.Add<MainMenuEntrypoint>();
                cfg.OnException(Debug.LogException);
            });

        }
    }
}