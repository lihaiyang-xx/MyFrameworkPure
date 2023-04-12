using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyFrameworkPure;
using UnityEngine;

namespace MyFrameworkPure
{
    public class ModuleManager : CSingleton<ModuleManager>
    {
        private IList<Module> modules;

        public ModuleManager()
        {
            modules = new List<Module>();
        }
        public void AddModule<T>(T module) where T : Module
        {
            if (modules.OfType<T>().Any())
                throw new Exception("重复添加模块:" + nameof(T));
            modules.Add(module);
        }

        public T GetModule<T>()
        {
            if (!modules.OfType<T>().Any())
                throw new Exception("没有添加模块:" + nameof(T));
            return modules.OfType<T>().FirstOrDefault();
        }

        public void Init()
        {
            modules.ForEach(x=>x.Init());
        }

        public void Run()
        {
            modules.ForEach(x=>x.Run());
        }
    }
}

