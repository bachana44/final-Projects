using System.Security.Principal;
using System.Text.Json;

namespace ATMOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Account> accounts = getData();
            if (accounts.Count != 0)
            {
                Account.classId = accounts.Last().accountId + 1;
            } else
            {
                Account.classId = 0;
            }

            int firstAction = printStartMenu();

            if (firstAction == 1)
            {
                Console.WriteLine("registracia");
                Account newAcc = Account.createAccount(accounts);
                accounts.Add(newAcc);
                updateData(accounts);
            } else if (firstAction == 2)
            {
                Console.WriteLine("shesvla");
                Account userAcc = logIn(accounts);
                ATMOperations(userAcc);
                updateData(accounts);
                Console.WriteLine("naxvamdis");

            }

        }

        static int printStartMenu()
        {
            Console.WriteLine("1 - registracia");
            Console.WriteLine("2 - shesvla");
            Console.Write("airchiet operacia, (1,2): ");
            int action;
            while (!int.TryParse(Console.ReadLine(), out action))
            {
                Console.WriteLine("gtxovt sheitvanot validuri ricxvi: ");
            }


            return action;
        }

        static List<Account> getData()
        {
            string filePath = "accounts.json";
            List<Account>? accounts = new List<Account>();

            if (File.Exists(filePath))
            {
                string dataString = File.ReadAllText(filePath);

                if (dataString.TrimStart().StartsWith("["))
                {
                    accounts = JsonSerializer.Deserialize<List<Account>>(dataString);
                }
                else
                {
                    Account account = JsonSerializer.Deserialize<Account>(dataString);
                    accounts = new List<Account> { account };
                }
            } else
            {
                using (File.Create(filePath)) { }
            }
            return accounts;
        }
        static void updateData(List<Account> accounts)
        {
            string filePath = "accounts.json";
            string newData = JsonSerializer.Serialize<List<Account>>(accounts);


            File.WriteAllText(filePath, newData);

        }

        static Account logIn(List<Account> listOfAccounts)
        {
            Console.Write("shiyvanet tqveni piradi nomeri: ");
            string personalNumber = Console.ReadLine();
            Account userAccount = listOfAccounts.FirstOrDefault(acc => acc.personalNumber == personalNumber);

            while (
               !personalNumber.All(char.IsDigit) ||
               personalNumber.Length != 6 ||
               userAccount == null
               )
            {
                Console.Write("gtxovt sheiyvnanot validur piradi nomeri: ");
                personalNumber = Console.ReadLine();
                userAccount = listOfAccounts.FirstOrDefault(acc => acc.personalNumber == personalNumber);
            }

            Console.WriteLine("sheiyvanet pin: ");
            string pin = Console.ReadLine();

            while (
              !pin.All(char.IsDigit) ||
              pin.Length != 4 ||
              pin != userAccount.pin
              )
            {
                Console.Write("gtxovt sheiyvnanot swori pin: ");
                pin = Console.ReadLine();
            }


            Console.WriteLine($"mogesalmebit {userAccount.firstName} {userAccount.lastName}");
            
            return userAccount;
        }

        static void ATMOperations(Account acc)
        {
            Console.Clear();
            Console.WriteLine("1 - balansis Shemowmeba");
            Console.WriteLine("2 - tanxis Shetana");
            Console.WriteLine("3 - tanxis gatana");
            Console.WriteLine("4 - operaciebis istoriis naxva");
            Console.WriteLine("5 - gasvla");
            Console.WriteLine("gtxovt airchiot moqmedeba: ");
            int action;

            while (!int.TryParse(Console.ReadLine(), out action) || action < 1 || action > 5)
            {
                Console.WriteLine("Gtxovt sheiyvanot validuri ricxvi 1-5: ");
            }

            if (action == 1)
            {
                checkBalance(acc);
            } else if (action == 2)
            {
                deposit(acc);
            }
            else if (action == 3)
            {
                withdraw(acc);
            }
            else if (action == 4)
            {
                printAccLogs(acc);
            }
        }

        static void checkBalance(Account acc)
        {
            Console.WriteLine($"tqveni balansi sheadgens {acc.balance}");
            acc.logs.Add($"tqven sheamowmet balansi {DateTime.Now}");

        }

        static void deposit(Account acc)
        {
            Console.Write("sheiyvanet tanxa: ");
            int deposit;
            while (!int.TryParse(Console.ReadLine(), out deposit) || deposit < 1)
            {
                Console.Write("gtxovt sheiyvanot swori tanxa");
            }
            acc.balance += deposit;

            Console.WriteLine($"waramtebit sheitanet tanxa: {deposit}, tqveni balansi sheadgens {acc.balance}, am dros {DateTime.Now}");
            acc.logs.Add($"tqven sheitanet tanxa {deposit}, am dros {DateTime.Now}");
        }

        static void withdraw(Account acc)
        {
            Console.Write($"tqveni banalsi aris {acc.balance} sheiyvanet gamostani tanxa: ");
            int withdraw;
            while (!int.TryParse(Console.ReadLine(), out withdraw) || withdraw < 1 || withdraw > acc.balance)
            {
                Console.Write("gtxovt sheiyvanot swori tanxa");
            }
            acc.balance -= withdraw;

            Console.WriteLine($"waramtebit gamoitanet tanxa: {withdraw}, tqveni balansi sheadgens {acc.balance}");
            acc.logs.Add($"tqven gamoitanet tanxa {withdraw}, am dros {DateTime.Now}");

        }

        static void printAccLogs(Account acc)
        {
            acc.logs.ForEach(log => Console.WriteLine(log));
            acc.logs.Add($"tqven sheamowmet logebis istoria am dros {DateTime.Now}");
        }
    }
}
