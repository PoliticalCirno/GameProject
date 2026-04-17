using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ItemSO/New")]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public bool isStackable = false;
    public Texture icon;
    public GameObject prefab;
}