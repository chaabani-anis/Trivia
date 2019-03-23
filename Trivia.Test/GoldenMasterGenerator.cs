using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Trivia.Test
{
    //[TestClass]
    public class GoldenMasterGenerator
    {
        //[TestMethod]
        public void GenerateGoldenMasterFiles()
        {
            foreach (var seed in Enumerable.Range(0, 1000))
            {
                GameToFile(seed);
            }
        }

        private void GameToFile(int seed)
        {
            string fileName = "../../../GoldenMasterOutputs/output_" + seed.ToString() + ".txt";
            using (var writer = File.CreateText(fileName))
            {
                Console.SetOut(writer);
                GameRunner.Play(new Random(seed));
            }
        }
    }
}
