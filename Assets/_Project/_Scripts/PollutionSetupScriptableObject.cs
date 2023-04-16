using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Pollution Setup")]
public class PollutionSetupScriptableObject : ScriptableObject
{
    public float ProductionEffect;
    public float PopulationEffect;
    public float EducationEffect;
    public float Decay;
}
