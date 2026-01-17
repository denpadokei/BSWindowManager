using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using BSWindowManager.Configuration;
using System.Reflection;
using Zenject;

namespace BSWindowManager.UI
{
    internal class Setting : BSMLAutomaticViewController, IInitializable
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public string ResourceName => string.Join(".", GetType().Namespace, "Setting.bsml");
        /// <summary>説明 を取得、設定</summary>
        [UIValue("auto-active")]
        public bool AutoActive
        {
            get => PluginConfig.Instance.AutoActive;

            set
            {
                PluginConfig.Instance.AutoActive = value;
                this.NotifyPropertyChanged();
            }
        }

        public void Initialize()
        {
            BSMLSettings.Instance.AddSettingsMenu("BS WINDOW MANAGER", ResourceName, this);
        }
    }
}
