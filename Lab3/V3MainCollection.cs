using System;
using System.Linq;
using System.Collections.Generic;

namespace Lab3
{
    class V3MainCollection
    {
        private List<V3Data> mainList = new List<V3Data>();
        public int Count { get { return mainList.Count; } }
        public V3Data this[int index]
        {
            get { return mainList[index]; }
        }
        public bool Contains(string ID)
        {
            foreach (V3Data el in mainList)
            {
                if (el.id == ID)
                    return true;
            }
            return false;
        }
        public bool Add(V3Data v3Data)
        {
            if (!Contains(v3Data.id))
            {
                mainList.Add(v3Data);
                return true;
            }
            else
                return false;
        }
        public string ToLongString(string format)
        {
            string str = "";
            int i = 1;
            foreach (V3Data el in mainList)
            {
                str += $"Element {i++}: " + el.ToLongString(format) + "\n";
            }
            return str;
        }
        public override string ToString()
        {
            string str = "";
            int i = 1;
            foreach (V3Data el in mainList)
            {
                str += $"Element {i++}: " + el.ToString() + "\n";
            }
            return str;
        }

        public double Average
        {
            get
            {
                if (mainList.Count != 0)
                {
                    var DataItems = from data in mainList
                                    from item in data
                                    select item;

                    return DataItems.Average(item => Math.Sqrt(item.x * item.x + item.y * item.y));
                }
                else return double.NaN;
            }
        }

        public IEnumerable<float> DiaField
        {
            get
            {
                if (mainList.Count != 0)
                {
                    IEnumerable<float> list = from elem in mainList
                                              where elem.Count != 0
                                              select elem.Max(item => item.value.Length()) - elem.Min(item => item.value.Length());
                    return list;
                }
                else return null;

            }
        }

        public IEnumerable<IGrouping<double, DataItem>> GroupByX
        {
            get
            {
                if (mainList.Count != 0)
                {
                    var DataItems = from data in mainList
                                    from item in data
                                    select item;

                    return DataItems.GroupBy(item => item.x);
                }
                else return null;
            }
        }
    }
}