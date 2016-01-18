using UnityEngine;
using System.Collections;
using TaidouCommon.Model;

public enum SkillType{
    Basic, 
    Skill
}
public enum PosType {
    Basic,One=1,Two=2,Three=3
}

public class Skill  {
    private int id;
    private string name;
    private string icon;
    private PlayerType playerType;
    private SkillType skillType;
    private PosType posType;
    private int coldTime;
    private int damage;
    private int level = 1;

    private SkillDB skillDB;

    public void Sync(SkillDB skillDB)
    {
        this.SkillDB = skillDB;
        Level = skillDB.Level;
    }

    public SkillDB SkillDB
    {
        get
        {
            return skillDB;
        }
        set { skillDB = value; }
    }

    public int Id {
        get { return id; }
        set { id = value; }
    }

    public string Name {
        get { return name; }
        set { name = value; }
    }

    public string Icon {
        get { return icon; }
        set { icon = value; }
    }

    public PlayerType PlayerType {
        get { return playerType; }
        set { playerType = value; }
    }

    public SkillType SkillType {
        get { return skillType; }
        set { skillType = value; }
    }

    public PosType PosType {
        get { return posType; }
        set { posType = value; }
    }

    public int ColdTime {
        get { return coldTime; }
        set { coldTime = value; }
    }

    public int Damage {
        get { return damage; }
        set { damage = value; }
    }

    public int Level {
        get { return level; }
        set { level = value; }
    }


    public void Upgrade() {
        Level++;
        if (skillDB == null)
        {
            skillDB = new SkillDB();
            skillDB.Level = Level;
            skillDB.SkillID = id;
        }
        else
        {
            skillDB.Level = Level;
        }
    }

}