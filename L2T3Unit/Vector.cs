using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2T3Unit
{
    public class Vector
    {

        private double[] coordinates;

        public Vector(int coordinatesCount)
        {
            if (coordinatesCount <= 0)
            {
                throw new ArgumentException(string.Format(WarningStrings.VectorWithCoordinatesCountErrorMessage, coordinatesCount));
            }

            coordinates = new double[coordinatesCount];
        }

        public Vector(Vector vector)
        {
            coordinates = new double[vector.coordinates.Length];

            Array.Copy(vector.coordinates, coordinates, vector.coordinates.Length);
        }

        public Vector(double[] array)
        {
            if (array == null)
            {
                throw new NullReferenceException(string.Format(WarningStrings.VectorWithNullArrayErrorMessage));
            }
            if (array.Length == 0)
            {
                throw new Exception(string.Format(WarningStrings.VectorWith0ArrayErrorMessage));
            }

            coordinates = new double[array.Length];

            Array.Copy(array, coordinates, array.Length);
        }

        public Vector(int coordinatesCount, double[] array)
        {
            if (coordinatesCount <= 0)
            {
                throw new ArgumentException(string.Format(WarningStrings.VectorWithCoordinatesCountAndArrayErrorMessage, coordinatesCount));
            }
            if (array == null)
            {
                throw new NullReferenceException(string.Format(WarningStrings.VectorWithCoordinatesCountAndNullArrayErrorMessage, coordinatesCount));
            }
            if (array.Length == 0)
            {
                throw new Exception(string.Format(WarningStrings.VectorWithCoordinatesCountAnd0ArrayErrorMessage, coordinatesCount));
            }

            coordinates = new double[coordinatesCount];

            int elementsFromArrayCount = Math.Min(coordinatesCount, array.Length);

            Array.Copy(array, coordinates, elementsFromArrayCount);
        }

        public int GetSize()
        {
            return coordinates.Length;
        }

        public override string ToString()
        {
            StringBuilder vectorStringBuilder = new StringBuilder();
            vectorStringBuilder.Append("{");

            for (int i = 0; i < coordinates.Length - 1; i++)
            {
                vectorStringBuilder.Append(coordinates[i]);
                vectorStringBuilder.Append(", ");
            }

            vectorStringBuilder.Append(coordinates[coordinates.Length - 1]);
            vectorStringBuilder.Append("}");

            return vectorStringBuilder.ToString();
        }

        //4. Реализовать нестатические методы:
        //a.Прибавление к вектору другого вектора     

        public Vector AddVector(Vector vector)
        {
            return AddOrSubtractVector(vector, true);
        }

        //b.Вычитание из вектора другого вектора    

        public Vector SubtractVector(Vector vector)
        {
            return AddOrSubtractVector(vector, false);
        }

        private Vector AddOrSubtractVector(Vector vector, bool isAddition)
        {
            int operationMark = isAddition ? 1 : -1;

            if (coordinates.Length < vector.coordinates.Length)
            {
                Array.Resize(ref coordinates, vector.coordinates.Length);
            }

            for (int i = 0; i < vector.coordinates.Length; i++)
            {
                coordinates[i] += operationMark * vector.coordinates[i];
            }

            return this;
        }

        //c.Умножение вектора на скаляр        
        public Vector MultiplyBy(double number)
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] *= number;
            }

            return this;
        }

        //d.Разворот вектора (умножение всех компонент на -1)
        public Vector Reverse()
        {
            return MultiplyBy(-1);
        }

        //e.Получение длины вектора
        public double GetLength()
        {
            double tmpLength = 0;

            foreach (double element in coordinates)
            {
                tmpLength += element * element;
            }

            return Math.Sqrt(tmpLength);
        }

        //f.Получение и установка компоненты вектора по индексу
        public double GetCoordinate(int index)
        {
            if (index < 0 || index >= coordinates.Length)
            {
                throw new IndexOutOfRangeException(string.Format(WarningStrings.GetCoordinateRangeErrorMessage, index, coordinates.Length - 1));
            }

            return coordinates[index];
        }

        public void SetCoordinate(int index, double value)
        {
            if (index < 0 || index >= coordinates.Length)
            {
                throw new IndexOutOfRangeException(string.Format(WarningStrings.SetCoordinateRangeErrorMessage, index, coordinates.Length - 1));
            }

            coordinates[index] = value;
        }

        //g.Переопределить метод equals, чтобы был true  векторы имеют одинаковую размерность 
        // и соответствующие компоненты равны. 
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
            {
                return false;
            }

            Vector vector = (Vector)obj;

            if (coordinates.Length != vector.coordinates.Length)
            {
                return false;
            }

            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i] != vector.coordinates[i])
                {
                    return false;
                }
            }

            return true;
        }

        // Соответственно, переопределить hashCode
        public override int GetHashCode()
        {
            int prime = 37;
            int hash = 1;

            foreach (double element in coordinates)
            {
                hash = prime * hash + element.GetHashCode();
            }

            return hash;
        }

        //5. Реализовать статические методы – должны создаваться новые векторы:
        //a.Сложение двух векторов
        public static Vector GetAddition(Vector vector1, Vector vector2)
        {
            Vector vector = new Vector(vector1);
            return vector.AddVector(vector2);
        }

        //b.Вычитание векторов
        public static Vector GetDifference(Vector vector1, Vector vector2)
        {
            Vector vector = new Vector(vector1);
            return vector.SubtractVector(vector2);
        }

        //c.	Скалярное произведение векторов
        public static double GetScalarProduct(Vector vector1, Vector vector2)
        {
            int minVectorsLength = Math.Min(vector1.coordinates.Length, vector2.coordinates.Length);

            double scalarProduct = 0;

            for (int i = 0; i < minVectorsLength; i++)
            {
                scalarProduct += vector1.coordinates[i] * vector2.coordinates[i];
            }

            return scalarProduct;
        }

        //изменить длину массива координат вектора
        public Vector Resize(int newSize)
        {
            Array.Resize(ref coordinates, newSize);
            return this;
        }

        //копирование вектора
        public static void Copy(Vector sourceArray, Vector destinationArray, int copyLength)
        {
            if (destinationArray.GetSize() > copyLength)
            {
                Array.Resize(ref destinationArray.coordinates, copyLength);
            }

            Array.Copy(sourceArray.coordinates, 0, destinationArray.coordinates, 0, copyLength);
        }

    }
}
