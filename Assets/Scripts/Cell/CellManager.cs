using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellManager : Common.Singleton<CellManager>
{
    private List<CardCell> cellList;
    public List<CellCommand> cellCommand;

    protected override void Init()
    {
        cellList = new List<CardCell>();
        cellCommand = new List<CellCommand>();

        tempCommand = new CellCommand();
    }

    public void RegisterCell(CardCell cell)
    {
        cellList.Add(cell);
    }
    
    public void DeleteCell(CardCell cell)
    {
        cellList.Remove(cell);
        
    }

    public void ResetCellListRun()
    {
        foreach (var cell in cellList)
        {
            cell.CanRun = true;
        }
    }

    public void AddCommand(CellCommand command)
    {
        cellCommand.Add(command);
    }

    private CellCommand tempCommand;
    private bool haveTempCommand = false;

    public CellCommand ComsumeCommand()
    {
        if (cellCommand.Count == 0) return null;
        CellCommand c = cellCommand[0];
        cellCommand.Remove(c);

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