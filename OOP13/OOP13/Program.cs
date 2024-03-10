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

    class Dealership
    {
        public Dealership(int money)
        {
            _money = money;
        }

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

        private int _money;
        private List<Detail> _details = new List<Detail>() {new Detail(1000, Flywheel, 500, ProblemWithFlywheel), new Detail(2500, Bearing, 1000, ProblemWithBearing), new Detail(10000, Piston, 5000, ProblemWithPiston), new Detail(5000, Relay, 2500, ProblemWithRelay), new Detail(100000, Transmission, 30000, ProblemWithTransmission), new Detail(200000, Engine, 50000, ProblemWithEngine) };
        private List<string> _detailsInDealership = new List<string>() { Flywheel, Bearing, Piston, Relay, Transmission, Engine };

        public bool ShowBalance()
        {
            if(_money <= 0)
            {
                Console.WriteLine("Вы обанкротились");

                return false;
            }
            else
            {
                Console.WriteLine("Ваш баланс - " + _money);

                return true;
            }  
        }

        public void NewClient()
        {
            Random random = new Random();
            int indexProblem = random.Next(_details.Count);
            bool dealershipCanRepair = false;

            Console.WriteLine($"К вам пришёл новый клиент, у него проблема с {_details[indexProblem].Name} - {_details[indexProblem].Breakdowns}\nOн готов заплатить {_details[indexProblem].PaymentForRepairs}, за отказ от работы вы получите штраф {_details[indexProblem].Fine}");

            for(int i = 0; i < _detailsInDealership.Count; i++)
            {
                if (_detailsInDealership[i] == _details[indexProblem].Name)
                {
                    dealershipCanRepair = true;
                    _detailsInDealership.RemoveAt(i);

                    Console.WriteLine("У вас на складе есть эта деталь, вы починили машину клинта и получили " + _details[indexProblem].PaymentForRepairs);

                    _money += _details[i].PaymentForRepairs;

                    break;
                }
            }

            if(dealershipCanRepair == false)
            {
                Console.WriteLine("У вас на складе нет этой детали, вы не починили машину клинта и получили штраф " + _details[indexProblem].Fine);

                _money -= _details[indexProblem].Fine;
            }
        }
    }

    class Detail
    {
        public int PaymentForRepairs { get; private set; }
        public string Name { get; private set; }
        public int Fine { get; private set; }
        public string Breakdowns { get; private set; }

        public Detail(int price, string name, int paymentForRepairs, string breakdowns) 
        {
            PaymentForRepairs = price;
            Name = name;
            Fine = paymentForRepairs;
            Breakdowns = breakdowns;
        }
    }
}
