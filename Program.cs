using System;
using System.Diagnostics;
using System.IO;

namespace HandbrakeAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Acquire the list of input video files
            string[] inputFiles = Directory.GetFiles("C:\\input", "*.mp4");

            // Define the Handbrake CLI executable path
            string handbrakeCLI = "C:\\HandBrakeCLI.exe";
            // Determine the preset to wield
            string preset = "Fast 720p30";
            // Traverse the input files list
            for (int i = 0; i < inputFiles.Length; i++)
            {
                // Acquire the input file path
                string inputFile = inputFiles[i];
                // Obtain the output file path
                string outputFile = Path.ChangeExtension(inputFile, ".mp4");
                outputFile = outputFile.Replace("input", "output");
                // Invoke the Handbrake CLI process
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = handbrakeCLI;
                startInfo.Arguments = string.Format("-i \"{0}\" -o \"{1}\" --preset=\"{2}\"", inputFile, outputFile, preset);
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardError = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    // Decipher the output and error streams
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    // Await the process's departure
                    process.WaitForExit();
                    // Examine for anomalies
                    if (process.ExitCode != 0)
                    {
                        Console.WriteLine("Anomaly transmuting file: " + inputFile);
                        Console.WriteLine(error);
                    }
                    else
                    {
                        Console.WriteLine("File transmuted: " + inputFile);
                    }
                }
            }
            Console.WriteLine("Transmutation complete!");
        }
    }
}



