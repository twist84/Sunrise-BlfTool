﻿using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarthogInc.BlfChunks
{
    class BLFChunkWriter
    {
        public void WriteChunk(BitStream<StreamByteStream> outputStream, IBLFChunk blfChunk)
        {
            BLFChunkHeader header = new BLFChunkHeader(blfChunk);
            header.WriteHeader(outputStream);
            blfChunk.WriteChunk(outputStream);
        }
    }
}
