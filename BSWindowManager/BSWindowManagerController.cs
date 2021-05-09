using BSWindowManager.Configuration;
using BSWindowManager.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BSWindowManager
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class BSWindowManagerController : MonoBehaviour
    {
        public static BSWindowManagerController Instance { get; private set; }

        private bool _autoAvtive = false;

        // These methods are automatically called by Unity, you should remove any you aren't using.
        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            // For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
            //   and destroy any that are created while one already exists.
            if (Instance != null) {
                Plugin.Log?.Warn($"Instance of {GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            Instance = this;
            PluginConfig.Instance.OnReloadEvent -= this.Instance_OnChangedEvent;
            PluginConfig.Instance.OnReloadEvent += this.Instance_OnChangedEvent;

            PluginConfig.Instance.OnChangedEvent -= this.Instance_OnChangedEvent;
            PluginConfig.Instance.OnChangedEvent += this.Instance_OnChangedEvent;
            SceneManager.activeSceneChanged += this.SceneManager_activeSceneChanged;
            this._autoAvtive = PluginConfig.Instance.AutoActive;
            Plugin.Log?.Debug($"{name}: Awake()");
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

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            if (Instance == this)
                Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

        }
        #endregion
    }
}
