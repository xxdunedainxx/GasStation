using UnityEngine;
using System.Collections.Generic;
using System;
using gasstation.code.core.logging;

namespace gasstation.code.core.objects
{
    public abstract class SingletonMonobehaviour : MonoBehaviour
    {
        public static Dictionary<string, SingletonMonobehaviour> instances = new Dictionary<string, SingletonMonobehaviour>();
        public abstract void AfterStart();
        public static void PrintSingletons()
        {
            LogFactory.INFO("SINGLETONS::");
            foreach (string key in SingletonMonobehaviour.instances.Keys)
            {
                LogFactory.INFO(key);
            }
        }

        public static SingletonMonobehaviour getInstance(string singleton)
        {
            if (instances.ContainsKey(singleton))
            {
                return SingletonMonobehaviour.instances[singleton];
            }
            else
            {
                return null;
            }
        }

        public SingletonMonobehaviour getThisInstance()
        {
            return SingletonMonobehaviour.getInstance(this.GetType().Name);
        }

        public void Awake()
        {
            // ensure only one is created 
            if (this.getThisInstance() != null && this.getThisInstance() != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                if (SingletonMonobehaviour.instances.ContainsKey(this.GetType().Name) == true)
                {
                    SingletonMonobehaviour.instances.Remove(this.GetType().Name);
                }
                SingletonMonobehaviour.instances.Add(this.GetType().Name, this);
                this.AfterStart();
            }
        }
    }
}