using System;
using System.Collections.Generic;
using System.Linq;

namespace First
{
    public class SumPairSlow
    {
        private readonly List<int> _numbers;
        private readonly int _x; 
        private readonly HashSet<Pair> _result = new HashSet<Pair>(new PairCompaper());
        
        public SumPairSlow(IEnumerable<int> numbers, int x)
        {
            if (numbers == null)
                throw new ArgumentException("Не задан массив чисел");
            _numbers = new List<int>(numbers);
            _x = x;
            
            Process();
        }
        
        private void Process()
        {
            // двойной цикл, перебираем все варианты
            for (int i = 0; i < _numbers.Count; i++)
            {
                for (int j = 1; j < _numbers.Count; j++)
                {
                    if (i != j)
                    {
                        var elem1 = _numbers[i];
                        var elem2 = _numbers[j];
                        if (elem1 + elem2 == _x)
                        {
                            AddToResult(elem1, elem2);  
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return string.Join("\n", _result.Select(t => "x = " + t.x + ", y = " + t.y));
        }

        private void AddToResult(int elem1,int  elem2)
        {
            var pair = new Pair(elem1, elem2);
            if (!_result.Contains(pair))
            {
                _result.Add(pair);
            }
        }

        private struct Pair
        {
            public Pair(int x, int y)
            {
                this.x = x; 
                this.y = y;
            }

            public int x;
            public int y;
        }

        private class PairCompaper : IEqualityComparer<Pair>
        {
            public bool Equals(Pair x, Pair y)
            {
                return (x.x == y.x && x.y == y.y) || (x.x == y.y && x.y == y.x); 
            }

            public int GetHashCode(Pair obj)
            {
                unchecked 
                {
                    return obj.x + obj.y;
                }
            }
        }
    }
}
