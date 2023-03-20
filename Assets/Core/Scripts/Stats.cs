[System.Serializable]
public struct Stats
{
    public float damage;
    public float defense;
    public float health;
    public float average => (damage + defense + health) / 3;
}