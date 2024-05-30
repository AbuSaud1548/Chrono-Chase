using UnityEngine;

public class PlayerKeyTracker : MonoBehaviour
{
    public int startingAmountOfKeys = 0;
    
    int numberOfKeys = 0;

    void Start()
    {
        numberOfKeys = startingAmountOfKeys;
    }

    public void AddKey()
    {
        numberOfKeys++;
    }

    public void RemoveKey()
    {
        numberOfKeys = Mathf.Clamp(numberOfKeys - 1, 0, int.MaxValue);
    }

    public bool HasEnoughKeys(int minKeyAmount)
    {
        return numberOfKeys >= minKeyAmount;
    }

    public void ConsumeKey(int amountToConsume)
    {
        if (HasEnoughKeys(amountToConsume))
            numberOfKeys -= amountToConsume;
    }
}
