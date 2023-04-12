using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyFrameworkPure;
using UnityEngine;

namespace MyFrameworkPure
{
    public class ViewModule : Module
    {
        private IList<Panel> panels;

        public override void Init()
        {
            panels = GameObjectTool.FindObjectsOfType<Panel>();
        }

        public override void Run()
        {
            
        }

        public override void Destroy()
        {

        }

        public T GetPanel<T>() where T : Panel
        {
            T panel = panels.OfType<T>().FirstOrDefault();
            return panel ?? GameObjectTool.FindObjectOfType<T>();
        }

        public T GetPanel<T>(string name) where T : Panel
        {
            T panel = panels.OfType<T>().FirstOrDefault(x => x.name == name);
            return panel ?? GameObjectTool.FindObjectsOfType<T>().FirstOrDefault(x => x.name == name);
        }
    }
}

