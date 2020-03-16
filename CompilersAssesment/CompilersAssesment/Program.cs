using AllanMilne.Ardkit;
using AllanMilne.PALCompiler;
using CompilersAssesment.PALCompiler;
using System;
using System.Collections.Generic;
using System.IO;

namespace CompilersAssesment
{
    internal class Program
    {
        //==================PARSER CHECKS

        public static void Main(String[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid usage: PAL <filename>");
                return;
            }
            RunCompiler run = new RunCompiler();
            run.Execute(args[0]);
        }
    }

    //=============================================================
}