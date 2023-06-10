using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// 饿汉式单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : Singleton<T> //注意此约束为T必须为其本身或子类
    {
        protected Singleton() { }
        public static T Instance { get; private set; }

        static Singleton()
        {
            Instance = Activator.CreateInstance(typeof(T), true) as T;
            Instance.Init(); //初始化一次
        }

        /// <summary>
        /// 可选初始化函数
        /// </summary>
        protected virtual void Init()
        {

        }
    }
}
