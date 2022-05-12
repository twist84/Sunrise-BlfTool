﻿using Newtonsoft.Json;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using Sunrise.BlfTool;
using System;
using System.IO;
using WarthogInc.BlfChunks;

namespace WarthogInc
{
    class Program
    {
        static void Main(string[] args)
        {
            //File.Delete("C:\\Users\\codie\\Desktop\\user.bin");
            //var streamHelper = new BitStream<StreamByteStream>(new StreamByteStream(new FileStream("C:\\Users\\codie\\Desktop\\user.bin", FileMode.CreateNew)));

            //_blf blfFileHeader = new _blf();
            //ServiceRecordIdentity srid = new ServiceRecordIdentity()
            //{
            //    appearanceFlags = 0xFF,
            //    primaryColor = ServiceRecordIdentity.Color.SILVER,
            //    secondaryColor = ServiceRecordIdentity.Color.SILVER,
            //    tertiaryColor = ServiceRecordIdentity.Color.SILVER,
            //    isElite = ServiceRecordIdentity.PlayerModel.SPARTAN,
            //    foregroundEmblem = 2,
            //    backgroundEmblem = 2,
            //    emblemFlags = 0,
            //    playerName = "codietest",
            //    serviceTag = "CODIE",
            //    rank = ServiceRecordIdentity.Rank.GENERAL,
            //    grade = ServiceRecordIdentity.Grade.GRADE_4,
            //    totalEXP = 50000,
            //    campaignProgress = 5,
            //};

            //fupd fupd = new fupd()
            //{
            //    bungieUserRole = 1,
            //    highestSkill = 1,
            //    unknown0 = 1,
            //    hopperDirectory = "default_hoppers"
            //};

            //// This chunk should be refactored into a "BLF File Writer" which writes an array of chunks.
            //BLFChunkWriter bLFChunkWriter = new BLFChunkWriter();
            //bLFChunkWriter.WriteChunk(ref streamHelper, blfFileHeader);

            //bLFChunkWriter.WriteChunk(ref streamHelper, srid);
            //bLFChunkWriter.WriteChunk(ref streamHelper, fupd);
            //bLFChunkWriter.WriteChunk(ref streamHelper, new EndOfFile(blfFileHeader.GetLength() + srid.GetLength() + fupd.GetLength() + (0xC * 3)));
            //streamHelper.Write(0, 8);


            //var mhcf = JsonConvert.DeserializeObject<HopperConfigurationTable>(File.ReadAllText("../../../../json/matchmaking_hopper_011.json"));
            //var hopperFile = new BlfFile();
            //hopperFile.AddChunk(mhcf);
            //hopperFile.WriteFile("../../../../blf/matchmaking_hopper_011.bin");

            //var onfm = JsonConvert.DeserializeObject<Manifest>(File.ReadAllText("../../../../json/manifest_001.json"));
            //onfm.SetFileHash("/title/default_hoppers/matchmaking_hopper_011.bin", hopperFile.ComputeHash());
            //var manifestFile = new BlfFile();
            //manifestFile.AddChunk(onfm);
            //manifestFile.WriteFile("../../../../blf/manifest_001.bin");



            ////hoppersIn.Seek(0);
            ////File.Delete("C:\\Users\\codie\\Desktop\\matchmaking_hopper_011.bin");
            //var hoppersOut = new BitStream<StreamByteStream>(new StreamByteStream(new FileStream("../../../../blf/matchmaking_hopper_011.bin", FileMode.Create)));
            ////mhcf.configurations[4].name = "Hello, World!";
            //bLFChunkWriter.WriteChunk(ref hoppersOut, blfFileHeader);
            //bLFChunkWriter.WriteChunk(ref hoppersOut, mhcf);
            //bLFChunkWriter.WriteChunk(ref hoppersOut, new EndOfFile(blfFileHeader.GetLength() + mhcf.GetLength() + (0xC * 2)));
            //hoppersOut.Write(0, 8);

            var setting = new JsonSerializerSettings { Converters = { new ByteArrayConverter(), new HexStringConverter() }, Formatting = Formatting.Indented };
            //string output = JsonConvert.SerializeObject(mhcf, setting);

            ////File.WriteAllText("./out/matchmaking_hopper_011.json", output);
            ////streamHelper.Write(0, 8);

            //var mhdf = JsonConvert.DeserializeObject<Sunrise.BlfTool.HopperDescriptions>(File.ReadAllText("../../../../json/en/matchmaking_hopper_descriptions_003.json"));
            //var mhdf = new HopperReader().ReadNewHopperDescriptions(new BitStream<StreamByteStream>(new StreamByteStream(new FileStream("C:\\Users\\codie\\Desktop\\descriptions", FileMode.Open))));


            //var descriptionsOut = new BitStream<StreamByteStream>(new StreamByteStream(new FileStream("../../../../blf/en/matchmaking_hopper_descriptions_03.bin", FileMode.Create)));
            //bLFChunkWriter.WriteChunk(ref descriptionsOut, blfFileHeader);
            //bLFChunkWriter.WriteChunk(ref descriptionsOut, mhdf);
            //bLFChunkWriter.WriteChunk(ref descriptionsOut, new EndOfFile(blfFileHeader.GetLength() + mhdf.GetLength() + (0xC * 2)));
            //descriptionsOut.Write(0, 8);

            //BlfFile blfFile = new BlfFile();
            //blfFile.ReadFile(@"D:\Projects\Local\Halo 3 Matchmaking\title storage\title\default_hoppers\network_configuration_135.bin");
            //var netc = blfFile.GetChunk<NetworkConfiguration>();
            //string output = JsonConvert.SerializeObject(netc, setting);
            ////Directory.CreateDirectory("../../../../json/00101/");
            //File.WriteAllText("../../../../json/network_configuration_135.json", output);

            //BlfFile blfFile = new BlfFile();
            //blfFile.ReadFile(@"D:\Projects\Local\Halo 3 Matchmaking\title storage\title\default_hoppers\en\motd_popup.bin");
            //var motd = blfFile.GetChunk<MessageOfTheDayPopup>();
            //string output = JsonConvert.SerializeObject(motd, setting);
            //File.WriteAllText("../../../../json/en/motd_popup.json", output);



            //output = JsonConvert.SerializeObject(mhdf, setting);
            //File.WriteAllText("../../../../json/en/matchmaking_hopper_descriptions_003.json", output);



            ConvertBlfToJson("D:\\Projects\\Local\\Halo 3 Matchmaking\\title storage\\title\\default_hoppers\\", "../../../../json/");
        }

        public static void ConvertBlfToJson(string titleStorageFolder, string jsonFolder)
        {
            var titleDirectoryEnumerator = Directory.EnumerateFiles(titleStorageFolder, "*.*", SearchOption.AllDirectories).GetEnumerator();

            var jsonSettings = new JsonSerializerSettings { Converters = { new ByteArrayConverter(), new HexStringConverter() },  Formatting = Formatting.Indented };

            while (titleDirectoryEnumerator.MoveNext())
            {
                // We remake the manifest on conversion back to BLF.
                if (titleDirectoryEnumerator.Current.EndsWith("manifest_001.bin"))
                    continue;

                if (titleDirectoryEnumerator.Current.EndsWith(".bin"))
                {
                    try
                    {
                        BlfFile blfFile = new BlfFile();
                        blfFile.ReadFile(titleDirectoryEnumerator.Current);
                        var blfChunk = blfFile.GetChunk(1);
                        string output = JsonConvert.SerializeObject((object)blfChunk, jsonSettings);


                        string fileRelativePath = titleDirectoryEnumerator.Current.Replace(titleStorageFolder, "");
                        if (fileRelativePath.Contains("\\"))
                        {
                            string fileDirectoryRelativePath = fileRelativePath.Substring(0, fileRelativePath.LastIndexOf("\\"));
                            Directory.CreateDirectory(jsonFolder + fileDirectoryRelativePath);
                        }
                        File.WriteAllText(jsonFolder + fileRelativePath.Replace(".bin", ".json"), output);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to convert file: " + titleDirectoryEnumerator.Current);
                        //Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}
