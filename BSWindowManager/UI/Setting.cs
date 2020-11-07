using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using BSWindowManager.Configuration;
using UnityEngine;

namespace BSWindowManager.UI
{
    internal class Setting : PersistentSingleton<Setting>, INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string member = null)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(member));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}
