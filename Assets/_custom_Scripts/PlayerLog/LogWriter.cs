
//#define LOG

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;

/**
 * Eine statische Klasse, die eine ueberladene
 * oeffentliche Funktion zur Verfuegung stellt,
 * um strukturierte Log Eintraege zu erzeugen.*/
public static class LogWriter {

#if (LOG)
    /**
     * Das Verzeichnis, in dem die Logdateien erstellt werden.*/
    private static string dir = @"Logs";
    /**
     * Der Name des Logdatei, die erzeugt wird.*/
    private static string path = @"Logs/GameLog "+ DateTime.Now.ToString("yyyy-MM-dd")+" "+ DateTime.Now.ToString("HH-mm-ss") + ".txt";
#endif

    /**
     * Eine oeffentliche Funktion, die eine Zeile in die Logdatei
     * eintraegt. Die Zeile umfasst das uebergebene GamebOject und
     * den uebergebenen Text. Das GO sollte immer das
     * aufrufende Object oder ein relevantes Parent-Object sein.
     * 
     * Die Ausgabe im Logfile ist durch ";" getrennt,
     * damit man sie in ein Tabellenkalkulationsprogramm
     * importieren kann. Sie umfasst die Uhrzeit,
     * den uebergebnen Objektnamen,
     * die Klasse und die Funktion aus der der Aufruf stammt,
     * sowie den uebergebenen String.*/
    public static void WriteLog(String line, GameObject callingObj)
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

    /**
     * Eine oeffentliche Funktion, die eine Zeile in die Logdatei
     * eintraegt.
     * 
     * Die Ausgabe im Logfile ist durch ";" getrennt,
     * damit man sie in ein Tabellenkalkulationsprogramm
     * importieren kann. Sie umfasst die Uhrzeit,
     * die Klasse und die Funktion aus der der Aufruf stammt,
     * sowie den uebergebenen String.*/
    public static void WriteLog(String line)
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
