using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgrammingData
{
    public List<Level> levels = new List<Level>();
    private List<LevelContent> levelContents = new List<LevelContent>();
    private System.Random rn = new System.Random();

    public ProgrammingData()
    {
        levelContents.AddRange(new List<LevelContent> {
            new LevelContent(new string[] { "for", "true", "foreach", "false" }, @"for (Gameobject floor in floors)<newLine>{<newLine><tab>floor.setActive(true)<newLine>}<newLine><newLine>while (Gameobject floor in floors)<newLine>{<newLine><tab>floor.setActive(true)<newLine>}"),
            new LevelContent(new string[] { "foreach", "false" }, @"for (Gameobject floor in floors)<newLine>{<newLine><tab>floor.setActive(true)<newLine>}")
        });

        for (int i = 1; i < 3; i++)
        {
            int random = rn.Next(0, levelContents.Count);
            Level level = new Level(i, levelContents[random].results, levelContents[random].code);
            levels.Add(level);
        }
    }

    public class Level : LevelContent
    {
        public int level;
        public Level(int iLevel, string[] strResults, string strCode) : base(strResults, strCode)
        {
            level = iLevel;
        }
    }

    public class LevelContent
    {
        public string[] results;
        public string code;
        public LevelContent(string[] strResults, string strCode)
        {
            results = strResults;
            code = strCode;
        }
    }
}
