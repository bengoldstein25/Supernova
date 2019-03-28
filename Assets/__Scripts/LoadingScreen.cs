using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingScreen {

    private static string from, to;
    
    public static string From {
        get {
            return from;
        } set {
            from = value;
        }
    }

    public static string To {
        get {
            return to;
        } set {
            to = value;
        }
    }

}
