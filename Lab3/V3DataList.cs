using System;
using System.IO;
using System.Numerics;
using System.Globalization;
using System.Collections.Generic;

namespace Lab3
{
    class V3DataList : V3Data
    {
        public List<DataItem> DataList { get; private set; }
        public V3DataList(string id, DateTime time) : base(id, time)
        {
            DataList = new List<DataItem>(100);
        }
        public bool Add(DataItem newItem)
        {
            foreach (DataItem el in DataList)
            {
                if ((el.x == newItem.x) && (el.y == newItem.y))
                    return false;
            }
            DataList.Add(newItem);
            return true;
        }
        public int AddDefaults(int nItems, FdblVector2 F)
        {
            int result = 0;
            double tmp_x, tmp_y;
            //Vector2 tmp_value = new Vector2(); ??
            Vector2 tmp_value;
            DataItem tmp_item;

            for (int i = 0; i < nItems; i++)
            {
                tmp_x = Count * Math.Cos(2.0 * Math.PI * i / nItems);
                tmp_y = Count * Math.Sin(2.0 * Math.PI * i / nItems);
                tmp_value = F(Convert.ToSingle(tmp_x), Convert.ToSingle(tmp_y));
                tmp_item = new DataItem(tmp_x, tmp_y, tmp_value);
                result += Convert.ToInt32(Add(tmp_item));
            }
            return result;
        }
        public override int Count
        {
            get { return DataList.Count; }
        }
        public override double MaxDistance
        {
            // O(n^2) :(
            get
            {
                if (Count < 2) return -1.0;
                Vector2 tmpVec2 = new Vector2(Convert.ToSingle(DataList[0].x - DataList[1].x), Convert.ToSingle(DataList[0].y - DataList[1].y));
                double curDistance, maxDistance = tmpVec2.Length();
                for (int i = 0; i < Count - 1; i++)
                {
                    for (int j = i + 1; j < Count; j++)
                    {
                        tmpVec2.X = Convert.ToSingle(DataList[i].x - DataList[j].x);
                        tmpVec2.Y = Convert.ToSingle(DataList[i].y - DataList[j].y);
                        curDistance = tmpVec2.Length();
                        if (curDistance > maxDistance)
                            maxDistance = curDistance;
                    }
                }
                return maxDistance;
            }
        }
        public override string ToString()
        {
            return $"V3DataList: ({base.ToString()})\n" +
                $"Number of elements: {Count}";
        }
        public override string ToLongString(string format)
        {
            string res = ToString() + "\n";
            foreach (DataItem el in DataList)
            {
                res += el.ToLongString(format) + "\n";
            }
            return res;
        }

        public override IEnumerator<DataItem> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return DataList[i];
            }
        }

        public static bool SaveAsText(string filename, V3DataList v3)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(fs);

                writer.WriteLine(v3.id);
                writer.WriteLine(v3.time.ToString());

                foreach (var elem in v3.DataList)
                {
                    writer.WriteLine(elem.x + " " + elem.y + " " + elem.value.X + " " + elem.value.Y);
                }
                writer.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return true;
        }

        public static bool LoadAsText(string filename, ref V3DataList v3)
        {
            CultureInfo CI = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            FileStream fs = null;

            try
            {
                fs = new FileStream(filename, FileMode.Open);
                StreamReader reader = new StreamReader(fs);

                v3.id = reader.ReadLine();
                v3.time = DateTime.Parse(reader.ReadLine(), CI);
                v3.DataList.Clear();

                string str = reader.ReadLine();
                string[] elem = null;

                while (str != null)
                {
                    elem = str.Split(' ');
                    v3.Add(new DataItem(float.Parse(elem[0], CI), float.Parse(elem[1], CI), new Vector2(float.Parse(elem[2], CI), float.Parse(elem[3], CI))));
                    str = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return true;
        }
    }
}