[System.Serializable]
public class StackItem
{
    public Item item;
    public int amount;

    public StackItem(Item newItem,int amount)
    {
        item = newItem;
        this.amount = amount; 
    }
}
