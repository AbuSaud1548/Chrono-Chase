using UnityEngine;

public class PlayerKeyTracker : MonoBehaviour
{
    public int startingAmountOfKeys = 0; // initial starting amount of key

    int numberOfKeys = 0; // Stores the number of keys with player

    void Start()
    {
        numberOfKeys = startingAmountOfKeys;
    }

    public int GetKey()
    {
        return numberOfKeys;
    }

    /// <summary>
    /// Add single key
    /// </summary>
    public void AddKey()
    {
        numberOfKeys++;
    }

    /// <summary>
    /// Removes single key
    /// </summary>
    public void RemoveKey()
    {
        numberOfKeys = Mathf.Clamp(numberOfKeys - 1, 0, int.MaxValue);
    }

    /// <summary>
    /// will return true when number of keys is above 0
    /// </summary>
    /// <returns></returns>
    public bool HasKey()
    {
        return numberOfKeys > 0;
    }

    /// <summary>
    /// checks whether there're enough keys
    /// </summary>
    /// <param name="minKeyAmount"></param>
    /// <returns></returns>
    public bool HasEnoughKeys(int minKeyAmount)
    {
        return numberOfKeys >= minKeyAmount;
    }

    /// <summary>
    /// Will remove a set amount of keys only if there is enough of it
    /// </summary>
    /// <param name="amountToConsume"></param>
    public void ConsumeKey(int amountToConsume)
    {
        if (HasEnoughKeys(amountToConsume))
            numberOfKeys -= amountToConsume;
    }
}
