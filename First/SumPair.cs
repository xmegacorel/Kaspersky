using System;
using System.Collections.Generic;
using System.Linq;

namespace First
{
    /*
     * Решение таково: берем сортируем массив, и бинарным поиском отсекаем те элементы, которые сами больше, чем число X
     * Далее навстречу друг другу идем с индексаторами и проверяем сумму чисел
     */
    public class SumPair
    {
        private readonly List<int> _numbers;
        private readonly int _x;
        private readonly List<Pair> _result = new List<Pair>();

        public SumPair(IEnumerable<int> numbers, int x)
        {
            if (numbers == null)
                throw new ArgumentException("Не задан массив чисел");

            _numbers = new List<int>(numbers);
            _x = x;

            Process();
        }

        private void Process()
        {
            _numbers.Sort();
            
            /*
             * есть еще одна оптимизация, можно попробовать ограничить  j значение, которое в массиве указывает на элемент, который явно больше чем X
             * судя по тестам на 1e5 элементов, прибавка совсем небольшая
             */

            int i = 0, j = _numbers.Count - 1;
            while (i != j)
            {
                var elem1 = _numbers[i];
                var elem2 = _numbers[j];
                var sum = elem1 + elem2;
                if (sum == _x)
                {
                    _result.Add(new Pair(elem1, elem2));
                    i++;
                    j--;
                }
                else if (sum > _x)
                    j--;
                else
                    i++;
            }
        }

        public override string ToString()
        {
            return string.Join("\n", _result.Select(t => "x = " + t.X + ", y = " + t.Y));
        }

        // в стиле java
        private struct Pair
        {
            public Pair(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public readonly int X;
            public readonly int Y;
        }    
    } 
}
