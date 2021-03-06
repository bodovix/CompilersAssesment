﻿using AllanMilne.Ardkit;
using AllanMilne.PALCompiler;
using CompilersAssesment.PALCompiler;
using System;
using System.Collections.Generic;
using System.IO;

namespace CompilersAssesment
{
    internal class Program
    {
        public static void Main(String[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid number of command arguments");
                return;
            }
            RunCompiler run = new RunCompiler();
            run.Execute(args[0]);
        }
    }
}