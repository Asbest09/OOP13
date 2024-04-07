using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace OOP13
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Exit = "exit";
            const string CommandNewClient = "new";

            bool isWorking = true;

            Console.WriteLine("Введите ваш баланс");
            Dealership dealership = new Dealership(Convert.ToInt32(Console.ReadLine()));

            ClearConsole();

            while(isWorking)
            {
                isWorking = dealership.ShowBalance();

                if(isWorking)
                {
                    Console.WriteLine($"Для начала работы введите {CommandNewClient}, для выхода - {Exit}");
                    string input = Console.ReadLine();

                    if (input == Exit)
                    {
                        Console.WriteLine("Программа завершена");

                        isWorking = false;
                    }
                    else if (input == CommandNewClient)
                    {
                        dealership.NewClient();
                    }
                }

                ClearConsole();
            }
        }

        private static void ClearConsole()
        {
            Console.WriteLine("Для продолжения нажмите ENTER");
            Console.ReadKey();
            Console.Clear();
        }
    }

    class Storage
    {
        private const string Flywheel = "Маховик";
        private const string Bearing = "Подшибник";
        private const string Piston = "Поршень";
        private const string Relay = "Реле";
        private const string Transmission = "Трансмисия";
        private const string Engine = "Двигатель";
        private const string ProblemWithFlywheel = "Проблема с маховиком";
        private const string ProblemWithBearing = "Надо смазать подшибник";
        private const string ProblemWithPiston = "На поршне трещина";
        private const string ProblemWithRelay = "Сгорело реле";
        private const string ProblemWithTransmission = "Трансмисия сломана";
        private const string ProblemWithEngine = "Двигатель не заводится";

        public List<Cell> Details { get; private set; }
        public List<string> AllDetails;

        public Storage()
        {
            Details = new List<Cell>() { new Cell (2, new Detail(1000, Flywheel, 500, ProblemWithFlywheel)), new Cell (2, new Detail(2500, Bearing, 1000, ProblemWithBearing)), new Cell(2, new Detail(10000, Piston, 5000, ProblemWithPiston)), new Cell (2, new Detail(5000, Relay, 2500, ProblemWithRelay)), new Cell(2, new Detail(100000, Transmission, 30000, ProblemWithTransmission)), new Cell (2, new Detail(200000, Engine, 50000, ProblemWithEngine)) };
            AllDetails = new List<string>() { Flywheel, Bearing, Piston, Relay, Transmission, Engine };
        }
        
        public void CheakCells()
        {
            foreach (var cell in Details)
            {
                if (cell.Count == 0)
                    Details.Remove(cell);
            }
        }
    }

    class Cell
    {
        public Detail Detail { get; private set; }
        public int Count { get; private set; }

        public Cell(int count, Detail detail)
        {
            Count = count;
            Detail = detail;
        }

        public void ExpenditureDetail()
        {
            Count--;
        }
    }

    class Dealership
    {
        private const string CommandRefusal = "no";

        Storage storage = new Storage();

        public Dealership(int money)
        {
            _balance = money;
        }

        private int _balance;

        public bool ShowBalance()
        {
            if(_balance <= 0)
            {
                Console.WriteLine("Вы обанкротились");

                return false;
            }
            else
            {
                Console.WriteLine("Ваш баланс - " + _balance);

                return true;
            }  
        }

        private void ShowDetails(List <string> detailsInDealership)
        {
            for(int i = 0; i < detailsInDealership.Count; i++)
                Console.WriteLine((i + 1) + ". " + detailsInDealership[i]);
        }

        public void NewClient()
        {
            Random random = new Random();
            int indexProblem = random.Next(storage.Details.Count);

            Console.WriteLine($"К вам пришёл новый клиент, у него проблема - {storage.Details[indexProblem].Detail.Breakdowns}\nOн готов заплатить {storage.Details[indexProblem].Detail.PaymentForRepairs}, за отказ от работы вы получите штраф {storage.Details[indexProblem].Detail.Fine}");
            ShowDetails(storage.AllDetails);
            Console.WriteLine("Введите индекс поломки или " + CommandRefusal + " для отказа от работы");
            string input = Console.ReadLine();

            if(input != CommandRefusal && int.TryParse(input, out int index) && index > 0 && index <= storage.Details.Count)
            {
                index--;

                if (storage.AllDetails[index] == storage.Details[indexProblem].Detail.Name)
                {
                    Console.WriteLine("Вы починили машину клинта и получили " + storage.Details[indexProblem].Detail.PaymentForRepairs);

                    _balance += storage.Details[index].Detail.PaymentForRepairs;

                    storage.Details[index].ExpenditureDetail();
                    storage.CheakCells();
                }
                else
                {
                    Console.WriteLine("Вы не правильно выбрали деталь или у вас её нет, вы не починили машину клинта и получили штраф " + storage.Details[indexProblem].Detail.Fine);

                    _balance -= storage.Details[indexProblem].Detail.Fine;
                }
            }
            else if(input == CommandRefusal)
            {
                Console.WriteLine("Вы отказались от работы");
            }
            else
            {
                Console.WriteLine("Некорректный ввод");
            }
        }
    }

    class Detail
    {
        public int PaymentForRepairs { get; private set; }
        public string Name { get; private set; }
        public int Fine { get; private set; }
        public string Breakdowns { get; private set; }

        public Detail(int price, string name, int fine, string breakdowns) 
        {
            PaymentForRepairs = price;
            Name = name;
            Fine = fine;
            Breakdowns = breakdowns;
        }
    }
}
