using System;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string action = getAction();
            double firstNumber = getNumber("first");
            double secondNumber = getNumber("second");

            Console.Clear();
            double result = calculate(action, firstNumber, secondNumber);

            printResult(action, firstNumber, secondNumber, result);


            bool continueApp = true;
            while (continueApp)
            {
                printMenu();

                int continueCalculate = getMenuChoice();

                if (continueCalculate == 1)
                {
                    action = getAction();
                    firstNumber = result;
                    secondNumber = getNumber("second");
                    result = calculate(action, firstNumber, secondNumber);
                    printResult(action, firstNumber, secondNumber, result);
                }
                else if (continueCalculate == 2)
                {
                    action = getAction();
                    firstNumber = getNumber("first");
                    secondNumber = getNumber("second");
                    result = calculate(action, firstNumber, secondNumber);
                    printResult(action, firstNumber, secondNumber, result);
                }
                else if (continueCalculate == 3) continueApp = false;
            }


        }
        static double calculate(string action, double firstNumber, double secondNumber)
        {

            if (action == "+")
            {
                return firstNumber + secondNumber;

            }
            else if (action == "-")
            {
                return firstNumber - secondNumber;  
            }
            else if (action == "*")
            {
                return (firstNumber * secondNumber);
            }
            else if (action == "/")
            {
                if(secondNumber != 0) {
                    return (firstNumber / secondNumber);
                } else {
                    return double.NaN;
                }
            }

            return default;

        }
        static string getAction()
        {
            Console.Write("sheiyvanet sasurveli moqmedeba: (+,-,*,/): ");
            string action = Console.ReadLine();
            while (action != "+" && action != "-" && action != "*" && action != "/")
            {
                Console.Write("araswori operacia, gtxovt airchiot tavidan (+, -, *, /): ");
                action = Console.ReadLine();
            }

            return action;
        }
        static double getNumber(string placement)
        {
            Console.Write($"enter {placement} number: ");
            double number;

            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("gtxovt sheiyvanot validuri ricxvi: ");
            }
            return number;
        }
        static void printMenu() {
            Console.WriteLine("(1)-Gagrdzeleba");
            Console.WriteLine("(2)-Xelaxla Gamotvla");
            Console.WriteLine("(3)-Gatishva");
            Console.Write("Airchiet Shemdegi moqmedeba (1,2,3):");
        }
        static void printResult(string action, double firstNumber, double secondNumber, double result)
        {
            if (!double.IsNaN(result))
            {
                Console.WriteLine($"{firstNumber} {action} {secondNumber} = {result}");
            }
            else if (double.IsNaN(result))
            {
                Console.WriteLine("nolze gayofa ar sheidzleba!");
            }
        }
        static int getMenuChoice()
        {
            int menuNumber;
            while (!int.TryParse(Console.ReadLine(), out menuNumber) || menuNumber > 3 || menuNumber < 1)
            {
                Console.Write("tqven mier shemoyvanili ricxvi ar moidzebneba menu-shi, sheiyvanet xelaxla: ");
            }
            return menuNumber;
        }
    }
}