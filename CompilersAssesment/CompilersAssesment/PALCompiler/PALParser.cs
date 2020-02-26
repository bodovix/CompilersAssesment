using AllanMilne.Ardkit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompilersAssesment.PALCompiler
{
    internal class PALParser : RecoveringRdParser
    {
        public PALParser(IScanner scan) : base(scan)
        {
        }

        protected override void recStarter()
        {
            throw new NotImplementedException();
        }
    }
}