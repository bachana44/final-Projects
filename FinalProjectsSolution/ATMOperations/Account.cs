using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ATMOperations
{
    internal class Account
    {
        public static int classId { get; set; }
        public int accountId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personalNumber { get; set; }
        public int balance { get; set; }
        public string pin { get; set; }
        public List<string> logs { get; set; } = new List<string>();



        public Account() 
        {
            this.balance = 0;
            this.pin = createPin();
            this.accountId = classId;
        }
        public static string createPin()
        {
            Random random = new Random();
            List<int> pinCode = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                pinCode.Add(random.Next(0, 10));
            }
            return string.Join("", pinCode);
        }

        public static Account createAccount(List<Account> accountsList)
        {
            Account account = new Account();
            Console.Write("sheitvanet tqveni saxeli (minimum 2 simbolo): ");
            account.firstName = Console.ReadLine();
            while (account.firstName.Length < 2)
            {
                Console.Write("gtxovt saxelshi sheiyvnot 2 ze meti simbolo: ");
                account.firstName = Console.ReadLine();
            }

            Console.Write("sheitvanet tqveni gvari (minimum 2 simbolo): ");
            account.lastName = Console.ReadLine();
            while (account.lastName.Length < 2)
            {
                Console.Write("gtxovt gvarshi sheiyvnot 2 ze meti simbolo: ");
                account.lastName = Console.ReadLine();
            }

            Console.Write("sheiyvane tqveni piradi nomeri: ");
            account.personalNumber = Console.ReadLine();
            while (
                !account.personalNumber.All(char.IsDigit) || 
                account.personalNumber.Length != 6 || 
                accountsList.Any(acc => acc.personalNumber == account.personalNumber)
                )
            {
                Console.Write("gtxovt sheiyvnanot validur piradi nomeri: ");
                account.personalNumber = Console.ReadLine();
            }

            account.logs.Add($"{account.firstName} {account.lastName} ({account.personalNumber}) daregistrirda {DateTime.Now}");

            return account;
        }
    }
}
