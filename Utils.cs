using System;
using System.IO;
using UnityEngine;

namespace RFMultipMod
{
    public class Utils : MonoBehaviour
    {
        private static bool _hasLoggedThisSession;
        private static readonly string _logFilePath = Application.dataPath + "\\mpmod.log";
        private static object[] _lockObj = { };

        private static StreamWriter _logWriter;

        public static void Log(String message)
        {
            Console.WriteLine("[INFO] [RFMpMod] " + message);
            if (!_hasLoggedThisSession && File.Exists(_logFilePath))
            {
                File.Delete(_logFilePath);
            }

            lock (_lockObj)
            {
                if (_logWriter == null)
                {
                    Console.WriteLine("Creating log writer.");
                    _logWriter =
                        new StreamWriter(File.Open(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read)) {AutoFlush = true};
                    Console.WriteLine("Created: " + _logWriter);
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
            GameObject[] allGO = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (var oneGO in allGO)
            {
                Log(oneGO.name);
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