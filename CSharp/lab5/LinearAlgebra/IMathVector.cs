using System.Collections;
namespace LinearAlgebra;

public interface IMathVector : IEnumerable
{
    // Получить размерность вектора (количество координат).
    int Dimensions { get; }

    // Индексатор для доступа к элементам вектора. Нумерация с нуля.
    double this[int i] { get; set; }

    // Рассчитать длину (модуль) вектора.
    double Length { get; }

    // Сравнение двух векторов (доп. метод) !!
    bool AreEqual(IMathVector otherVector);

    // Покомпонентное сложение с числом.
    IMathVector SumNumber(double number);

    // Покомпонентное умножение на число.
    IMathVector MultiplyNumber(double number);

    // Сложение с другим вектором.
    IMathVector Sum(IMathVector vector);

    // Покомпонентное умножение с другим вектором.
    IMathVector Multiply(IMathVector vector);

    // Скалярное умножение на другой вектор.
    double ScalarMultiply(IMathVector vector);

    // Вычислить Евклидово расстояние до другого вектора.
    double CalcDistance(IMathVector vector);
}

