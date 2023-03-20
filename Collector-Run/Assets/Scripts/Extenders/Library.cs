using System.Collections.Generic;
using UnityEngine;

namespace Extenders
{
    public static class Library
    {
        public static Dictionary<float, WaitForSeconds> waitList;

        /// <summary>
        /// Store wfs for next usage for better optimization
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static WaitForSeconds GetWait(this float time)
        {
            if (waitList == null) waitList = new Dictionary<float, WaitForSeconds>();
            if (!waitList.ContainsKey(time))
            {
                WaitForSeconds waitTime = new WaitForSeconds(time);
                waitList.Add(time, waitTime);
                return waitTime;
            }
        
            return waitList[time];
        }

        /// <summary>
        /// Destroys object
        /// </summary>
        /// <param name="go"></param>
        public static void Destroy(this GameObject go)
        {
            Object.Destroy(go);
        }
    }
}