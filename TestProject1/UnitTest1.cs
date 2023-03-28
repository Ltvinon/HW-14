
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataRow("2", "foobarfoobar")]
        [DataRow("5", "foobarfoobarfoobarfoobarfoobar")]
        [DataRow("", "")]
        public void TestMethod1(string str, string expected)
        {
            var consoleInput = new StringReader(str);
            Console.SetIn(consoleInput);
            // Arrange
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            Program.Main(new string[] { });
            string output = consoleOutput.ToString().Substring(29);

            // Assert
            Assert.AreEqual(expected, output);

            consoleOutput.Dispose(); // Очистка ресурсів
        }
    }
}