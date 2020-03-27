using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CompilersAssesment.Test
{
    [TestClass]
    public class MyTests
    {
        public string SourceFolder { get; set; }
        public string GwydMissingKeywords { get; set; }

        public MyTests()
        {
            SourceFolder = @"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment.Test\TestSourceFIles\";
            GwydMissingKeywords = @"GwydMissingKeywords\";
        }

        private static void PrintErrorsFromRun(RunCompiler run)
        {
            foreach (string s in run.ErrorsForTests)
                Trace.WriteLine(s);
        }

        #region NormalTests

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

        #endregion NormalTests

        #region MissingKeywords

        [TestMethod]
        public void INPUTMissingIdentifiers_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"INPUTMissingIdentifiers.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissiingAS_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingAS.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingELSE_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingELSE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingEND_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingEND.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingENDIF_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingENDIF.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingENDLOOP_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingENDLOOP.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingIF_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingIF.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingIN_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingIN.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingPROGRAM_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingPROGRAM.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingREPEAT_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingREPEAT.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingTHEN_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingTHEN.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingUNTIL_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingUNITL.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingWITH_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"MissingWith.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void OUTPUTMissingIdentifiers_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + GwydMissingKeywords + @"OUTPUTMissingIdentifiers.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        #endregion MissingKeywords
    }
}