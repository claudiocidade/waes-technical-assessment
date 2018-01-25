// <copyright file="FileCreatorTest.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Tests.Utilities
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using TechnicalAssessment.Console;

    /// <summary>
    /// A basic test class.
    /// </summary>
    [TestClass]
    public class FileManagerTest
    {
        private const string FolderName = "c:\\temp";

        /// <summary>
        /// A basic test method.
        /// </summary>
        [TestMethod]
        public void ShouldCreateNewFile()
        {
            string file = FileManager.Create(FolderName, 0.001);

            System.IO.File.Exists(file).ShouldBeTrue();
        }

        /// <summary>
        /// A basic test method.
        /// </summary>
        [TestMethod]
        public void ShouldDuplicateExistingFile()
        {
            string file = FileManager.Create(FolderName, 0.001);

            File.Exists(file).ShouldBeTrue();

            string copy = FileManager.Duplicate(file);

            File.Exists(copy).ShouldBeTrue();
        }
    }
}
