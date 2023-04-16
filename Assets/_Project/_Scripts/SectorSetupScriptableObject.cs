using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Sector Setup")]
public class SectorSetupScriptableObject : ScriptableObject
{
    public float DemandPerCapita;
    public float SupplyEfficiency;
    public float PopulationEffect;
}
