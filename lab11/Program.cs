using System;
using System.Diagnostics;
using System.Collections;
using Musical_Instrument;
using System.Reflection;
using System.Buffers;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace lab11
{
    public class Program
    {
        /// <summary>
        /// Основная функция
        /// </summary>
        static void Main()
        {
            FirstPart(); // Вызов первой части лабораторной
            SecondPart(); // Вызов второй части лабораторной
            ThirdPart(); // Вызов третьей части лабораторной
        }

        /// <summary>
        /// Вывод объектов очереди в консоль
        /// </summary>
        /// <param name="queue">Очередь</param>
        static void showQueue(Queue queue)
        {
            if (queue.Count != 0)
            {
                int index = 1;
                foreach (var item in queue)
                {
                    Console.Write($"{index}. ");
                    Console.WriteLine(item);
                    index++;
                }
            }
            else
                Console.WriteLine("Очередь пустая.");
        }

        /// <summary>
        /// Получить пианино с максимальным количеством клавиш
        /// </summary>
        /// <param name="queue">Очередь</param>
        /// <returns>пианино с максимальным количеством клавиш</returns>
        static Piano GetPianoWithMaxButtons(Queue queue)
        {
            Piano resPiano = new();
            foreach (var item in queue)
            {
                Piano? piano = item as Piano;
                if (piano != null)
                {
                    if (piano.CountButtons >= resPiano.CountButtons)
                    {
                        resPiano = piano;
                    }
                }
            }
            return resPiano;
        }

        /// <summary>
        /// Поиск всех гитар
        /// </summary>
        /// <param name="queue">Очередь</param>
        static MusicalInstrument[] FindAllGuitars(Queue queue)
        {
            int index = 0;
            foreach (var item in queue)
            {
                Guitar? guitar = item as Guitar;
                if (guitar != null)
                {
                    index++;
                }
            }
            MusicalInstrument[] mi = new MusicalInstrument[index];
            index = 0;
            foreach (var item in queue)
            {
                Guitar? guitar = item as Guitar;
                if (guitar != null)
                {
                    mi[index] = guitar;
                    index++;
                }
            }
            return mi;
        }

        /// <summary>
        /// Поиск отличников
        /// </summary>
        /// <param name="queue">Очередь для поиска</param>
        static Student[] searchExcellentStudents(Queue queue)
        {
            int index = 0;
            foreach (var item in queue)
            {
                Student? student = item as Student;
                if (student != null)
                {
                    if (student.Gpa > 6)
                        index++;
                }
            }
            Student[] mi = new Student[index];
            index = 0;
            foreach (var item in queue)
            {
                Student? student = item as Student;
                if (student != null)
                {
                    if (student.Gpa > 6)
                    {
                        mi[index] = student;
                        index++;
                    }
                }
            }
            return mi;
        }

        /// <summary>
        /// Сортировка очереди
        /// </summary>
        /// <param name="queue">Очередь для сортировки</param>
        /// <returns>Отсортированная очередь</returns>
        static Queue sortQueue(Queue queue)
        {
            List<MusicalInstrument> instrumentList = new List<MusicalInstrument>();
            foreach (var item in queue)
            {
                MusicalInstrument mi = (MusicalInstrument)item;
                instrumentList.Add((MusicalInstrument)mi.Clone());
            }
            instrumentList.Sort();
            Queue sortedQueue = new Queue();
            foreach (var item in instrumentList)
            {
                sortedQueue.Enqueue(item);
            }
            return sortedQueue;
        }

        /// <summary>
        /// Поиск интрумента в очереди
        /// </summary>
        /// <param name="queue">Очередь</param>
        /// <param name="instrument">Искомый инструмент</param>
        static void seachItem(Queue queue, MusicalInstrument instrument)
        {
            if (queue.Contains(instrument))
                Console.WriteLine($"Элемент найден");
            else
                Console.WriteLine($"Элемент не найден");
        }

        static Queue Clone(Queue queue)
        {
            Queue newQueue = new();
            foreach (MusicalInstrument item in queue)
            {
                newQueue.Enqueue(item.Clone());
            }
            return newQueue;
        }

        /// <summary>
        /// Первая часть лабораторной (работа с очередью)
        /// </summary>
        /// 
        static void FirstPart()
        {
            // Создание очереди и добавление элементов
            Console.WriteLine("Первая часть ЛР - работа с очередью");
            Console.WriteLine("В созданной очереди присутствуют следующие объекты классов: Piano, Guitar, Student.");
            Queue queue = new Queue();
            for (int i = 0; i < 3; i++)
            {
                Piano piano = new Piano();
                piano.RandomInit();
                queue.Enqueue(piano);

                Guitar guitar = new Guitar();
                guitar.RandomInit();
                queue.Enqueue(guitar);

                Student student = new Student();
                student.RandomInit();
                queue.Enqueue(student);
            }
            Console.WriteLine("Вывод элементов очереди:");
            showQueue(queue);

            // Удаление элементов
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Вывод элементов очереди после удаления 3х первых элементов:");
            showQueue(queue);

            // Запросы к очереди
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Поиск пианино с максимальным количеством клавиш:");
            GetPianoWithMaxButtons(queue).ShowVirtualMethod();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Поиск всех гитар в очереди:");
            int count = 1;
            foreach (var item in FindAllGuitars(queue))
            {
                Console.Write($"{count}. ");
                item.ShowVirtualMethod();
                count++;
            }
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Поиск всех отличников:");
            count = 1;
            foreach (var item in searchExcellentStudents(queue))
            {
                Console.Write($"{count}. ");
                Console.WriteLine(item.ToString());
                count++;
            }

            // Клонирование коллекции
            Queue cloneQueue = new Queue();
            for (int i = 0; i < 3; i++)
            {
                Piano piano = new Piano();
                piano.RandomInit();
                cloneQueue.Enqueue(piano);

                Guitar guitar = new Guitar();
                guitar.RandomInit();
                cloneQueue.Enqueue(guitar);
            }
            cloneQueue = Clone(cloneQueue);
            cloneQueue.Dequeue();
            cloneQueue.Dequeue();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Клонирование очереди и удаление 2х элементов в клоне.");
            Console.WriteLine("Вывод склонированной очереди:");
            showQueue(cloneQueue);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Вывод изначальной очереди:");
            showQueue(queue);

            // Сортировка очереди O_O
            // Сама очередь не подразумевает сортировку, но я реализую ее
            queue.Clear();
            Piano pianoForSearch = new Piano();
            pianoForSearch.RandomInit();
            queue.Enqueue(pianoForSearch);

            Guitar guitarForSearch = new Guitar();
            guitarForSearch.RandomInit();
            queue.Enqueue(guitarForSearch);

            for (int i = 0; i < 5; i++)
            {
                Piano piano = new Piano();
                piano.RandomInit();
                queue.Enqueue(piano);

                Guitar guitar = new Guitar();
                guitar.RandomInit();
                queue.Enqueue(guitar);
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Сортировка очереди:");
            Console.WriteLine("Исходная очередь:");
            showQueue(queue);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Отсортированная очередь очереди:");
            queue = sortQueue(queue);
            showQueue(queue);

            // Поиск элементов в очереди
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Поиск элемента {pianoForSearch}:");
            seachItem(queue, pianoForSearch);

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Поиск элемента {guitarForSearch}:");
            seachItem(queue, guitarForSearch);

            Guitar unknown = new Guitar();
            unknown.RandomInit();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Поиск несуществующего элемента {unknown}:");
            seachItem(queue, unknown);
        }

        /// <summary>
        /// Вывод объектов стека в консоль
        /// </summary>
        /// <param name="stack">Стек</param>
        static void showStack(Stack<MusicalInstrument> stack)
        {
            if (stack.Count != 0)
            {
                int index = 1;
                foreach (var item in stack)
                {
                    Console.Write($"{index}. ");
                    Console.WriteLine(item);
                    index++;
                }
            }
            else
                Console.WriteLine("Стек пустой.");

        }

        /// <summary>
        /// Получить пианино с максимальным количеством клавиш
        /// </summary>
        /// <param name="stack">Стек</param>
        /// <returns>пианино с максимальным количеством клавиш</returns>
        static Piano GetPianoWithMaxButtons(Stack<MusicalInstrument> stack)
        {
            Piano resPiano = new();
            foreach (var item in stack)
            {
                Piano? piano = item as Piano;
                if (piano != null)
                {
                    if (piano.CountButtons >= resPiano.CountButtons)
                    {
                        resPiano = piano;
                    }
                }
            }
            return resPiano;
        }

        /// <summary>
        /// Поиск всех гитар
        /// </summary>
        /// <param name="stack">Стек</param>
        static MusicalInstrument[] FindAllGuitars(Stack<MusicalInstrument> stack)
        {
            int index = 0;
            foreach (var item in stack)
            {
                Guitar? guitar = item as Guitar;
                if (guitar != null)
                {
                    index++;
                }
            }
            MusicalInstrument[] mi = new MusicalInstrument[index];
            index = 0;
            foreach (var item in stack)
            {
                Guitar? guitar = item as Guitar;
                if (guitar != null)
                {
                    mi[index] = guitar;
                    index++;
                }
            }
            return mi;
        }

        /// <summary>
        /// Поиск всех электогитар с типом питания USB
        /// </summary>
        /// <param name="stack">Стек</param>
        /// <returns>все электогитары с типом питания USB</returns>
        static Stack<MusicalInstrument> GetAllGuitarsWithUSB(Stack<MusicalInstrument> stack)
        {
            Stack<MusicalInstrument> resStack = new Stack<MusicalInstrument>();
            foreach (var item in stack)
            {
                ElectricGuitar? guitar = item as ElectricGuitar;
                if (guitar != null)
                {
                    if (guitar.EnergySource == "USB")
                    {
                        resStack.Push(guitar);
                    }
                }
            }
            return resStack;
        }

        /// <summary>
        /// Сортировка стека
        /// </summary>
        /// <param name="stack">Стек для сортировки</param>
        /// <returns>Отсортированный стек</returns>
        static Stack<MusicalInstrument> sortStack(Stack<MusicalInstrument> stack)
        {
            List<MusicalInstrument> instrumentList = new List<MusicalInstrument>();
            foreach (var item in stack)
            {
                MusicalInstrument mi = (MusicalInstrument)item;
                instrumentList.Add((MusicalInstrument)mi.Clone());
            }
            instrumentList.Sort();
            instrumentList.Reverse();
            Stack<MusicalInstrument> sortedStack = new Stack<MusicalInstrument>();
            foreach (var item in instrumentList)
            {
                sortedStack.Push(item);
            }
            return sortedStack;
        }

        /// <summary>
        /// Поиск интрумента в стеке
        /// </summary>
        /// <param name="stack">стек</param>
        /// <param name="instrument">Искомый инструмент</param>
        static void searchItem(Stack<MusicalInstrument> stack, MusicalInstrument instrument)
        {
            if (stack.Contains(instrument))
                Console.WriteLine($"Элемент найден");
            else
                Console.WriteLine($"Элемент не найден");
        }

        static Stack<MusicalInstrument> Clone(Stack<MusicalInstrument> stack)
        {
            List<MusicalInstrument> lst = new();
            foreach (MusicalInstrument item in stack)
            {
                lst.Add((MusicalInstrument)item.Clone());
            }
            Stack<MusicalInstrument> newStack = new Stack<MusicalInstrument>();
            foreach (var item in lst)
                newStack.Push(item);
            return newStack;
        }

        /// <summary>
        /// Вторая часть лабораторной работы (работа со стеком)
        /// </summary>
        static void SecondPart()
        {
            // Создание стека и добавление элементов
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Вторая часть ЛР - работа со стеком");
            Console.WriteLine("В созданном стеке присутствуют следующие объекты классов: Piano, Guitar, ElectricGuitar.");
            Stack<MusicalInstrument> stack = new Stack<MusicalInstrument>();
            for (int i = 0; i < 4; i++)
            {
                Piano piano = new Piano();
                piano.RandomInit();
                stack.Push(piano);

                Guitar guitar = new Guitar();
                guitar.RandomInit();
                stack.Push(guitar);

                ElectricGuitar eguitar = new ElectricGuitar();
                eguitar.RandomInit();
                stack.Push(eguitar);
            }
            Console.WriteLine("Вывод элементов стека:");
            showStack(stack);

            // Удаление элементов
            stack.Pop();
            stack.Pop();
            stack.Pop();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Вывод элементов стека после удаления 3х первых элементов:");
            showStack(stack);

            // Запросы к стеку
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Поиск пианино с максимальным количеством клавиш:");
            GetPianoWithMaxButtons(stack).ShowVirtualMethod();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Поиск всех гитар в очереди:");
            int count = 1;
            foreach (var item in FindAllGuitars(stack))
            {
                Console.Write($"{count}. ");
                item.ShowVirtualMethod();
                count++;
            }
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Поиск всех гитар с типом питания UBS:");
            Stack<MusicalInstrument> res = GetAllGuitarsWithUSB(stack);
            showStack(res);


            // Клонирование коллекции
            Stack<MusicalInstrument> cloneStack = new Stack<MusicalInstrument>();
            cloneStack = Clone(stack);
            cloneStack.Pop();
            cloneStack.Pop();
            cloneStack.Peek().Name = "Test";
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Клонирование стека и удаление 2х элементов в клоне.");
            Console.WriteLine("Вывод склонированного стека:");
            showStack(cloneStack);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Вывод изначального стека:");
            showStack(stack);

            // Сортировка стека O_O
            // Сам стек не подразумевает сортировку, но я реализую ее
            Piano pianoForSearch = new Piano();
            pianoForSearch.RandomInit();
            stack.Push(pianoForSearch);

            Guitar guitarForSearch = new Guitar();
            guitarForSearch.RandomInit();
            stack.Push(guitarForSearch);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Сортировка стека:");
            Console.WriteLine("Исходный стек:");
            showStack(stack);
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("отсортированный стек:");
            stack = sortStack(stack);
            showStack(stack);

            // Поиск элементов в стеке
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Поиск элемента {guitarForSearch}:");
            searchItem(stack, guitarForSearch);

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Поиск элемента {pianoForSearch}:");
            searchItem(stack, pianoForSearch);

            Guitar unknown = new Guitar();
            unknown.RandomInit();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Поиск несуществующего элемента {unknown}:");
            searchItem(stack, unknown);
        }

        /// <summary>
        /// Печать времени
        /// </summary>
        /// <param name="key">Показатель найденного элемента</param>
        /// <param name="dataType">Тип данных</param>
        /// <param name="sw">stopwatch</param>
        static void printTime(bool[] key, string dataType, double time)
        {
            bool value = true;
            Console.WriteLine(dataType.PadRight(57) + $"{key.Count(c => c == value)}/{key.Length}".PadRight(19) + time);
        }

        /// <summary>
        /// Поиск гитары в коллекции
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="guitar">Искомая гитара</param>
        static void searchItemInCollection(TestCollections collection, Guitar guitar)
        {
            const int count = 1000;
            Stopwatch sw = Stopwatch.StartNew();
            bool[] key1 = new bool[count];
            bool[] key2 = new bool[count];
            bool[] key3 = new bool[count];
            bool[] key4 = new bool[count];
            bool[] key5 = new bool[count];
            bool[] key6 = new bool[count];
            long[] time1 = new long[count];
            long[] time2 = new long[count];
            long[] time3 = new long[count];
            long[] time4 = new long[count];
            long[] time5 = new long[count];
            long[] time6 = new long[count];


            for (int i = 0; i < count; i++)
            {
                sw.Restart();
                key1[i] = collection.firstQueue.Contains(guitar);
                sw.Stop();
                time1[i] = sw.ElapsedTicks;

                String str = guitar.ToString();
                sw.Restart();
                key2[i] = collection.secondQueue.Contains(str);
                sw.Stop();
                time2[i] = sw.ElapsedTicks;

                sw.Restart();
                key3[i] = collection.firstDict.ContainsValue(guitar);
                sw.Stop();
                time3[i] = sw.ElapsedTicks;

                sw.Restart();
                key4[i] = collection.secondDict.ContainsValue(guitar);
                sw.Stop();
                time4[i] = sw.ElapsedTicks;

                MusicalInstrument mi = guitar.GetBase;
                sw.Restart();
                key5[i] = collection.firstDict.ContainsKey(mi);
                sw.Stop();
                time5[i] = sw.ElapsedTicks;

                sw.Restart();
                key6[i] = collection.secondDict.ContainsKey(str);
                sw.Stop();
                time6[i] = sw.ElapsedTicks;
            }

            printTime(key1, "Queue1<Gutiar>", time1.Average());
            printTime(key2, "Queue2<string>", time2.Average());
            printTime(key3, "Dictionary1<MusicalInstrument, Guitar> by value", time3.Average());
            printTime(key4, "Dictionary2<string, Guitar> by value", time4.Average());
            printTime(key5, "Dictionary1<MusicalInstrument, Guitar> by key", time5.Average());
            printTime(key6, "Dictionary2<string, Guitar> by key", time6.Average());
        }

        /// <summary>
        /// Третья часть ЛР
        /// </summary>
        static void ThirdPart()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Третья часть ЛР - поиск в разных коллекциях".PadRight(53) + "Кол-во итераций");
            // Создание тестовой коллекции
            TestCollections collection = new(1000);

            MusicalInstrument start = collection.firstQueue.Peek();
            Queue q = new Queue(collection.firstQueue);
            for (int i = 0; i < q.Count / 2; i++)
                q.Dequeue();
            MusicalInstrument middle = collection.firstQueue.Peek();
            for (int i = 0; i < q.Count - 1; i++)
                q.Dequeue();
            MusicalInstrument end = collection.firstQueue.Peek();

            Guitar randomGuitar = new Guitar();
            randomGuitar.RandomInit();
            while (collection.firstQueue.Contains(randomGuitar))
                randomGuitar.RandomInit();

            Console.WriteLine("Тип данных:".PadRight(50) + "с найденным элементом".PadRight(25) + "Время");
            // Поиск элемента в разных коллекциях
            Console.WriteLine("Поиск элементов в начале ");
            searchItemInCollection(collection, (Guitar)start);

            Console.WriteLine("---------");
            Console.WriteLine("Поиск элементов в середине ");
            searchItemInCollection(collection, (Guitar)middle);

            Console.WriteLine("---------");
            Console.WriteLine("Поиск элементов в конце ");
            searchItemInCollection(collection, (Guitar)end);

            Console.WriteLine("---------");
            Console.WriteLine("Поиск несуществующего элемента");
            searchItemInCollection(collection, randomGuitar);
        }
    }
}