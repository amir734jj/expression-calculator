using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces;
using Logic.Tokens;

namespace Logic.Abstracts
{
    public abstract class Visitor<T> : IVisitor<T> where T: ICombinable<T>, new()
    {
        public virtual T Visit(IEnumerable<IToken> tokens, T state = default)
        {
            return tokens.Aggregate(state ?? new T(), (x, y) => Visit(y, x));
        }
        
        public virtual T Visit(AddToken token, T state = default)
        {
            return Visit(token.Item1, state ?? new T()).Combine(Visit(token.Item2, state ?? new T()));
        }
        
        public virtual T Visit(SubtractToken token, T state = default)
        {
            return Visit(token.Item1, state ?? new T()).Combine(Visit(token.Item2, state ?? new T()));
        }
        
        public virtual T Visit(MultiplyToken token, T state = default)
        {
            return Visit(token.Item1, state ?? new T()).Combine(Visit(token.Item2, state ?? new T()));
        }
        
        public virtual T Visit(DivideToken token, T state = default)
        {
            return Visit(token.Item1, state ?? new T()).Combine(Visit(token.Item2, state ?? new T()));
        }
        
        public virtual T Visit(NegateToken token, T state = default)
        {
            return Visit(token.Item1, state ?? new T());
        }
        
        public virtual T Visit(FactorialToken token, T state = default)
        {
            return Visit(token.Item1, state ?? new T());
        }
        public virtual T Visit(PowerToken token, T state = default)
        {
            return Visit(token.Item1, state ?? new T()).Combine(Visit(token.Item2, state ?? new T()));
        }
        
        public virtual T Visit(AssignmentToken token, T state = default)
        {
            return Visit(token.Item2, state ?? new T());
        }
        
        public virtual T Visit(NumberToken token, T state = default)
        {
            return state ?? new T();
        }
        
        public virtual T Visit(VariableToken token, T state = default)
        {
            return state ?? new T();
        }

        protected T Visit(IToken token, T state = default)
        {
            return token switch
            {
                AddToken addToken => Visit(addToken, state),
                SubtractToken subtractToken => Visit(subtractToken, state),
                MultiplyToken multiplyToken => Visit(multiplyToken, state),
                DivideToken divideToken => Visit(divideToken, state),
                NegateToken negateToken => Visit(negateToken, state),
                NumberToken numberToken => Visit(numberToken, state),
                FactorialToken factorialToken => Visit(factorialToken, state),
                PowerToken powerToken => Visit(powerToken, state),
                VariableToken variableToken => Visit(variableToken, state),
                AssignmentToken assignmentToken => Visit(assignmentToken, state),
                _ => throw new ArgumentOutOfRangeException(nameof(token))
            };
        }
    }
}