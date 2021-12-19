using System.Numerics;

namespace Lab3
{
    delegate Vector2 FdblVector2(double x, double y);

    struct DataItem
    {
        public double x { get; set; }
        public double y { get; set; }
        public Vector2 value { get; set; }
        public DataItem(double x, double y, Vector2 value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }
        public string ToLongString(string format)
        {
            return $"В точке ({x.ToString(format)}, {y.ToString(format)}) модуль переменной равен: {value.Length().ToString(format)}, " +
                $"компонента по X равна: {value.X.ToString(format)}, " +
                $"компонента по Y равна: {value.Y.ToString(format)}";
        }
        public override string ToString()
        {
            return $"В точке ({x}, {y}) значение равно: {value.Length()}";
        }
    }
}