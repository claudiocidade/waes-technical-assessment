// <copyright file="FileManager.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Console
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Manages the files that are used to test the product.
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// Block size is set to 1024 * 8 in order to improve IO performance.
        /// </summary>
        private const int BlockSize = 1024 * 8;

        /// <summary>
        /// Blocks per mb.
        /// </summary>
        private const int BlocksPerMb = (1024 * 1024) / FileManager.BlockSize;

        /// <summary>
        /// Creates a new random binary file.
        /// </summary>
        /// 
        /// 
        /// <param name="directoryPath">Path to the save directory.</param>
        /// <param name="sizeInMb">Size of the file in MB.</param>
        /// <returns>New file path.</returns>
        public static string Create(string directoryPath, double sizeInMb)
        {
            string filePath = Path.Combine(directoryPath, $"{Guid.NewGuid()}.dat");

            byte[] data = new byte[FileManager.BlockSize];

            Random rng = new Random();

            using (FileStream stream = File.OpenWrite(filePath))
            {
                for (int i = 0; i < sizeInMb * FileManager.BlocksPerMb; i++)
                {
                    rng.NextBytes(data);

                    stream.Write(data, 0, data.Length);
                }
            }

            return filePath;
        }

        /// <summary>
        /// Duplicates an existing file.
        /// </summary>
        /// <param name="filename">Name of the file to be duplicated.</param>
        /// <returns>The name of the new file copy.</returns>
        public static string Duplicate(string filename)
        {
            string newFileName = $"{Path.Combine(new FileInfo(filename).DirectoryName, Guid.NewGuid().ToString())}.dat";

            File.Copy(filename, newFileName);

            return newFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">Name of the file to be encoded.</param>
        /// <returns>A base 64 representation string.</returns>
        public static string ConvertBase64(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return Convert.ToBase64String(reader.ReadBytes((int)stream.Length));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">Name of the file to be encoded.</param>
        /// <returns>The checksum representation string.</returns>
        public static string GetCheckSum(string filename)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                        
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static bool FileEquals(string fileName1, string fileName2)
        {
            using (FileStream first = new FileStream(fileName1, FileMode.Open))
            {
                using (FileStream second = new FileStream(fileName2, FileMode.Open))
                {
                    return FileStreamEquals(first, second);
                }
            }
        }

        private static bool FileStreamEquals(Stream left, Stream right)
        {
            const int bufferSize = 2048;

            byte[] buffer1 = new byte[bufferSize];

            byte[] buffer2 = new byte[bufferSize];

            while (true)
            {
                int count1 = left.Read(buffer1, 0, bufferSize);
                int count2 = right.Read(buffer2, 0, bufferSize);

                if (count1 != count2)
                    return false;

                if (count1 == 0)
                    return true;

                if (!buffer1.Take(count1).SequenceEqual(buffer2.Take(count2)))
                    return false;
            }
        }
    }
}