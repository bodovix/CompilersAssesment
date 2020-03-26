using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace CompilersAssesment.Test
{
    [TestClass]
    public class AmyInOrbitTests
    {
        public string SourceFolder { get; set; }
        public string SemanticErrors { get; set; }
        public string SyntaxErrors { get; set; }

        public AmyInOrbitTests()
        {
            SourceFolder = @"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment\TestSourceFIles\";
            SemanticErrors = @"GithubPpl\Amyinorbit\semantic-errors\";
            SyntaxErrors = @"GithubPpl\Amyinorbit\syntax-errors\";
        }

        private static void PrintErrorsFromRun(RunCompiler run)
        {
            foreach (string s in run.ErrorsForTests)
                Trace.WriteLine(s);
        }

        #region SemanticErrors

        [TestMethod]
        public void AssignTypeMissmatch_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SemanticErrors + @"assignTypeMismatch.txt");
            //Assert
            Assert.AreEqual(3, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void BoleanTypeMissmatch_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SemanticErrors + @"booleanTypeMismatch.txt");
            //Assert
            Assert.AreEqual(3, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void ExpressionTypeMissmatch_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SemanticErrors + @"expressionTypeMismatch.txt");
            //Assert
            Assert.AreEqual(3, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void UndeclaredVariable_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SemanticErrors + @"undeclaredVariable.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count); //TODO:Waiting on reply back from Adam about this one 4 or 2
        }

        [TestMethod]
        public void VariableReDeclaration_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SemanticErrors + @"variable-redeclaration.txt");
            //Assert
            Assert.AreEqual(2, run.ErrorsForTests.Count);
        }

        #endregion SemanticErrors

        #region Syntax Errors

        [TestMethod]
        public void BadAssignment_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"badAssignment.txt");
            //Assert
            Assert.AreEqual(3, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void InvalidChar_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"invalidchar.pal.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void InvalidIdent_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingDeclarationComma_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingEnd_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingProgramIdent_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingStatements_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoBooleanConditional_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NonExistentType_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoProgramName_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoProgram_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoTypeDeclaration_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoVariables_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void ValueAssignment_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void StatementAfterProgram_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void TrailingDeclarationComma_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"TODOCOPYNAMEHERE.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }
    }

    #endregion Syntax Errors
}