﻿using Newtonsoft.Json;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarthogInc.BlfChunks;

namespace Sunrise.BlfTool
{
    class MatchmakingBanhammerMessages : IBLFChunk
    {
        [JsonIgnore]
        public uint banMessagesCount { get { return (uint)banMessages.Length; } }
        public string[] banMessages;

        public ushort GetAuthentication()
        {
            return 1;
        }

        public uint GetLength()
        {
            return (uint)(banMessagesCount * 0x100) + 4;
        }

        public string GetName()
        {
            return "bhms";
        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            int tipCount = hoppersStream.Read<int>(32);
            banMessages = new string[tipCount];
            for (int i = 0; i < tipCount; i++)
            {
                byte[] tipBytes = new byte[0x100];
                int tipLength = tipBytes.Length;
                for (int j = 0; j < tipBytes.Length; j++)
                {
                    byte tipByte = hoppersStream.Read<byte>(8);
                    if (tipByte == 0)
                    {
                        tipLength = j;
                        hoppersStream.SeekRelative(tipBytes.Length - j - 1);
                        break;
                    } 
                    else
                    {
                        tipBytes[j] = tipByte;
                    }
                }

                banMessages[i] = Encoding.UTF8.GetString(tipBytes.Take(tipLength).ToArray());
            }
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            //hoppersStream.Write<byte>(gameEntryCount, 6);

            //for (int i = 0; i < gameEntryCount; i++)
            //{
            //    FileEntry entry = gameEntries[i];
            //    //hoppersStream.Write<ushort>(entry.identifier, 16);
            //    //hoppersStream.Write<byte>(description.type ? (byte)1 : (byte)0, 1);
            //    //hoppersStream.WriteString(description.description, 256, Encoding.UTF8);
            //}

            //hoppersStream.Seek(hoppersStream.NextByteIndex, 0);
        }
    }
}