﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpicetifyManager.Source
{
    internal static class ProcessInvoker
    {
        public static List<string> Invoke(string fileName, string args = "", bool printToConsole = false)
        {
            Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                },
            };

            try
            {
                process.Start();
            }
            catch(Exception e)
            {
                Logger.Log(e.Message);
                Console.WriteLine(e);
                throw;
            }

            Logger.Log(fileName + " " + args);
            if(printToConsole)
                Console.WriteLine(fileName + " " + args);

            List<string> result = new();
            while(!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine() ?? string.Empty;
                result.Add(line);

                Logger.Log(ClearEscapeSeq(line), true);
                if(printToConsole)
                    Console.WriteLine("   " + ClearEscapeSeq(line));
            }

            process.WaitForExit();
            return result;
        }

        private static string ClearEscapeSeq(string text)
        {
            while(text.Contains("\u001b"))
            {
                int escIndex = text.IndexOf("\u001b", StringComparison.Ordinal);
                text = text.Remove(escIndex, text.IndexOf('m', escIndex) - escIndex + 1);
            }

            return text;
        }
    }
}