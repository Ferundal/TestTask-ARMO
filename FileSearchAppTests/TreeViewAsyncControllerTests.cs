using NUnit.Framework;
using System;
using System.Reflection;
using TestTask_ARMO;

[TestFixture]
    public class FileHelperTests
    {
        [Test]
        public void TryGetTopLevelDirectoryName_ReturnsTrueAndCorrectDirectoryName()
        {
            // Arrange
            string filePath = "C:\\Users\\Username\\Documents\\ExampleFile.txt";
            string expectedDirectoryName = "Documents"; // The expected top-level directory name

            // Create an instance of the class under test
            var fileHelper = new TreeViewAsyncController();

            // Get the private method using reflection
            MethodInfo methodInfo = typeof(FileHelper).GetMethod("TryGetTopLevelDirectoryName", BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new Exception("Method not found");
            }

            // Act
            object[] parameters = { filePath, null }; // The second parameter is for the out parameter topLevelDirectoryName
            bool result = (bool)methodInfo.Invoke(fileHelper, parameters);
            string topLevelDirectoryName = (string)parameters[1];

            // Assert
            Assert.IsTrue(result, "Expected the method to return true");
            Assert.AreEqual(expectedDirectoryName, topLevelDirectoryName, "Expected the top-level directory name to match");
        }
    }
}