using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Generik<T> where T : class
    {
        private List<T> _elementi = new List<T>();
        private readonly object _lock = new object();

        public void Dodaj(T element)
        {
            lock (_lock)
            {
                _elementi.Add(element);
            }
        }

        public bool Odstrani (T element)
        {
            lock (_lock)
            {
                return _elementi.Remove(element);
            }
        }

        public T Najdi(Func<T, bool> pogoj)
        {
            lock(_lock)
            {
                return _elementi.FirstOrDefault(pogoj);
            }
        }

        public List<T> VsiElementi()
        {
            lock (_lock)
            {
                return new List<T>(_elementi);
            }
        }

        public int SteviloElementov
        {
            get
            {
                lock (_lock)
                {
                    return _elementi.Count;
                }
            }
        }
    }
}
