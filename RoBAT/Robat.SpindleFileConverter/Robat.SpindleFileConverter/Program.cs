﻿using System;
using System.IO;

namespace Robat.SpindleFileConverter
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ConsoleLogger.WriteError("No part program input file path has been given");

                return;
            }

            string path = args[0];
            if (!File.Exists(path))
            {
                ConsoleLogger.WriteError($"The part program file '{path}' does not exist");

                return;
            }

            try
            {
                Process(path);
            }
            catch (Exception exception)
            {
                ConsoleLogger.WriteError(exception);
            }

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine();
                Console.Write("Press any key to continue...");
                Console.ReadKey(intercept: true);
            }
        }

        private static void Process(string filePath)
        {
            try
            {
                var converter = new SpindleConverter(filePath);
                string outputFilePath = converter.Convert();

                ConsoleLogger.WriteSuccess($"File conversion completed successfully!");
                ConsoleLogger.WriteInformation($"Output file: {outputFilePath}");
            }
            catch
            {
                ConsoleLogger.WriteError($"Failed to convert the single-spindle command file");

                throw;
            }
        }
    }
}