using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{


    public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T> //ע���Լ��ΪT����Ϊ�䱾�������
    {



        private static T instance; 

        public static T Instance
        {

            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<T>();

                if (instance == null)
                {

                    new GameObject("Singleton of "+typeof(T)).AddComponent<T>();
                }
                else instance.Init(); 

                return instance;

            }
        }

        private void Awake()
        {
            instance = this as T;
            Init();
        }

        /// <summary>
        /// Called During MonoSingleton Awake
        /// </summary>
        protected virtual void Init()
        {

        }
    }

}
