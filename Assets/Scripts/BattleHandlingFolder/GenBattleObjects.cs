using UnityEngine;

public abstract class GenBattleObjects : MonoBehaviour
{
    public abstract void localUpdate();
    public abstract void TakeAction();
    public abstract void addToList();
    public abstract void Die();
}
