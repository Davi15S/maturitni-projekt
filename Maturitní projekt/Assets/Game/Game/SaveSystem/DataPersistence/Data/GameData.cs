using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int floor;
    public Vector3 playerPosition;
    public Level[] levels;
    [System.Serializable]
    public class Level
    {
        public int level;
        public LevelSubject[] subjects;
        public Level(int level, LevelSubject[] subjects)
        {
            this.level = level;
            this.subjects = subjects;
        }
    }

    [System.Serializable]
    public class LevelSubject
    {
        public Subject subject;
        public bool finished;
        public LevelSubject(Subject subject, bool finished)
        {
            this.subject = subject;
            this.finished = finished;
        }
    }

    public GameData()
    {
        this.floor = 0;
        playerPosition = new Vector3(0.4f, -0.9f, 0f);
        this.level = 1;
        this.levels = new Level[] { new Level(1, new LevelSubject[] { new LevelSubject(Subject.Math, false), new LevelSubject(Subject.Robotics, false), new LevelSubject(Subject.PE, false) }), new Level(2, new LevelSubject[] { new LevelSubject(Subject.Programming, false), new LevelSubject(Subject.Math, false), new LevelSubject(Subject.Network, false) }), new Level(3, new LevelSubject[] { new LevelSubject(Subject.Programming, false), new LevelSubject(Subject.Math, false), new LevelSubject(Subject.PE, false) }), new Level(4, new LevelSubject[] { new LevelSubject(Subject.Math, false), new LevelSubject(Subject.Czech, false), new LevelSubject(Subject.Programming, false) }) };
    }
}
