using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CompilersAssesment.Test
{
    [TestClass]
    public class MyTests
    {
        public string SourceFolder { get; set; }

        public MyTests()
        {
            SourceFolder = @"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment.Test\TestSourceFIles\";
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
            Assert.AreEqual(2, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void GwydTestFile_Invalid_AlreadyDeclared_Int_Invalid()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"Invalid_AlreadyDeclared_Int.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void GwydTestFile_Invalid_AlreadyDeclared_IntInline_Invalid()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"Invalid_AlreadyDeclared_IntInline.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void GwydTestFile_Invalid_AlreadyDeclared_Real_Invalid()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"Invalid_AlreadyDeclared_Real.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void GwydTestFile_Invalid_AlreadyDeclared_RealInline_Invalid()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"Invalid_AlreadyDeclared_RealInline.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
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
            Assert.AreEqual(5, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Invalid_RealAssignedToIngeger_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"Invalid_RealAssignedToIngeger.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void EmptyFile_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"EmptyFile.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Invalid_Invalid_IntAssignedToReal_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"Invalid_IntAssignedToReal.txt");
            PrintErrorsFromRun(run);
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
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

        [TestMethod]
        public void Valid2_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"valid2.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Valid1_NoWhitespaceForNonKeywords_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"valid1_NoWhitespaceForNonKeywords.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void WithButNoVariables_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + @"WithButNoVariables.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }
    }
}