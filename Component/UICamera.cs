using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrameworkPure
{
    /// <summary>
    /// ������ȾUI���
    /// </summary>
    public class UICamera : MonoBehaviour
    {
        private Camera camera;
        // Start is called before the first frame update
        void Start()
        {
            camera = GetComponent<Camera>();
        }

        void Update()
        {
            camera.orthographicSize = Screen.height * 0.5f;
            camera.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, -10);
        }
    }
}

