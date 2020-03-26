using AllanMilne.Ardkit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompilersAssesment.PALCompiler
{
    public class PALParser : RecoveringRdParser
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
            //program name identifier has no semantic value (ignore it)
            mustBe(Token.IdentifierToken);
            mustBe("WITH");
            RecVarDecls();
            mustBe("IN");
            do
            {
                RecStatement();   // MIGHT NEED TO LOOP THIS ( ()+ so one or more) - recursively call itself
            } while (have(Token.IdentifierToken) || have("UNTIL") || have("IF") || have("INPUT") || have("OUTPUT")); // while has tokens for terminal operators for this
            mustBe("END");

            Scope.CloseScope();
        }

        private void RecVarDecls()
        {//()* = 0 or more
            List<IToken> tokens = new List<IToken>();
            while (have(Token.IdentifierToken))
            {
                tokens = RecIdentList();
                mustBe("AS");
                int type = RecType();
                foreach (IToken token in tokens)
                {
                    semantics.DeclareId(token, type);
                }
            }
        }

        private List<IToken> RecIdentList()
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

        private void RecStatement()
        {
            if (have(Token.IdentifierToken))
            {//Assignment
                RecAssignment();// think semantics for this is done
            }
            else if (have("UNTIL"))
            {//loop
                RecLoop();
            }
            else if (have("IF"))
            {//condition
                RecConditional();// this semantics for this are done
            }
            else if (have("INPUT") || have("OUTPUT"))
            {//I-O
                RecIO();
            }
            else
            {
                syntaxError("<Statement>");
            }
        }

        private void RecIO()
        {
            if (have("INPUT"))
            {
                mustBe("INPUT");
                List<IToken> tokensInInput = RecIdentList();
                foreach (IToken token in tokensInInput)
                {
                    semantics.CheckId(token); // tokens must already exist in semantics tree - types can vary with input
                }
            }
            else if (have("OUTPUT"))//TODO: NOT SURE IF SEMANTICS NEEDED FOR THIS PART>>> testing to do
            {
                mustBe("OUTPUT");
                RecExpression();
                //( , <Expression>)* = 0 or more
                while (have(","))
                {
                    mustBe(",");
                    RecExpression();
                }
            }
            else
            {
                syntaxError("<IO>");
            }
        }

        private int RecExpression()
        {//<Expression> ::= <Term> ( (+|-) <Term>)* ;   //()* = 0 or more
            IToken currentToken = scanner.CurrentToken;
            int leftTokenLangType, rightTokenLangType;

            leftTokenLangType = RecTerm();
            while (have("+") || have("-"))
            {
                if (have("+"))
                    mustBe("+");
                else if (have("-"))
                    mustBe("-");
                else
                    syntaxError("<Expression>"); //shouldn't get hit
                rightTokenLangType = RecTerm();

                //all types must be the same in the expression
                semantics.CheckMatch(currentToken, leftTokenLangType, rightTokenLangType);
            }

            return leftTokenLangType;//since types inside the expression match just return the left
        }

        private int RecTerm()
        {//<Term> ::= <Factor> ( (*|/) <Factor>)* ;    //()* = 0 or more
            IToken currentToken = scanner.CurrentToken;
            int leftTokenLangType, rightTokenLangType;

            leftTokenLangType = RecFactor();
            while (have("*") || have("/"))
            {
                if (have("*"))
                    mustBe("*");
                else if (have("/"))
                    mustBe("/");
                else
                    syntaxError("<Term>"); //shouldn't get hit
                rightTokenLangType = RecFactor();
                semantics.CheckMatch(currentToken, leftTokenLangType, rightTokenLangType);
            }
            return leftTokenLangType;//there was no right side so just return the left
        }

        private int RecFactor()
        {//<Factor> ::= (+|-)? ( <Value> | "(" <Expression> ")" ) ;
            int tokenLanguageType;

            if (have("+"))
                mustBe("+");
            else if (have("-"))
                mustBe("-");
            else { }//do nothing

            if (have(Token.IdentifierToken) || have(Token.IntegerToken) || have(Token.RealToken))
            {//Value
                tokenLanguageType = RecValue();
            }
            else if (have("("))
            {
                mustBe("(");
                tokenLanguageType = RecExpression();
                mustBe(")");
            }
            else
            {
                syntaxError("<Factor>");
                tokenLanguageType = LanguageType.Undefined;
            }
            return tokenLanguageType;
        }

        private int RecValue()
        {//<Value> ::= Identifier | IntegerValue | RealValue ;
            if (have(Token.IdentifierToken))
            {
                int tokenLangType = semantics.CheckId(scanner.CurrentToken);
                mustBe(Token.IdentifierToken);
                return tokenLangType;
            }
            else if (have(Token.IntegerToken))
            {
                mustBe(Token.IntegerToken);
                return LanguageType.Integer;
            }
            else if (have(Token.RealToken))
            {
                mustBe(Token.RealToken);
                return LanguageType.Real;
            }
            else
            {
                syntaxError("<Value>");
                return LanguageType.Undefined;
            }
        }

        private void RecConditional()
        {//<Conditional> ::= IF <BooleanExpr> THEN (<Statement>)* ( ELSE (<Statement>)* )? ENDIF ;        ()* = 0 or more  | ()? 0 or 1
            mustBe("IF");
            RecBooleanExpression();
            mustBe("THEN");
            while (have(Token.IdentifierToken) || have("UNTIL") || have("IF") || have("INPUT") || have("OUTPUT"))
            {
                RecStatement();
            }
            if (have("ELSE"))
            {
                mustBe("ELSE");
                while (have(Token.IdentifierToken) || have("UNTIL") || have("IF") || have("INPUT") || have("OUTPUT"))
                {
                    RecStatement();
                }
            }
            else { }//do nothing
            mustBe("ENDIF");
        }

        private void RecBooleanExpression()
        {
            IToken startingToken = scanner.CurrentToken;
            int leftTokenLanguageType = RecExpression();
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
            {
                syntaxError("<Expression>");
                return;
            }
            int rightTokenLanguagType = RecExpression();
            semantics.CheckMatch(startingToken, leftTokenLanguageType, rightTokenLanguagType);
        }

        private void RecLoop()
        {//<Loop> ::= UNTIL <BooleanExpr> REPEAT (<Statement>)* ENDLOOP ;           //()* = 0 or more
            mustBe("UNTIL");
            RecBooleanExpression();
            mustBe("REPEAT");
            while (have(Token.IdentifierToken) || have("UNTIL") || have("IF") || have("INPUT") || have("OUTPUT"))
            {
                RecStatement();
            }
            mustBe("ENDLOOP");
        }

        private void RecAssignment()
        {
            IToken currentToken = scanner.CurrentToken;
            int leftTokenLangType = semantics.CheckId(currentToken);

            mustBe(Token.IdentifierToken);
            mustBe("=");
            int rightTokenLanType = RecExpression();
            semantics.CheckMatch(currentToken, leftTokenLangType, rightTokenLanType);
        }

        private int RecType()
        {
            if (have("REAL"))
            {
                mustBe("REAL");
                return LanguageType.Real;
            }
            else if (have("INTEGER"))
            {
                mustBe("INTEGER");
                return LanguageType.Integer;
            }
            else
            {
                syntaxError("<recType>");
                return LanguageType.Undefined;
            }
        }
    }
}