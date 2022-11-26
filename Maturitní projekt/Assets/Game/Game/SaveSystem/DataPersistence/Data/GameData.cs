using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int floor;
    public Vector3 playerPosition;

    public GameData()
    {
        this.floor = 0;
        playerPosition = new Vector3(0.4f, -0.9f, 0f);
        this.level = 1;
    }
}
