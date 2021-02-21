namespace MyFrameworkPure
{
    /// <summary>
    /// µ¥Àý»ùÀà
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CSingleton<T> where T : new()
    {
        private static readonly object lockHelper = new object();
        private static T m_sInstance;

        public static T sInstance
        {

            get
            {
                if (m_sInstance == null)
                {
                    lock (lockHelper)
                    {
                        if (m_sInstance == null)
                        {
                            m_sInstance = new T();
                        }
                    }
                }
                return m_sInstance;
            }

        }

        //destroy instance
        public void Destroy()
        {
            m_sInstance = default(T);
        }
    }

}
