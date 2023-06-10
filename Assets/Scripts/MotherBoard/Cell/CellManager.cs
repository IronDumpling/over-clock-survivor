using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellManager : Common.Singleton<CellManager>
{
    private List<CellBlock> ecList;
    public List<CellCommand> ecCommand;

    protected override void Init()
    {
        ecList = new List<CellBlock>();
        ecCommand = new List<CellCommand>();

        tempCommand = new CellCommand();
    }

    public void RegisterEC(CellBlock ec)
    {
        ecList.Add(ec);
    }
    
    public void DeleteEC(CellBlock ec)
    {
        ecList.Remove(ec);
        
    }

    public void ResetECListRun()
    {
        foreach (var ec in ecList)
        {
            ec.CanRun = true;
        }
    }


    public void AddCommand(CellCommand command)
    {
        ecCommand.Add(command);
    }

    private CellCommand tempCommand;
    private bool haveTempCommand = false;

    public CellCommand ComsumeCommand()
    {
        if (ecCommand.Count == 0) return null;
        CellCommand c = ecCommand[0];
        ecCommand.Remove(c);

        switch (c.type)
        {
            case CellType.Attack:
                if (haveTempCommand)
                {
                    c.dmg += tempCommand.dmg;
                    c.multiTimes *= tempCommand.multiTimes;
                    c.scaleMultiTimes *= tempCommand.scaleMultiTimes;
                    haveTempCommand = false;
                    tempCommand = new CellCommand();
                }
                
                return c;
            case CellType.Support:
                tempCommand.dmg += c.dmg;
                tempCommand.multiTimes *= c.multiTimes;
                tempCommand.scaleMultiTimes *= c.scaleMultiTimes;
                haveTempCommand = true;
                return null;
        }

        return null;
    }
}

public enum CellType
{
    Attack,
    Support,
    Shield,
    None
}
public class CellCommand
{
    public CellType type;
    public int multiTimes;
    public float scaleMultiTimes;
    public float dmg;

    public CellCommand()
    {
        type = CellType.None;
        multiTimes = 1;
        scaleMultiTimes = 1;
        dmg = 1;
    }
}