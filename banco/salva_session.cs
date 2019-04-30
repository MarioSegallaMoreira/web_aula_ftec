using projeto_web_aula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projeto_web_aula.banco
{
    public static class salva_session
    {
      
            /// <summary> 
            /// Get value. 
            /// </summary> 
            /// <typeparam name="T"></typeparam> 
            /// <param name="session"></param> 
            /// <param name="key"></param> 
            /// <returns></returns> 
            public static T GetDataFromSession<T>(this HttpContext session, string key)
            {
                return (T)session.Session[key];
            }
            /// <summary> 
            /// Set value. 
            /// </summary> 
            /// <typeparam name="T"></typeparam> 
            /// <param name="session"></param> 
            /// <param name="key"></param> 
            /// <param name="value"></param> 
            public static void SetDataToSession<T>(this HttpContext session, string key, object value)
            {
                session.Session[key] = value;
            }
        
    }
}