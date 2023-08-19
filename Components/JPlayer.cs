using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JHelper.Components
{
    public class JPlayer : MonoBehaviour 
    {
        public Player NativePlayer { get; set; }


        protected virtual void Awake()
        {
            NativePlayer = GetComponent<Player>();
        }

        protected virtual void Start()
        {
        }

        protected virtual void OnDestroy()
        {

        }

        public void AddJComponent<T>() where T : Component
        {
            NativePlayer.gameObject.AddComponent<T>();
        }

        public T GetJComponent<T>() where T : Component
        {
            return NativePlayer.gameObject.GetComponent<T>();
        }
    }
}
