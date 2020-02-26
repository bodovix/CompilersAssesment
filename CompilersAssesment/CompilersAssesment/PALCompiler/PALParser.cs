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
            scanner = scan;
        }

        protected override void recStarter()
        {
            Scope.OpenScope();

            mustBe("PROGRAM");
            mustBe(Token.IdentifierToken);
            mustBe("WITH");
            recVarDecls();
            mustBe("IN");
            do
            {
                recStatement();   // MIGHT NEED TO LOOP THIS ( ()+ so one or more) - recursively call itself
            } while (have(Token.IdentifierToken) || have("UNTIL") || have("IF") || have("INPUT") || have("OUTPUT")); // while has tokens for terminal operators for this
            mustBe("END");

            Scope.CloseScope();
        }

        private void recStatement()
        {
            throw new NotImplementedException();
        }

        private void recVarDecls()
        {//()* = 0 or more
            while (have(Token.IdentifierToken))
            {
                recIdentList();
                mustBe("AS");
                recType();
            }
        }

        private void recType()
        {
            throw new NotImplementedException();
        }

        private void recIdentList()
        {//()* = 0 or more
            throw new NotImplementedException();
        }
    }
}