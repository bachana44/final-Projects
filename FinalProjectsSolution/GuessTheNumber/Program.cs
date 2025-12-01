namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int menu = startMenu();
            List<GameLog> listOfLogs = getGameLogs();

            if (menu == 1)
            {
                List<GameLog> sortedList = listOfLogs.OrderByDescending(log => log.score).ToList();
                
                if(sortedList.Count < 10)
                {
                    for (int i = 0; i < sortedList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}) {sortedList[i].userName} - {sortedList[i].score}");
                    }
                } else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine($"{i + 1}) {sortedList[i].userName} - {sortedList[i].score}");
                    }
                }

            }
            else
            {
                string userName = getUserName();


                printMenu();
                int gameLevel = getGameLevel();

                if (gameLevel == 1)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 16);
                    Console.WriteLine("gamoicanit ricxvi 1 idan 15 is chatvlit");
                    startGame(randomNumber, 1, userName);
                }
                else if (gameLevel == 2)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 26);
                    Console.WriteLine("gamoicanit ricxvi 1 idan 25 is chatvlit");
                    startGame(randomNumber, 2, userName);
                }
                else if (gameLevel == 3)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 51);
                    Console.WriteLine("gamoicanit ricxvi 1 idan 50 is chatvlit");
                    startGame(randomNumber, 3, userName);
                }
            }



        }


        static void startGame(int randomNumber, int gameLevel, string userName)
        {
            int playerTries = 10;


            while (playerTries != 0)
            {
                int userNumber = getUserNumber(gameLevel);


                if (userNumber == randomNumber)
                {
                    playerTries--;
                    int gameScore = playerTries * gameLevel;
                    Console.WriteLine($"gilocavt, tqveni sheyvaini ricxvi, {userNumber} sworia, tqven dagjirdat {10 - playerTries} cda, tqveni qula: {gameScore}");
                    saveGameLog(userName, gameScore);
                    break;
                }
                else if (userNumber > randomNumber)
                {
                    Console.Write("tqveni sheyvanili ricxvi metia compiuteris mier chafiqrebul ricxvze, scadet tavidan:");
                }
                else if (userNumber < randomNumber)
                {
                    Console.Write("tqveni sheyvanili ricxvi naklebia compiuteris mier chafiqrebul ricxvze, scadet tavidan: ");
                }

                playerTries--;
            }



        }
        static void printMenu()
        {
            Console.WriteLine("1 - easy");
            Console.WriteLine("2 - medium");
            Console.WriteLine("3 - hard");
            Console.WriteLine("airchiet tamashis done: 1,2,3");
        }
        static int getGameLevel()
        {
            int gameLevel;
            while (!int.TryParse(Console.ReadLine(), out gameLevel) || gameLevel < 1 || gameLevel > 3)
            {
                Console.WriteLine("gtxovt sheiyvanot swori ricxvi: ");
            }
            return gameLevel;
        }
        static string getUserName()
        {
            Console.Write("sheiyvanet tqveni nickname (minimum 4 simbolo): ");
            string userName = Console.ReadLine();

            while (userName.Length < 4)
            {
                Console.WriteLine("scadet xelaxla: ");
                userName = Console.ReadLine();
            }
            return userName;
        }
        static int getUserNumber(int gameLevel)
        {
            int userNumber;
            int max;

            if (gameLevel == 1) {
                max = 16;
            }else if (gameLevel == 2)
            {
                max = 26;
            }else
            {
                max = 51;
            }

            while (!int.TryParse(Console.ReadLine(), out userNumber) || userNumber < 0 || userNumber > max)
            {
                Console.WriteLine("gtxovt shemoiyvanot validuri nomeri: ");
            }
            return userNumber;
        }
        static int startMenu()
        {
            Console.WriteLine("1 - top 10 cxrili");
            Console.WriteLine("2 - daiwye tamashi");
            Console.Write("airchie sasurveli moqmedeba: ");
            int action;
            while (!int.TryParse(Console.ReadLine(), out action) || action < 1 || action > 2) 
            {
                Console.Write("airchiet validuri moqmedeba: ");
            }
            return action;
        }
        static List<GameLog> getGameLogs()
        {
            List<GameLog> list = new List<GameLog>();
            string filePath = "gamelog.csv";

            if (File.Exists(filePath))
            {
                string[] gamelogs = File.ReadAllLines(filePath);

                foreach (string gamelog in gamelogs)
                {
                    // vigebt tito chanawers stringad
                    string singleGamelog = gamelog;

                    string[] arrayOfLog = singleGamelog.Split(',');
                    string userName = arrayOfLog[0];
                    int score = int.Parse(arrayOfLog[1]);
                    GameLog log = new GameLog(userName, score);
                    list.Add(log);
                }
            } else
            {
                File.Create(filePath);
            }

            return list;
        }
        static void saveGameLog(string userName, int score)
        {
            string filePath = "gamelog.csv";
            string logString = $"{userName},{score}";
            File.AppendAllText(filePath, logString + Environment.NewLine);
        }
    }
}
