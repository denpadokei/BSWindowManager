using BeatSaberMarkupLanguage.Settings;
using BSWindowManager.Configuration;
using BSWindowManager.UI;
using BSWindowManager.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace BSWindowManager
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class BSWindowManagerController
    {
        private bool _autoAvtive = false;

        // These methods are automatically called by Unity, you should remove any you aren't using.
        #region Monobehaviour Messages
        public BSWindowManagerController()
        {
            PluginConfig.Instance.OnReloadEvent -= this.Instance_OnChangedEvent;
            PluginConfig.Instance.OnReloadEvent += this.Instance_OnChangedEvent;

            PluginConfig.Instance.OnChangedEvent -= this.Instance_OnChangedEvent;
            PluginConfig.Instance.OnChangedEvent += this.Instance_OnChangedEvent;
            SceneManager.activeSceneChanged += this.SceneManager_activeSceneChanged;
            this._autoAvtive = PluginConfig.Instance.AutoActive;
            Plugin.Log?.Debug($"");
        }

        private void Instance_OnChangedEvent(PluginConfig obj)
        {
            this._autoAvtive = obj.AutoActive;
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            try {
                if (!this._autoAvtive) {
                    return;
                }

                var Winhdl = WindowManager.FindWindow(null, "Beat Saber");
                if (Winhdl == IntPtr.Zero) {
                    return;
                }
                WindowManager.ActiveWindow(Winhdl);
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }
        #endregion
    }
}
