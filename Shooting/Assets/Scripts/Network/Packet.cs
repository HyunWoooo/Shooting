using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Collections;
using Network;

public class Packet
{

    byte[] header;
    byte[] packet;
    byte[] body;


    public static byte[] Serialize(object data)
    {
        try
        {
            int rawsize = Marshal.SizeOf(data);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(data, buffer, false);
            byte[] RawData = new byte[rawsize];
            Marshal.Copy(buffer, RawData, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return RawData;
            //MemoryStream ms = new MemoryStream(512);
            //BinaryFormatter bf = new BinaryFormatter();
            //bf.Serialize(ms, data);
            //return ms.ToArray();
        }
        catch
        {
            Debug.Log("Packet Serialize fail");
            return null;
        }
    }

    public static object Deserialize(byte[] data, Type type)
    {
        try
        {
            int rawsize = Marshal.SizeOf(type);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.Copy(data, 0, buffer, rawsize);
            object retobj = Marshal.PtrToStructure(buffer, type);
            Marshal.FreeHGlobal(buffer);
            return retobj;

            //MemoryStream ms = new MemoryStream(512);
            //ms.Write(data, 0, data.Length);
            //ms.Position = 0;

            //BinaryFormatter bf = new BinaryFormatter();
            //object obj = bf.Deserialize(ms);
            //ms.Close();
            //return obj;
        }
        catch
        {
            Debug.Log("Packet Deserialize fail");
            return null;
        }
    }

    public void Packet_Create(byte[] data)
    {
        //header = System.BitConverter.GetBytes(protocol);
        //body = System.Text.Encoding.UTF8.GetBytes(data);

        //header = data.Length; ;
        body = data;

        packet = new byte[512];
        //System.Array.Copy(header, 0, packet, 0, header.Length);
        System.Array.Copy(body, 0, packet, 0, body.Length);
    }

    public byte[] GetPacket()
    {
        return packet;
    }
}
