using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab3
{
    abstract class V3Data : IEnumerable<DataItem>
    {
        public string id { get; protected set; }
        public DateTime time { get; protected set; }
        public V3Data(string id, DateTime time)
        {
            this.id = id;
            this.time = time;
        }
        public abstract int Count { get; }
        public abstract double MaxDistance { get; }
        public abstract string ToLongString(string format);
        public override string ToString() { return $"id: {id}, time: {time}"; }

        public abstract IEnumerator<DataItem> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}