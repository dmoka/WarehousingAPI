using System.Collections.Generic;

namespace KaliGasService.Core.Types
{

    public class BiDictionary<T1, T2> : List<(T1, T2)>
    {
        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        public BiDictionary()
        {
            this.Forward = new Indexer<T1, T2>(_forward);
            this.Reverse = new Indexer<T2, T1>(_reverse);
        }

        public class Indexer<T3, T4>
        {
            private Dictionary<T3, T4> _dictionary;

            public Indexer(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }

            public T4 this[T3 index]
            {
                get => _dictionary[index];
                set => _dictionary[index] = value;
            }
        }

        //For using inline initialization
        public new void Add((T1, T2) param)
        {
            Add(param.Item1, param.Item2);
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public IEnumerable<T1> GetKeys()
        {
            var keyCollection = new Dictionary<T1, T2>.KeyCollection(_forward);

            return new List<T1>(keyCollection);
        }

        public IEnumerable<T2> GetValues()
        {
            var valueCollection = new Dictionary<T2, T1>.KeyCollection(_reverse);

            return new List<T2>(valueCollection);
        }

        public Indexer<T1, T2> Forward { get; private set; }
        public new Indexer<T2, T1>  Reverse { get; private set; }
    }

}
