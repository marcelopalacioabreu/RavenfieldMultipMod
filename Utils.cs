using System;
using System.IO;
using UnityEngine;

namespace RFMultipMod
{
    public class Utils : MonoBehaviour
    {
        private static bool _hasLoggedThisSession;
        private static readonly string LogFilePath = Application.dataPath + "\\mpmod.log";
        private static readonly object[] LockObj = { };

        private static StreamWriter _logWriter;

        public static void Log(string message)
        {
            Console.WriteLine("[INFO] [RFMpMod] " + message);
            if (!_hasLoggedThisSession && File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
            }

            lock (LockObj)
            {
                if (_logWriter == null)
                {
                    _logWriter =
                        new StreamWriter(File.Open(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.Read)) {AutoFlush = true};
                }   
            }
            
            _logWriter.WriteLine(message);


            _hasLoggedThisSession = true;
        }

        /// <summary>
        /// Logs all the gameobjects in a scene
        /// </summary>
        public static void LogAllGameObjects()
        {
            Log("All GameObjects in Scene: ");
            GameObject[] allGo = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            if (allGo == null)
            {
                Log("<Error retrieving game objects>");
                return;
            }
            
            foreach (var oneGo in allGo)
            {
                Log(oneGo.name);
            }
        }

        public static void LogAllChildren(GameObject gameObject)
        {
            Log("Logging all " + gameObject.transform.childCount + " contained objects for " + gameObject);
            foreach (Transform child in gameObject.transform)
            {
                Log("-" + child.gameObject);
            }
        }
    }
}