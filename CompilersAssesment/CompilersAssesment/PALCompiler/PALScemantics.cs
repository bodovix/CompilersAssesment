using AllanMilne.Ardkit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompilersAssesment.PALCompiler
{
    public class PALScemantics : Semantics
    {
        public PALScemantics(IParser p) : base(p)
        {
        }

        public void DeclareId(IToken id)
        {
            if (!id.Is(Token.IdentifierToken)) return;  // only for identifier tokens.
            Scope symbols = Scope.CurrentScope;
            if (symbols.IsDefined(id.TokenValue))
            {
                semanticError(new AlreadyDeclaredError(id, symbols.Get(id.TokenValue)));
            }
            else
            {
                symbols.Add(new VarSymbol(id, currentType));
            }
        }

        public int CheckId(IToken id)
        {
            if (!Scope.CurrentScope.IsDefined(id.TokenValue))
            {
                semanticError(new NotDeclaredError(id));
                return LanguageType.Undefined;
            }
            else return (CheckType(id));
        }

        //--- Check type compatibility of current token.
        //--- Side-effect: sets current type if it is undefined at present.
        //--- Must only be called for identifiers that are declared.
        public int CheckType(IToken token)
        {
            int thisType = LanguageType.Undefined;
            if (token.Is(Token.IdentifierToken))
            {
                thisType = Scope.CurrentScope.Get(token.TokenValue).Type;
                return thisType;
            }
            else if (token.Is(Token.IntegerToken))
            {
                thisType = LanguageType.Integer;
                return thisType;
            }
            else if (token.Is(Token.RealToken))
            {
                thisType = LanguageType.Real;
                return thisType;
            }
            // if not already set then set the current type being processed.
            if (currentType == LanguageType.Undefined)
            {
                currentType = thisType;
                return thisType;
            }
            if (currentType != thisType)
            {
                semanticError(new TypeConflictError(token, thisType, currentType));
            }
            return thisType;
        }

        public bool checkMatch(IToken token, int leftTokenLangType, int rightTokenLangType)
        {
            if (leftTokenLangType != rightTokenLangType)
            {
                semanticError(new TypeConflictError(token, leftTokenLangType, rightTokenLangType));
                return false;
            }
            return true;
        }
    }
}