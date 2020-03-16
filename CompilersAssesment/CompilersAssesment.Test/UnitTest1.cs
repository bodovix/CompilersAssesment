using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompilersAssesment.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GwydTestFile_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(@"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment\TestSourceFIles\validDeclarations.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Invalid1_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(@"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment\TestSourceFIles\invalid1.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Invalid2_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(@"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment\TestSourceFIles\invalid2.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }

        [TestMethod]
        public void Valid1_Success()
        {
            //Arrange
            RunCompiler run = new RunCompiler();
            //Act
            run.Execute(@"C:\Users\Gwydion\source\repos\UNIVERSITY\CompilersAssesment\CompilersAssesment\CompilersAssesment\TestSourceFIles\valid1.txt");
            //Assert
            Assert.AreEqual(0, run.ErrorsForTests.Count);
        }
    }
}