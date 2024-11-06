using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Program : MonoBehaviour
{
    private static Program singleton = null;
    public static Program Get()
    {
        if(singleton == null)
            singleton = GameObject.FindObjectOfType<Program>();
        return singleton;
    }
    [SerializeField] TMP_Text resultText = null;
    [SerializeField] string classPath = null;
    StrategyRunner runner = null;
    private void Start()
    {
        runner = new StrategyRunner();
        var findStrategy = CreateStrategyFromClassName(classPath.Contains("strategy_",
            StringComparison.OrdinalIgnoreCase)?classPath : "Strategy_"+classPath);
        if (findStrategy != null)
        {
            runner.SetStrategy(findStrategy);
            runner.Run();
        }
    }
    private void Update()
    {
        runner.Update();
    }
    public Strategy GetCurrentStrategy()
    {
        return runner.GetStrategy();
    }
    Strategy CreateStrategyFromClassName(string className)
    {
        Type type = Type.GetType(className);
        if(type != null)
        {
            object instance = Activator.CreateInstance(type);
            if(instance is Strategy myClassInstance)
            {
                return (Strategy)instance;
            }
        }
        Program.WriteLine("Type not found");
        return null;
    }
    public static void WriteLine(object line = null) {
        if (line == null)
            Get().resultText.text += "\r\n";
        else
            Get().resultText.text += line + "\r\n";
    }
    public static void WriteLine(object line, params object[] args)
    {
        if(line != null)
        {
            string lineResult = string.Format(line.ToString(), args);
            Get().resultText.text += lineResult + "\r\n";
        }
        else
        {
            throw new FormatException("Parameter line is null");
        }
    }
    public static GameObject FindObject(string name)
    {
        if (!name.Contains('/'))
        {
            return null;
        }
        Transform finded = null;
        string[] hierachy;
        if (name.StartsWith('/'))
        {
            name = name.Substring(1);
            hierachy = name.Split('/');

            GameObject go =GameObject.Find(hierachy[0]);
            if(go != null)
            {
                finded = go.transform;
            }
            else
            {
                return null;
            }
            name = name.Substring(hierachy[0].Length);
        }
        hierachy = name.Split('/');
        foreach (string KeyName in hierachy)
        {
            finded = finded.Find(KeyName);
            if(finded == null)
            {
                return null;
            }
        }
        return finded.gameObject;
    }
}
