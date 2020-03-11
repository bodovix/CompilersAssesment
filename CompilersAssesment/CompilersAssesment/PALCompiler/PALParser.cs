﻿using AllanMilne.Ardkit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompilersAssesment.PALCompiler
{
    internal class PALParser : RecoveringRdParser
    {
        private PALScemantics scemantics;

        public PALParser(IScanner scan) : base(scan)
        {
            scanner = scan;
            scemantics = new PALScemantics(this);
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
            if (have(Token.IdentifierToken))
            {//Assignment
                recAssignment();
            }
            else if (have("UNTIL"))
            {//loop
                recLoop();
            }
            else if (have("IF"))
            {//condition
                recConditional();
            }
            else if (have("INPUT") || have("OUTPUT"))
            {//I-O
                recIO();
            }
            else
            {
                syntaxError("<Statement>");
            }
        }

        private void recIO()
        {
            if (have("INPUT"))
            {
                mustBe("INPUT");
                recIdentList();
            }
            else if (have("OUTPUT"))
            {
                mustBe("OUTPUT");
                recExpression();
                //( , <Expression>)* = 0 or more
                while (have(","))
                {
                    mustBe(",");
                    recExpression();
                }
            }
            else
            {
                syntaxError("<IO>");
            }
        }

        private void recExpression()
        {//<Expression> ::= <Term> ( (+|-) <Term>)* ;   //()* = 0 or more
            recTerm();
            while (have("+") || have("-"))
            {
                if (have("+"))
                    mustBe("+");
                else if (have("-"))
                    mustBe("-");
                else
                    syntaxError("<Expression>"); //shouldn't get hit
                recTerm();
            }
        }

        private void recTerm()
        {//<Term> ::= <Factor> ( (*|/) <Factor>)* ;    //()* = 0 or more
            recFactor();
            while (have("*") || have("/"))
            {
                if (have("*"))
                    mustBe("*");
                else if (have("/"))
                    mustBe("/");
                else
                    syntaxError("<Term>"); //shouldn't get hit
                recFactor();
            }
        }

        private void recFactor()
        {//<Factor> ::= (+|-)? ( <Value> | "(" <Expression> ")" ) ;
            if (have("+"))
                mustBe("+");
            else if (have("-"))
                mustBe("-");
            else { }//do nothing

            if (have(Token.IdentifierToken) || have(Token.IntegerToken) || have(Token.RealToken))
            {//Value
                recValue();
            }
            else if (have("("))
            {
                mustBe("(");
                recExpression();
                mustBe(")");
            }
            else
                syntaxError("<Factor>");
        }

        private void recValue()
        {
            if (have(Token.IdentifierToken))
            {
                mustBe(Token.IdentifierToken);
            }
            else if (have(Token.IntegerToken))
            {
                mustBe(Token.IntegerToken);
            }
            else if (have(Token.RealToken))
            {
                mustBe(Token.RealToken);
            }
            else
                syntaxError("<Value>");
        }

        private void recConditional()
        {//<Conditional> ::= IF <BooleanExpr> THEN (<Statement>)* ( ELSE (<Statement>)* )? ENDIF ;        ()* = 0 or more  | ()? 0 or 1
            mustBe("IF");
            recBooleanExpression();
            mustBe("THEN");
            while (have(Token.IdentifierToken) || have("UNTIL") || have("IF") || have("INPUT") || have("OUTPUT"))
            {
                recStatement();
            }
            if (have("ELSE"))
            {
                mustBe("ELSE");
                while (have(Token.IdentifierToken) || have("UNTIL") || have("IF") || have("INPUT") || have("OUTPUT"))
                {
                    recStatement();
                }
            }
            else { }//do nothing
            mustBe("ENDIF");
        }

        private void recBooleanExpression()
        {
            recExpression();
            if (have("<"))
            {
                mustBe("<");
            }
            else if (have("="))
            {
                mustBe("=");
            }
            else if (have(">"))
            {
                mustBe(">");
            }
            else
                syntaxError("<Expression>");
            recExpression();
        }

        private void recLoop()
        {//<Loop> ::= UNTIL <BooleanExpr> REPEAT (<Statement>)* ENDLOOP ;           //()* = 0 or more
            mustBe("UNTIL");
            recBooleanExpression();
            mustBe("REPEAT");
            while (have(Token.IdentifierToken) || have("UNTIL") || have("IF") || have("INPUT") || have("OUTPUT"))
            {
                recStatement();
            }
            mustBe("ENDLOOP");
        }

        private void recAssignment()
        {
            mustBe(Token.IdentifierToken);
            mustBe("=");
            recExpression();
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
            if (have("REAL"))
            {
                mustBe("REAL");
            }
            else if (have("INTEGER"))
            {
                mustBe("INTEGER");
            }
            else
                syntaxError("<recType>");
        }

        private void recIdentList()
        {//<IdentList> ::= Identifier ( , Identifier)* ;     //()* = 0 or more
            mustBe(Token.IdentifierToken);
            while (have(","))
            {
                mustBe(",");
                mustBe(Token.IdentifierToken);
            }
        }
    }
}