using System;
using UnityEngine;

namespace RFMultipMod
{
    public class Utils : MonoBehaviour
    {
        public static void Log(String message)
        {
            Console.WriteLine("[INFO] [RFMpMod] " + message);
        }
        /// <summary>
        /// Logs all the gameobjects in a scene
        /// </summary>
        public static void LogAllGameObjects()
        {
            Log("All GameObjects in Scene: ");
            GameObject[] allGO = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (var oneGO in allGO)
            {
                Log(oneGO.name);
            }
        }

        public static void LogAllChildren(GameObject gameObject)
        {
            Log("Logging all " + gameObject.GetComponents<GameObject>().Length + " contained objects for " + gameObject);
            foreach (var obj in gameObject.GetComponentsInChildren<GameObject>())
            {
                Log("-" + obj);
            }
        }
    }
}
