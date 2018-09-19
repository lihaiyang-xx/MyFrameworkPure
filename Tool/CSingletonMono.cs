using UnityEngine;
using System.Collections;

//µ¥Àý»ùÀà£¬
public class CSingletonMono<T> : MonoBehaviour
    where T : MonoBehaviour
{
	private static T m_sInstance;

    private static readonly object lockHelper = new object();

    public static T Instance
	{
		get
		{
			if (m_sInstance == null)
			{
				m_sInstance = (T)FindObjectOfType (typeof(T));
                lock(lockHelper)
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
		if( m_sInstance == this )
        {
			m_sInstance = default(T);
		}
	}

	public static bool IsValid()
	{
		return ( Instance != null ) ;
	}
}