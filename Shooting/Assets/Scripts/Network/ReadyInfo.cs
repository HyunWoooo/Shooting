using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using Network;

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class ReadyInfo {

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    int protocol_id;

    [MarshalAs(UnmanagedType.Bool, SizeConst = 1)]
    bool is_Ready;

    public ReadyInfo()
    {
    }

    public ReadyInfo(int id)
    {
        protocol_id = id;
    }

    public void SetReady()
    {
        is_Ready = !is_Ready;
    }

	public bool GetReady()
    {
        return is_Ready;
    }
}
