
//#define LOG

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;

public static class LogWriter  {
    static string dir = @"Logs";
    private static string path = @"Logs/GameLog "+ DateTime.Now.ToString("yyyy-MM-dd")+" "+ DateTime.Now.ToString("HH-mm-ss") + ".txt";
 
    
    
    static LogWriter()
    {
        
    }

    public static void WriteLog(String line, GameObject callingObj)
    // schreibt eine Zeile ins Logfile, erstellt falls nötig das File und das directory
    {

#if (LOG)
        if (!Directory.Exists(path)) Directory.CreateDirectory(dir);
       
        using (StreamWriter sw = File.AppendText(path))
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            String CallingObjName = callingObj.name;
            string methodName = methodBase.Name;
            string typeName = methodBase.DeclaringType.Name;
            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss: ") + " ; " + CallingObjName + " ; " + typeName + " ; " + methodName + " ; " + line);
        }
#endif

    }
    public static void WriteLog(String line)
    // schreibt eine Zeile ins Logfile, erstellt falls nötig das File und das directory
    {

#if (LOG)
        if (!Directory.Exists(path)) Directory.CreateDirectory(dir);
         
        using (StreamWriter sw = File.AppendText(path))
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            string methodName = methodBase.Name;
            string typeName = methodBase.DeclaringType.Name;
            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss: ") + " ; " + "Kein Objektname gegeben" + " ; " + typeName + " ; " + methodName + " ; " + line);
        }
#endif

    }
}
