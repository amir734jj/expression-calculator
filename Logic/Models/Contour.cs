using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces;

namespace Logic.Models
{
    public class Contour<T> : IContour<T>, ICombinable<Contour<T>>
    {
        private readonly Dictionary<string, T> _table;

        private Contour<T> _parent;

        public Contour(): this(new Dictionary<string, T>())
        {
            
        }
        
        public Contour(string key, T immediate)
        {
            _table = new Dictionary<string, T> {{key, immediate}};
        }
        
        public Contour(Dictionary<string, T> immediate)
        {
            _table = immediate;
        }
        
        public bool Lookup(string key, out T result)
        {
            if (_table.ContainsKey(key))
            {
                result = _table[key];
                return true;
            }

            if (_parent != null)
            {
                return _parent.Lookup(key, out result);
            }

            result = default;
            
            return false;
        }
        
        public Contour<T> Append(string key, T value)
        {
            _table[key] = value;

            return this;
        }

        public Contour<T> Pop()
        {
            return _parent;
        }
        
        public Contour<T> Push()
        {
            return new Contour<T>
            {
                _parent = this
            };
        }

        public Contour<T> Combine(Contour<T> other)
        {
            return new Contour<T>(_table.Except(other._table).Concat(other._table).ToDictionary(x => x.Key, x => x.Value))
            {
                _parent = _parent?.Combine(other._parent)
            };
        }
    }
}