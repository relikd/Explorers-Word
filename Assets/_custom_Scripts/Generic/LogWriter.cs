
#define LOG

using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace XplrDebug
{
	/**
	 * Static class for handling the structured debug logging
	 */
	public static class LogWriter {

		#if (LOG)
		/**
		 * The directory for all log files */
		private static string dir = @"Logs";
		/**
		 * The name of the log file. Will be generated each time the game starts. */
		private static string path = @"Logs/GameLog "+ DateTime.Now.ToString("yyyy-MM-dd")+" "+ DateTime.Now.ToString("HH-mm-ss") + ".txt";
		#endif

		/**
		 * Writes information to the log file. Will automatically add timestamp, calling method, current GameObject and a custom string
		 * The output is ";" separated and can be used for table calculation programs like Excel
		 * If the user don't have writting permission, no log is created
		 * 
		 * @param line Custom information you want to log
		 * @param callingObj The calling object to identify which object caused the logging
		 */
		public static void Write(String line, GameObject callingObj) {
#if (LOG)
            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(dir);

                using (StreamWriter sw = File.AppendText(path))
                {
                    StackTrace stackTrace = new StackTrace();
                    MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                    String CallingObjName = (callingObj == null ? "Kein Objektname gegeben" : callingObj.name);
                    string methodName = methodBase.Name;
                    string typeName = methodBase.DeclaringType.Name;
                    sw.WriteLine(DateTime.Now.ToString("HH:mm:ss: ") + " ; " + CallingObjName + " ; " + typeName + " ; " + methodName + " ; " + line);
                }
            }
            catch(Exception) {

            }
			#endif
		}

		/**
		 * Just calls the {@link #Write(String, GameObject)} method without an object
		 */
		public static void Write(String line) {
			#if (LOG)
			Write (line, null);
			#endif
		}
	}
}