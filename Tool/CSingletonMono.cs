using UnityEngine;
using System.Collections;

namespace MyFrameworkPure
{
    /// <summary>
    /// ����monobehavior�ĵ�������
    /// </summary>
    /// <typeparam equimpentName="T"></typeparam>
    public class CSingletonMono<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        private static T m_sInstance;

        private static readonly object LockHelper = new object();

        public static T Instance
        {
            get
            {
                if (m_sInstance == null)
                {
                    m_sInstance = (T)FindObjectOfType(typeof(T));
                    lock (LockHelper)
                    {
                        if (m_sInstance == null)
                        {
                            GameObject obj = new GameObject(typeof(T).ToString());
                            m_sInstance = obj.AddComponent<T>();
                        }
                    }

                }

                return m_sInstance;
            }
        }

        void OnDestroy()
        {
            if (m_sInstance == this)
            {
                m_sInstance = default(T);
            }
        }
        /// <summary>
        /// �����Ƿ���Ч
        /// </summary>
        /// <returns></returns>
        public static bool IsValid()
        {
            return (Instance != null);
        }
    }

}
