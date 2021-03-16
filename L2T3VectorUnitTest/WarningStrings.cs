namespace L2T3VectorUnitTest
{
    internal class WarningStrings
    {
        internal const string VectorWithCoordinatesCountErrorMessage = "Ошибка в конструкторе Vector({0}):  заданное количество координат <= 0.";

        internal const string VectorWithCoordinatesCountAndArrayErrorMessage = "Ошибка в конструкторе Vector({0}, array):  заданное количество координат <= 0.";
        internal const string VectorWithCoordinatesCountAndNullArrayErrorMessage = "Ошибка в конструкторе Vector({0}, null):   массив - null.";
        internal const string VectorWithCoordinatesCountAnd0ArrayErrorMessage = "Ошибка в конструкторе Vector({0}, double[0] array):   массив длины ноль.";

        internal const string VectorWithNullArrayErrorMessage = "Ошибка в конструкторе Vector(null):  массив - null.";
        internal const string VectorWith0ArrayErrorMessage = "Ошибка в конструкторе Vector(double[0] array):  массив длины ноль.";

        internal const string GetCoordinateRangeErrorMessage = "Ошибка в GetCoordinate({0}): индекс вне диапазона [0, {1}].";
        internal const string SetCoordinateRangeErrorMessage = "Ошибка в SetCoordinate({0}, value): индекс вне диапазона [0, {1}].";
        
    }
}
