using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            Dealership dealership = new Dealership();

            Console.WriteLine("Введите ваш баланс");
            int money = Convert.ToInt32(Console.ReadLine());

            ClearConsole();

            while(isWorking)
            {
                Console.WriteLine("Ваш баланс - " + money);

                Console.WriteLine($"Для начала работы введите {CommandNewClient}, для выхода - {Exit}");
                string input = Console.ReadLine();

                if(input == Exit)
                {
                    Console.WriteLine("Программа завершена");

                    isWorking = false;
                }
                else if(input == CommandNewClient)
                {
                    money = dealership.NewClient(money);
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

    class Dealership
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

        private List<Detail> _details = new List<Detail>() {new Detail(1000, Flywheel, 500, ProblemWithFlywheel), new Detail(2500, Bearing, 1000, ProblemWithBearing), new Detail(10000, Piston, 5000, ProblemWithPiston), new Detail(5000, Relay, 2500, ProblemWithRelay), new Detail(100000, Transmission, 30000, ProblemWithTransmission), new Detail(200000, Engine, 50000, ProblemWithEngine) };
        private List<string> _detailsInDealership = new List<string>() { Flywheel, Bearing, Piston, Relay, Transmission, Engine };

        public int NewClient(int money)
        {
            Random random = new Random();
            int indexProblem = random.Next(_details.Count);
            bool canYouRepair = false;

            Console.WriteLine($"К вам пришёл новый клиент, у него проблема с {_details[indexProblem].Name} - {_details[indexProblem].Breakdowns}\nOн готов заплатить {_details[indexProblem].Price}, за отказ от работы вы получите штраф {_details[indexProblem].PaymentForRepairs}");

            for(int i = 0; i < _detailsInDealership.Count; i++)
            {
                if (_detailsInDealership[i] == _details[indexProblem].Name)
                {
                    canYouRepair = true;
                    _detailsInDealership.RemoveAt(i);

                    Console.WriteLine("У вас на складе есть эта деталь, вы починили машину клинта и получили " + _details[indexProblem].Price);

                    money += _details[i].Price;

                    break;
                }
            }

            if(canYouRepair == false)
            {
                Console.WriteLine("У вас на складе нет этой детали, вы не починили машину клинта и получили штраф " + _details[indexProblem].PaymentForRepairs);

                money -= _details[indexProblem].PaymentForRepairs;
            }

            return money;
        }
    }

    class Detail
    {
        public int Price { get; private set; }
        public string Name { get; private set; }
        public int PaymentForRepairs { get; private set; }
        public string Breakdowns { get; private set; }

        public Detail(int price, string name, int paymentForRepairs, string breakdowns) 
        {
            Price = price;
            Name = name;
            PaymentForRepairs = paymentForRepairs;
            Breakdowns = breakdowns;
        }
    }
}
