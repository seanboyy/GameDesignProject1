using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants{
    public int InitialPlayerHealth = 10;
    public int Score = 0;

    private static Constants instance;

    private Constants() { }

    public static Constants Instance{
        get{
            if (instance == null) instance = new Constants();
            return instance;
        }
    }
}
