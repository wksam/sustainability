using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static List<string> prefix = new List<string>() { "", "K", "M", "G", "T", "P", "E", "Z", "Y", "R", "Q" };
    public static string FormatNumber(float amount)
    {
        if (amount == 0) return "0";
        int index = (int)(Mathf.Log10(amount) / 3);
        if (index < prefix.Count)
            return String.Format("{0:0.##}{1}", amount / Mathf.Pow(10, index * 3), prefix[index]);
        return String.Format("{0:#.##E+0}", amount);
    }
}
