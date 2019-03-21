using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Trivia;

namespace Trivia.Test
{
    
    [TestClass]
    public class GoldenMasterTest //: IDisposable
    {
        private readonly TextWriter _originalOut;
        private const string ActualOutput = "output.txt";
        private const string GoldenMaster = "../../../golden.txt";

        public GoldenMasterTest()
        {
            _originalOut = Console.Out;
        }

        [TestMethod]
        public void golden_master()
        {
            RunTheProgram(seed: 99, outputFile: ActualOutput, times: 1000);

            var actual = File.ReadAllText(ActualOutput);
            var goldenMaster = File.ReadAllText(GoldenMaster);
            Assert.AreEqual(goldenMaster, actual);
           
            
        }

        private static void RunTheProgram(int seed, string outputFile, int times)
        {
            using (var writer = File.CreateText(outputFile))
            {
                Console.SetOut(writer);
                foreach (var i in Enumerable.Range(0, times))
                {
                    GameRunner.Play(new Random(seed));
                }
            }
        }
    }
}
