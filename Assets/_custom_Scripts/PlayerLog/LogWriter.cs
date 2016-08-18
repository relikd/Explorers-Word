
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
    private static bool newgame = true;
    
    
    static LogWriter()
    {
        
    }

    public static void WriteLog(String line, GameObject callingObj)
    // schreibt eine Zeile ins Logfile, erstellt falls n�tig das File und das directory
    {

    #if (LOG)
        if (newgame)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(dir);
            }
            //F�gt eine Zeile mit der Startuhrzeit ein
            using (StreamWriter sw = File.CreateText(path)) sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ":");
            newgame = false;
        }
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
    // schreibt eine Zeile ins Logfile, erstellt falls n�tig das File und das directory
    {

#if (LOG)
        if (newgame)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(dir);
            }
            //F�gt eine Zeile mit der Startuhrzeit ein
            using (StreamWriter sw = File.CreateText(path)) sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ":");
            newgame = false;
        }
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
