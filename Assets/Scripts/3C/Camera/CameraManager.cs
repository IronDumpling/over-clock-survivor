using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Common;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineBrain _brain;
    private Transform _boardFollowPoint;

    public Transform boardFollowPoint
    {
        get => _boardFollowPoint;
        set => _boardFollowPoint = value;
    }

    public CinemachineVirtualCamera curCamera;
    public CinemachineVirtualCamera[] cameraList;
    public CinemachineBlendDefinition defaultBlend;

    public void SetDefaultBlend()
    {
        _brain.m_DefaultBlend = defaultBlend;
    }

    public void SetCustomBlend(CinemachineBlendDefinition customBlend)
    {
        _brain.m_DefaultBlend = customBlend;
    }

    public void SwitchCamera(int idx)
    {
        if (idx < 0 || idx >= cameraList.Length)
            DebugLogger.Error(this.name, $"The wanted switch camera No.{idx} does not exist");

        cameraList[idx].m_Priority = 10;
        if(curCamera) curCamera.m_Priority = 0;
        curCamera = cameraList[idx];
        PriorityCheck();
    }

    public void SwitchCamera(CinemachineVirtualCamera switchVC)
    {
        switchVC.m_Priority = 10;
        if (curCamera) curCamera.m_Priority = 0;
        curCamera = switchVC;
    }

    public int GetCurCameraIdx()
    {
        for (int i = 0; i < cameraList.Length; i++)
        {
            if (cameraList[i] == curCamera)
            {
                return i;
            }
        }

        DebugLogger.Log(this.name, "Did not find _currVC " + curCamera.name + "in CommonCameraList");
        return -1;
    }

    public void PriorityCheck()
    {
        CinemachineVirtualCamera maxPriority = null;

        foreach (CinemachineVirtualCamera vCam in FindObjectsOfType<CinemachineVirtualCamera>())
        {
            if (maxPriority == null)
            {
                maxPriority = vCam;
                continue;
            }

            maxPriority = (vCam.m_Priority > maxPriority.m_Priority) ? vCam : maxPriority;
        }

        if (curCamera != maxPriority)
        {
            DebugLogger.Log(this.name, "Default Main Camera did not Set in CameraManager. Set curCamera to " + maxPriority.name);
            curCamera = maxPriority;
        }
    }

    private void Awake()
    {
        _boardFollowPoint = transform.Find("BoardFollowPoint");

        if(cameraList != null && curCamera == null)
        {
            curCamera = cameraList[0];
        }

        if (cameraList == null || curCamera == null)
        {
            DebugLogger.Error(this.name, "Default Camera List not set.\n"
            + "Or Currrent Camera doesn't set succussfully.");
        }

        PriorityCheck();
    }

    private void Start()
    {
        _brain = FindObjectOfType<CinemachineBrain>();
    }
}
