using UnityEngine;
using System.Collections.Generic;

public class Util {

    public static void Swap<T>(ref T a, ref T b) {
        var t = a; a = b; b = t;
    }

    public static void Shuffle<T>(List<T> xs) {
        var ar = xs.ToArray();
        Shuffle(ar);
        for (int i = 0; i < xs.Count; i++) {
            xs[i] = ar[i];
        }
    }

    public static void Shuffle<T>(T[] xs) {
        for (int i = 0; i < xs.Length; i++) {
            int randIndex = UnityEngine.Random.Range(0, xs.Length);
            var t = xs[i];
            xs[i] = xs[randIndex];
            xs[randIndex] = t;
        }
    }
}
