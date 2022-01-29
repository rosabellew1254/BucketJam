using UnityEngine;

[CreateAssetMenu]
public class PlantsSO : ScriptableObject
{
    public GameManager.plants type;
    public int seedCost;
    public int sellPrice;
    public int sanity;
    public int turnsToGrow;
}
