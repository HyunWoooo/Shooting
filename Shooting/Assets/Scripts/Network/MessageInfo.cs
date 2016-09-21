using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using Network;

[Serializable]
public class MessageInfo {

    [Serializable]
    struct Header
    {
        public PROTOCOL protocol_id;
    }

    string data;
    Header header;

    public MessageInfo()
    {
    }

    public MessageInfo(int id)
    {
        header.protocol_id = (PROTOCOL)id;
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
