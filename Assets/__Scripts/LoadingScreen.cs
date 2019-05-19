using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingScreen {

    private static string from, to;
    private static bool isSinglePlayer;
    
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

    public static bool IsSinglePlayer {
        get {
            return isSinglePlayer;
        } set {
            isSinglePlayer = value;
        }
    }

}
