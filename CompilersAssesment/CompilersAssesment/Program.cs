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

            //--- Open the input source file.
            StreamReader infile;
            try
            {
                infile = new StreamReader(args[0]);

                //--- Do what you gotta do!
                PALParser parser = new PALParser(new PALScanner());

                parser.Parse(infile);

                foreach (ICompilerError err in parser.Errors)
                    Console.WriteLine(err);

                infile.Close();
            }
            catch (IOException e)
            {
                PrintReadError("closing", args[0], e);
                return;
            }
        }

        private static void PrintReadError(String function, String filename, IOException e)
        {
            Console.WriteLine("Source File OI error {0} file {1}.", function, filename);
            Console.WriteLine(e);
        }
    }

    //=============================================================
}