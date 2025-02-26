// filepath: /C:/Проекты Юнити/NotFarm/Assets/_Project/Scripts/Interface/IInventoryItem.cs
public interface IInventoryItem
{
    void OnPickUp();
    void OnDrop(Vector3 dropPosition);
    Sprite GetIcon();
}