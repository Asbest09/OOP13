using System;
using System.Collections.Generic;
using System.Linq;
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
                dealership.ShowBalance();
                if (dealership.Balance < 0)
                    isWorking = false;

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
                        dealership.ServiceClient();
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

        public List<Cell> DetailsInDealership { get; private set; }
        public List<Cell> ExamplesDetails { get; private set; }

        public Storage()
        {
            DetailsInDealership = new List<Cell>() { new Cell (2, new Detail(1000, Flywheel, 500, ProblemWithFlywheel)), new Cell (2, new Detail(2500, Bearing, 1000, ProblemWithBearing)), new Cell(2, new Detail(10000, Piston, 5000, ProblemWithPiston)), new Cell (2, new Detail(5000, Relay, 2500, ProblemWithRelay)), new Cell(2, new Detail(100000, Transmission, 30000, ProblemWithTransmission)), new Cell (2, new Detail(200000, Engine, 50000, ProblemWithEngine)) };
            ExamplesDetails = DetailsInDealership;
        }

        public void ShowDetails()
        {
            for (int i = 0; i < ExamplesDetails.Count; i++)
            {
                Console.Write("\n" + (i + 1) + ". " + ExamplesDetails[i].Detail.Name + " - ");

                for(int j = 0; j < DetailsInDealership.Count; j++)
                {
                    if (ExamplesDetails[j].Detail.Name == DetailsInDealership[j].Detail.Name)
                        Console.Write(DetailsInDealership[j].CountDetails);

                    break;
                }
            }

            Console.WriteLine();
        }
    }

    class Cell
    {
        public Detail Detail { get; private set; }
        public int CountDetails { get; private set; }

        public Cell(int count, Detail detail)
        {
            CountDetails = count;
            Detail = detail;
        }

        public bool CheckAvailabilityDetail() =>
            CountDetails != 0;

        public void ExpenditureDetail()
        {
            CountDetails--;
        }
    }

    class Dealership
    {
        private const string CommandRefusal = "no";

        private Storage _storage = new Storage();
        public int Balance { get; private set; }

        public Dealership(int money)
        {
            Balance = money;
        }

        public void ShowBalance()
        {
            if(Balance < 0)
                Console.WriteLine("Вы обанкротились");
            else
                Console.WriteLine("Ваш баланс - " + Balance); 
        }

        public void ServiceClient()
        {
            Random random = new Random();
            int indexProblem = random.Next(_storage.DetailsInDealership.Count);

            Console.Write($"К вам пришёл новый клиент, у него проблема - {_storage.ExamplesDetails[indexProblem].Detail.Breakdowns}\nOн готов заплатить {_storage.ExamplesDetails[indexProblem].Detail.PaymentForRepairs}, за отказ от работы вы получите штраф {_storage.ExamplesDetails[indexProblem].Detail.Fine}");
            _storage.ShowDetails();
            Console.WriteLine("Введите индекс поломки или " + CommandRefusal + " для отказа от работы");
            string input = Console.ReadLine();

            if(input != CommandRefusal && int.TryParse(input, out int indexInput) && indexInput > 0 && indexInput <= _storage.DetailsInDealership.Count)
            {
                indexInput--;

                if (_storage.ExamplesDetails[indexProblem].Detail.Name == _storage.DetailsInDealership[indexInput].Detail.Name && _storage.DetailsInDealership[indexInput].CheckAvailabilityDetail())
                {
                    Console.WriteLine("Вы починили машину клинта и получили " + _storage.ExamplesDetails[indexProblem].Detail.PaymentForRepairs);

                    Balance += _storage.ExamplesDetails[indexProblem].Detail.PaymentForRepairs;

                    _storage.DetailsInDealership[indexInput].ExpenditureDetail();
                }
                else
                {
                    Console.WriteLine("Вы не правильно выбрали деталь или у вас её нет, вы не починили машину клинта и получили штраф " + _storage.ExamplesDetails[indexProblem].Detail.Fine);

                    Balance -= _storage.ExamplesDetails[indexProblem].Detail.Fine;
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
