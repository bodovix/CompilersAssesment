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
        public string ValidSource { get; private set; }

        public AmyInOrbitTests()
        {
            SourceFolder = @"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment\TestSourceFIles\";
            SemanticErrors = @"GithubPpl\first\semantic-errors\";
            SyntaxErrors = @"GithubPpl\first\syntax-errors\";
            ValidSource = @"GithubPpl\first\valid\";
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
            Assert.AreEqual(4, run.ErrorsForTests.Count);
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
            Assert.AreEqual(3, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void InvalidIdent_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"invalidident.pal.txt");
            //Assert
            Assert.AreEqual(2, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingDeclarationComma_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"missingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(4, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingEnd_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"missingEND.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingProgramIdent_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"missingProgramIdent.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void MissingStatements_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"missingStatements.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoBooleanConditional_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"nonBooleanConditional.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NonExistentType_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"nonexistentType.txt");
            //Assert
            Assert.AreEqual(3, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoProgramName_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"noprogname.pal.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoProgram_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"noProgram.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoTypeDeclaration_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"noTypeDeclaration.txt");
            //Assert
            Assert.AreEqual(2, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NoWithOrVariables_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"noWithOrVariables.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void ValueAssignment_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"rvalueAssignment.txt");
            //Assert
            Assert.AreEqual(2, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void StatementAfterProgram_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"statementAfterProgram.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void TrailingDeclarationComma_Fail()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + SyntaxErrors + @"trailingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(1, run.ErrorsForTests.Count);
        }

        #endregion Syntax Errors

        #region ValidTests

        [TestMethod]
        public void Basic_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + ValidSource + @"trailingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void EmptyIfElse_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + ValidSource + @"trailingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void EmptyLoop_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + ValidSource + @"trailingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Factorial_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + ValidSource + @"trailingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Fibonachi_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + ValidSource + @"trailingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void NestedFor_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + ValidSource + @"trailingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Squares_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(SourceFolder + ValidSource + @"trailingDeclarationComma.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        #endregion ValidTests
    }
}