using AllanMilne.Ardkit;
using AllanMilne.PALCompiler;
using System;
using System.Collections.Generic;
using System.IO;

namespace CompilersAssesment
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //==================SCANNER CHECKS: CANNER WORKS DONT CHANGE IT
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: arg[] missing from program.");
                return;
            }

            StreamReader infile = null;
            try
            {
                infile = new StreamReader(args[0]);
            }
            catch (IOException e)
            {
                Console.WriteLine("I.O error opening file '{0}'. ", args[0]);
                Console.WriteLine(e);
                return;
            }
            List<ICompilerError> errors = new List<ICompilerError>();
            PALScanner scanner = new PALScanner();
            scanner.Init(infile, errors);
            do
            {
                scanner.NextToken();
                Display(scanner.CurrentToken);
            } while (!scanner.EndOfFile);

            try
            {
                infile.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("Error closing file '{0}'. ", args[0]);
                Console.WriteLine(e);
                return;
            }
        }

        private static void Display(IToken token)
        {
            Console.WriteLine($"{token.TokenType} at ({token.Line} {token.Column})");
            if (!token.TokenType.Equals(token.TokenValue))
                Console.WriteLine($"actual token : {token.TokenValue}");
        }

        //=============================================================
    }
}