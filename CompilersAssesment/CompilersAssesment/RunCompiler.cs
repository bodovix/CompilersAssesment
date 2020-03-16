using AllanMilne.Ardkit;
using AllanMilne.PALCompiler;
using CompilersAssesment.PALCompiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CompilersAssesment
{
    public class RunCompiler
    {
        public PALParser Parser { get; set; }
        public List<string> ErrorsForTests { get; set; }

        public RunCompiler()
        {
            ErrorsForTests = new List<string>();
        }

        public void Execute(string sourceLocation)
        {
            //--- Open the input source file.
            StreamReader infile;
            try
            {
                infile = new StreamReader(sourceLocation);

                //--- Do what you gotta do!
                PALParser parser = new PALParser(new PALScanner());

                parser.Parse(infile);

                foreach (ICompilerError err in parser.Errors)
                {
                    ErrorsForTests.Add(err.ToString());
                    Console.WriteLine(err);
                }

                infile.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}