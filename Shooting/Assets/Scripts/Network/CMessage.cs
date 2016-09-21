using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using Network;

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class CMessage {

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
    int protocol_id;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 508)]
    string data;

    public CMessage()
    {
    }

    public CMessage(int id)
    {
        protocol_id = id;
    }

    public void SetMessage(string msg)
    {
        data = msg;
    }

    public string GetMessageA()
    {
        return data;
    }
}
