using AllanMilne.Ardkit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompilersAssesment.PALCompiler
{
    internal class PALParser : RecoveringRdParser
    {
        private PALScemantics semantics;

        public PALParser(IScanner scan) : base(scan)
        {
            scanner = scan;
            semantics = new PALScemantics(this);
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

        private void recVarDecls()
        {//()* = 0 or more
            List<IToken> tokens = new List<IToken>();
            while (have(Token.IdentifierToken))
            {
                tokens = recIdentList();
                mustBe("AS");
                recType();
                foreach (IToken token in tokens)
                {
                    semantics.DeclareId(token);
                }
            }
        }

        private List<IToken> recIdentList()
        {//<IdentList> ::= Identifier ( , Identifier)* ;     //()* = 0 or more
            List<IToken> tokens = new List<IToken>();
            tokens.Add(scanner.CurrentToken);
            mustBe(Token.IdentifierToken);
            while (have(","))
            {
                mustBe(",");
                tokens.Add(scanner.CurrentToken);
                mustBe(Token.IdentifierToken);
            }
            return tokens;
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

        private int recExpression()   //not totally sure about the semantics here
        {//<Expression> ::= <Term> ( (+|-) <Term>)* ;   //()* = 0 or more
            IToken currentToken = scanner.CurrentToken;
            int leftTokenLangType, rightTokenLangType;

            leftTokenLangType = recTerm();
            while (have("+") || have("-"))
            {
                if (have("+"))
                    mustBe("+");
                else if (have("-"))
                    mustBe("-");
                else
                    syntaxError("<Expression>"); //shouldn't get hit
                rightTokenLangType = recTerm();

                //all types must be the same in the expression
                if (semantics.checkMatch(currentToken, leftTokenLangType, rightTokenLangType))
                    return leftTokenLangType;
                else
                    return rightTokenLangType;
            }
            return leftTokenLangType;//there was no right side so just return the left
        }

        private int recTerm() //not totally sure about the semantics here
        {//<Term> ::= <Factor> ( (*|/) <Factor>)* ;    //()* = 0 or more
            IToken currentToken = scanner.CurrentToken;
            int leftTokenLangType, rightTokenLangType;

            leftTokenLangType = recFactor();
            while (have("*") || have("/"))
            {
                if (have("*"))
                    mustBe("*");
                else if (have("/"))
                    mustBe("/");
                else
                    syntaxError("<Term>"); //shouldn't get hit
                rightTokenLangType = recFactor();
                if (semantics.checkMatch(currentToken, leftTokenLangType, rightTokenLangType))
                    return rightTokenLangType;
                else
                    return leftTokenLangType;//semantic error should  have been added by Ardkit?
            }
            return leftTokenLangType;//there was no right side so just return the left
        }

        private int recFactor()
        {//<Factor> ::= (+|-)? ( <Value> | "(" <Expression> ")" ) ;
            int tokenLanguageType;

            if (have("+"))
                mustBe("+");
            else if (have("-"))
                mustBe("-");
            else { }//do nothing

            if (have(Token.IdentifierToken) || have(Token.IntegerToken) || have(Token.RealToken))
            {//Value
                tokenLanguageType = recValue();
            }
            else if (have("("))
            {
                mustBe("(");
                tokenLanguageType = recExpression();
                mustBe(")");
            }
            else
            {
                syntaxError("<Factor>");
                tokenLanguageType = LanguageType.Undefined;
            }
            return tokenLanguageType;
        }

        private int recValue()
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
            IToken currentToken = scanner.CurrentToken;
            int leftTokenLangType = semantics.CheckId(currentToken);

            mustBe(Token.IdentifierToken);
            mustBe("=");
            int rightTokenLanType = recExpression();
            semantics.checkMatch(currentToken, leftTokenLangType, rightTokenLanType);
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
    }
}