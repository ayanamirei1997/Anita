using UnityEngine;
using System;

namespace Anita
{
    public class ParamUtil : AnitaAVG
    {
        public static string StringNotNull(string s)
        {
            return s == null || s == "" ? null : s;
        }

        public static int ParseString2Int(string s, int defaultInt)
        {
            try
            {
                return s == null || s == "" ? defaultInt : int.Parse(s);
            }
            catch (Exception e)
            {
                Debug.Log(s + "Error");
                Debug.Log(e.Message + "\n" + e.StackTrace);
                return defaultInt;
            }
        }

        public static float ParseString2Float(string s, float defaultFloat)
        {
            try
            {
                return s == null || s == "" ? defaultFloat : float.Parse(s);
            }
            catch (Exception e)
            {
                Debug.Log(s + "Error");
                Debug.Log(e.Message + "\n" + e.StackTrace);
                return defaultFloat;
            }
        }

        //Not null or "" or \n or \r
        public static bool StringEffective(string s)
        {
            return !(s == null || s == "" || s == "\n" || s == "\r");
        }
    }
}