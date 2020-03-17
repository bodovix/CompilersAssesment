using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CompilersAssesment.Test
{
    [TestClass]
    public class UnitTest1
    {
        public string SourceFolder { get; set; }

        public UnitTest1()
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

        [TestMethod]
        public void GwydTestFile_VarNotDeclared_Invalid()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"GwydInvalid_NotDeclared.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(3, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Invalid2_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"invalid2.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Valid1_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"valid1.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }
    }
}