using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private List<NpcScript> npcs;
    private int level;
    private GameData.Level[] levels;

    public void LoadData(GameData data)
    {
        this.level = data.level;
        this.levels = data.levels;
    }
    public void SaveData(ref GameData data) { }

    void Start()
    {
        foreach (GameData.Level gameLevel in levels)
        {
            if (level == gameLevel.level)
            {
                foreach (NpcScript npc in npcs)
                {
                    npc.SetActive();
                    if (!gameLevel.subjects.Any(x => x.subject == npc.GetSubject()))
                    {
                        npc.SetHidden();
                    }
                }
            }
        }
    }
}
