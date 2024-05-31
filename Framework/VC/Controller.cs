using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// ¿ØÖÆÆ÷»ùÀà
    /// </summary>
    public abstract class Controller
    {
        public View baseView { get; set; }
        public string name { get; set; }

        protected Controller()
        {
            CM.Add(this);
        }

        public abstract void Run();

        public virtual void Destroy()
        {

        }

        public virtual void ShowView()
        {
            baseView.transform.SetActive(true);
        }

        public virtual void HideView()
        {
            baseView.transform.SetActive(false);
        }
    }

    public class CM
    {
        public static IList<Controller> Controllers = new List<Controller>();

        public static void Add<T>(T controller) where T : Controller
        {
            Controllers.Add(controller);
        }

        public static T Get<T>()where T:Controller
        {
            return Controllers.OfType<T>().FirstOrDefault();
        }

        public static T Get<T>(string name) where T : Controller
        {
            return Controllers.OfType<T>().FirstOrDefault(x=>x.name == name);
        }

        public static void DestroyAll()
        {
            foreach(Controller controller in Controllers)
                controller.Destroy();
            Controllers.Clear();
        }
    }
}

