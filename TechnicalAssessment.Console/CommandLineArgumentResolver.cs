// <copyright file="CommandLineArgumentResolver.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Console
{
    /// <summary>
    /// Program runtime initialization class.
    /// </summary>
    public class CommandLineArgumentResolver
    {
        /// <summary>
        /// Reads the command line arguments collection and 
        /// executes the specified operation accordingly.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public void Read(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
            }
        }

        /// <summary>
        /// Gets the specified argument value.
        /// </summary>
        /// <param name="command">Command line argument.</param>
        /// <param name="argument">Command argument character.</param>
        /// <returns>The command line argument value.</returns>
        private string GetArgumentValue(string command, char argument)
        {
            if (string.IsNullOrEmpty(command)) return string.Empty;
            
            return command.Replace($"/{argument}=", string.Empty);
        }
    }
}