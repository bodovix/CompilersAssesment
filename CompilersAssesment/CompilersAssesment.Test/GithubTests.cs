using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CompilersAssesment.Test
{
    [TestClass]
    public class GithubTests
    {
        public string SourceFolder { get; set; }

        public GithubTests()
        {
            SourceFolder = @"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment\TestSourceFIles\";
        }

        private static void PrintErrorsFromRun(RunCompiler run)
        {
            foreach (string s in run.ErrorsForTests)
                Trace.WriteLine(s);
        }

        [TestMethod]
        public void GwydTestFile_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"validDeclarations.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }
    }
}