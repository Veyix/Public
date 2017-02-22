using System.Collections.Generic;
using System.IO;

namespace Robat.SpindleFileConverter
{
    /// <summary>
    /// A class that converts single-spindle command files into double-spindle command files.
    /// </summary>
    public sealed class SpindleConverter
    {
        private readonly string _inputFilePath;

        public SpindleConverter(string inputFilePath)
        {
            Verify.NotNullOrEmpty(inputFilePath, nameof(inputFilePath));

            _inputFilePath = inputFilePath;
        }

        /// <summary>
        /// Converts the input file into a double-spindle command file.
        /// </summary>
        /// <returns>The path to the output file.</returns>
        public string Convert()
        {
            var commands = CreateCommandsFromInputFile();
            // TODO: Fix commands - error detection / correction.
            // TODO: Optimize commands.

            string outputFilePath = GetOutputFilePath(_inputFilePath);
            WriteCommands(commands, outputFilePath);

            return outputFilePath;
        }

        private IEnumerable<ICommand> CreateCommandsFromInputFile()
        {
            var factory = new CommandFactory();
            var lines = File.ReadLines(_inputFilePath);

            foreach (string commandText in lines)
            {
                yield return factory.CreateCommand(commandText);
            }
        }

        private void WriteCommands(IEnumerable<ICommand> commands, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                foreach (var command in commands)
                {
                    var translatedCommand = command.Translate();
                    writer.WriteLine(translatedCommand.CommandText);
                }
            }
        }

        private static string GetOutputFilePath(string inputFilePath)
        {
            string filename = Path.GetFileNameWithoutExtension(inputFilePath);
            return $"{filename}-converted.txt";
        }
    }
}