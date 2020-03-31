using AllanMilne.Ardkit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CompilersAssesment.PALCompiler
{
    public class PALScemantics : Semantics
    {
        public PALScemantics(IParser p) : base(p)
        {
        }

        /// <summary>
        ///adds the token to the semantic classes list of symbols
        /// returns Void
        /// </summary>
        /// <param name="tokenToAddtoSymbolsTree"></param>
        public void DeclareId(IToken tokenToAddtoSymbolsTree, int languageType)
        {
            if (!tokenToAddtoSymbolsTree.Is(Token.IdentifierToken)) return;  // only for identifier tokens.
            Scope symbols = Scope.CurrentScope;
            if (symbols.IsDefined(tokenToAddtoSymbolsTree.TokenValue))
            {
                semanticError(new AlreadyDeclaredError(tokenToAddtoSymbolsTree, symbols.Get(tokenToAddtoSymbolsTree.TokenValue)));
            }
            else
            {
                symbols.Add(new VarSymbol(tokenToAddtoSymbolsTree, languageType));//no need to set currentType - this replaced
            }
        }

        /// <summary>
        ///checks weather token is defined in Scope
        ///then returns its language type or creates a NotDeclaredError
        /// </summary>
        /// <param name="tokenToCheck"></param>
        /// <returns>language type for the token</returns>
        public int CheckId(IToken tokenToCheck)
        {
            //if its not an identifier do the type check but don't check if declared
            if (!tokenToCheck.Is(Token.IdentifierToken))
                return (CheckType(tokenToCheck));
            else if (!Scope.CurrentScope.IsDefined(tokenToCheck.TokenValue))
            {
                semanticError(new NotDeclaredError(tokenToCheck));
                return LanguageType.Undefined;
            }
            else return (CheckType(tokenToCheck));
        }

        /// <summary>
        /// Check type compatibility of current token.
        /// Side-effect: sets current type if it is undefined at present.
        /// Must only be called for identifiers that are declared.
        /// gs: takes in token and returns tokenLanguage type (as integer)
        /// </summary>
        /// <param name="token">The token of which to get the language type for</param>
        /// <returns>Language Type for the token</returns>
        private int CheckType(IToken token)
        {
            int thisLanguageType = LanguageType.Undefined;
            if (token.Is(Token.IdentifierToken))
            {
                thisLanguageType = Scope.CurrentScope.Get(token.TokenValue).Type;
                return thisLanguageType;
            }
            else if (token.Is(Token.IntegerToken))
            {
                thisLanguageType = LanguageType.Integer;
                return thisLanguageType;
            }
            else if (token.Is(Token.RealToken))
            {
                thisLanguageType = LanguageType.Real;
                return thisLanguageType;
            }

            return thisLanguageType;
        }

        /// <summary>
        ///checks weather or not the TokenLanguage types match.
        ///used to give more helpful semantic error advice
        /// </summary>
        /// <param name="token">the current token</param>
        /// <param name="leftTokenLangType">The left side of the expression (Language Type) Found</param>
        /// <param name="rightTokenLangType">The right side of the expression (Language Type) Expected</param>
        /// <returns>true or false with a TypeConflictError</returns>
        public bool CheckMatch(IToken token, int leftTokenLangType, int rightTokenLangType)
        {
            //don't bother checking if its undefined. that error has already been logged
            //and the check doesn't make much sense with undefined
            if (leftTokenLangType == LanguageType.Undefined)
                return false;
            if (leftTokenLangType != rightTokenLangType)
            {
                semanticError(new TypeConflictError(token, rightTokenLangType, leftTokenLangType));
                return false;
            }
            return true;
        }
    }
}