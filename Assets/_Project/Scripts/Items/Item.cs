using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [TextArea, SerializeField] private string _description;

    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
}
