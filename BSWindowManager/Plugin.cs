using BeatSaberMarkupLanguage.Settings;
using BS_Utils.Utilities;
using BSWindowManager.UI;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using IPALogger = IPA.Logging.Logger;

namespace BSWindowManager
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        #region BSIPA Config
        /// <summary>
        /// Initializes the plugin with the specified configuration, logger, and dependency injection context.
        /// </summary>
        /// <remarks>This method is called by the BSIPA framework during plugin initialization. It sets up
        /// configuration, logging, and dependency injection for the plugin. Components are registered for both the menu
        /// and application contexts.</remarks>
        /// <param name="conf">The configuration object used to load and manage plugin settings. Cannot be null.</param>
        /// <param name="logger">The logger instance used for recording informational and debug messages. Cannot be null.</param>
        /// <param name="zenjector">The dependency injection context used to install plugin components. Cannot be null.</param>
        [Init]
        public void InitWithConfig(IPA.Config.Config conf, IPALogger logger, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Log.Info("BSWindowManager initialized.");
            zenjector.Install(Location.Menu, z =>
            {
                z.BindInterfacesAndSelfTo<Setting>().FromNewComponentAsViewController().AsSingle().NonLazy();
            });
            zenjector.Install(Location.App, z =>
            {
                z.BindInterfacesAndSelfTo<BSWindowManagerController>().AsSingle().NonLazy();
            });
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        #endregion
    }
}
