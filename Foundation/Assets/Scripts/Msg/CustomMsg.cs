using UnityEngine;
using UnityEngine.Networking;
    
namespace Msg
{
    public class CustomMsg : MessageBase
    {
        public int msgId;

        public string content;

        public Vector3 pos;

        public byte[] bytes;

        public override void Serialize(NetworkWriter writer)
        {
            base.Serialize(writer);
            writer.Write(msgId);
            writer.Write(content);
            writer.Write(pos);
            writer.WriteBytesAndSize(bytes, bytes.Length);
        }

        public override void Deserialize(NetworkReader reader)
        {
            base.Deserialize(reader);
            msgId = reader.ReadInt32();
            content = reader.ReadString();
            pos = reader.ReadVector3();
            bytes = reader.ReadBytesAndSize();
        }
    }
}