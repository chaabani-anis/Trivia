using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Trivia;

namespace Trivia.Test
{
    
    [TestClass]
    public class GoldenMasterTest
    {
        private const string GoldenMasterPath = "../../../GoldenMasterOutputs/";
        private const string ExecutionPath = "../../../ExecutionOutputs/";

        public GoldenMasterTest()
        {
            RemoveExecutionFiles();
        }

        private static void RemoveExecutionFiles()
        {
            string[] filePaths = Directory.GetFiles(ExecutionPath);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
        }

        [TestMethod]
        public void Should_Current_Output_Matches_Golden_master()
        {
            foreach (var seed in Enumerable.Range(0, 1000))
            {
                RunTheProgram(seed, ExecutionPath);
                var actual = File.ReadAllText(ExecutionPath + "output_" + seed.ToString() + ".txt");
                var goldenMaster = File.ReadAllText(GoldenMasterPath + "output_" + seed.ToString() + ".txt");
                Assert.AreEqual(goldenMaster, actual);
            }
        }

        private static void RunTheProgram(int seed, string outputFile)
        {
            using (var writer = File.CreateText(outputFile + "output_" + seed.ToString() + ".txt"))
            {
                Console.SetOut(writer);
                GameRunner.Play(new Random(seed));
            }
        }
    }
}
