using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Population Setup")]
public class PopulationSetupScriptableObject : ScriptableObject
{
    public float PopulationGrowthThreshold;
    public float PopulationGrowthNormalize;
}
