using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

public class InternalDatabase : Singleton<InternalDatabase>, ISaveable
{
    public static Dictionary<string, CSVSheetToUnity> database = new Dictionary<string, CSVSheetToUnity>();

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public object CaptureState()
    {
        return database;
    }

    public void RestoreState(object state)
    {
        database = (Dictionary<string, CSVSheetToUnity>)state;
    }
}
