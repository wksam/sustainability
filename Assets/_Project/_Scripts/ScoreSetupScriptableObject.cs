using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Score Setup")]
public class ScoreSetupScriptableObject : ScriptableObject
{
    public int CapitalIndexMin;
    public int CapitalIndexMax;
    public int EducationIndexMin;
    public int EducationIndexMax;
    public int HealthIndexMin;
    public int HealthIndexMax;
}
