using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EComponentManager : Common.Singleton<EComponentManager>
{
    private List<EComponentBlock> ecList;
    public List<EComponentCommand> ecCommand;

    protected override void Init()
    {
        ecList = new List<EComponentBlock>();
        ecCommand = new List<EComponentCommand>();

        tempCommand = new EComponentCommand();
    }

    public void RegisterEC(EComponentBlock ec)
    {
        ecList.Add(ec);
    }
    
    public void DeleteEC(EComponentBlock ec)
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


    public void AddCommand(EComponentCommand command)
    {
        ecCommand.Add(command);
    }

    private EComponentCommand tempCommand;
    private bool haveTempCommand = false;

    public EComponentCommand ComsumeCommand()
    {
        if (ecCommand.Count == 0) return null;
        EComponentCommand c = ecCommand[0];
        ecCommand.Remove(c);

        switch (c.type)
        {
            case EComponentType.Attack:
                if (haveTempCommand)
                {
                    c.dmg += tempCommand.dmg;
                    c.multiTimes *= tempCommand.multiTimes;
                    c.scaleMultiTimes *= tempCommand.scaleMultiTimes;
                    haveTempCommand = false;
                    tempCommand = new EComponentCommand();
                }
                
                return c;
            case EComponentType.Support:
                tempCommand.dmg += c.dmg;
                tempCommand.multiTimes *= c.multiTimes;
                tempCommand.scaleMultiTimes *= c.scaleMultiTimes;
                haveTempCommand = true;
                return null;
        }

        return null;
    }
}

public enum EComponentType
{
    Attack,
    Support,
    Shield,
    None
}
public class EComponentCommand
{
    public EComponentType type;
    public int multiTimes;
    public float scaleMultiTimes;
    public float dmg;

    public EComponentCommand()
    {
        type = EComponentType.None;
        multiTimes = 1;
        scaleMultiTimes = 1;
        dmg = 1;
    }
}