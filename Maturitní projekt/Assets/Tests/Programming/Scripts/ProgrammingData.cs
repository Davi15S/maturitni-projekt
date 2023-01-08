using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgrammingData
{
    public List<Level> levels = new List<Level>();

    public ProgrammingData()
    {
        Level level1 = new Level(1, new string[] { "for", "true", "foreach", "false" }, @"for (Gameobject floor in floors)<newLine>{<newLine><tab>floor.setActive(true)<newLine>}<newLine><newLine>while (Gameobject floor in floors)<newLine>{<newLine><tab>floor.setActive(true)<newLine>}");

        Level level2 = new Level(2, new string[] { "foreach", "false" }, @"for (Gameobject floor in floors)<newLine>{<newLine><tab>floor.setActive(true)<newLine>}");

        levels.AddRange(new List<Level> { level1, level2 });
    }

    public class Level
    {
        public int level;
        public string[] results;
        public string code;

        public Level(int iLevel, string[] strResults, string strCode)
        {
            level = iLevel;
            results = strResults;
            code = strCode;
        }
    }
}
