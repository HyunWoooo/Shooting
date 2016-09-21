using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using Network;

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class PlayerInfo
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    public int protocol_id;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    public int user_id;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public Vector3 playerPos;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public Quaternion playerRot;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    public int health;
    [MarshalAs(UnmanagedType.U1)]
    public bool shoot;
    [MarshalAs(UnmanagedType.U1)]
    public bool walking;
    [MarshalAs(UnmanagedType.U1)]
    public bool death;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public bool[] skill;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public Vector3 CannonPos;


    public PlayerInfo()
    {
        skill = new bool[4];
    }

    public PlayerInfo(int id)
    {
        protocol_id = id;
        skill = new bool[4];
    }

}
