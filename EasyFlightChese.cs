using System;

namespace testClass4
{
    class Program
    {
        public static int[] Maps = new int[100];
        public enum MapType
        {
            Normal,
            LuckyTurn,
            LandMine,
            Pause,
            TimeTurn
        }
        public struct PlayerStatus
        {
            public int Pos;
            public int Type;
            public string Name;
            public bool Flag;
        }
        public struct MapStatus
        {
            public int Pos;
            public int Type;
        }
        public static MapStatus[] FormerStatus = new MapStatus[2];
        public static PlayerStatus[] CurrentPlayer = new PlayerStatus[2];
        static void Main(string[] args)
        {
            GameShow();
            Console.Write("请输入玩家A的姓名：");
            CurrentPlayer[0].Name = Console.ReadLine();
            while (CurrentPlayer[0].Name == "")
            {
                Console.Write("玩家A的姓名不能为空，请重新输入：");
                CurrentPlayer[0].Name = Console.ReadLine();
            }
            Console.Write("请输入玩家B的姓名：");
            CurrentPlayer[1].Name = Console.ReadLine();
            while (CurrentPlayer[1].Name == "" || CurrentPlayer[1].Name == CurrentPlayer[0].Name)
            {
                if (CurrentPlayer[1].Name == "")
                {
                    Console.Write("玩家B的姓名不能为空，请重新输入：");
                   CurrentPlayer[1].Name = Console.ReadLine();
                }
                else
                {
                    Console.Write("玩家B的名字不能与玩家A一样，请重新输入：");
                    CurrentPlayer[1].Name = Console.ReadLine();
                }
            }
            Console.Clear();
            GameShow();
            Console.WriteLine("{0}的士兵用A表示。", CurrentPlayer[0].Name);
            Console.WriteLine("{0}的士兵用B表示。", CurrentPlayer[1].Name);
            InitMaps();
            ShowMap();
            while (CheckWin() == -1){
                if (CurrentPlayer[0].Flag == false)
                {
                    PlayGame(0);
                }
                else
                {
                    CurrentPlayer[0].Flag = false;
                }
                if (CurrentPlayer[1].Flag == false)
                {
                    PlayGame(1);
                }
                else
                {
                    CurrentPlayer[1].Flag = false;
                }
            }
            Console.WriteLine("恭喜玩家{0}获得胜利，掌声响起来。",CurrentPlayer[CheckWin()].Name);
            Console.WriteLine("{0}同学，我不想再看到你。", CurrentPlayer[1-CheckWin()].Name);
            Console.ReadKey();
        }
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("***********超级飞行棋 V1.00***********");
            Console.WriteLine("*******By Arithman 2017-7-30**********");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("**************************************");
        }
        public static void InitMaps()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("地图初始化中，请稍等.....");
            int[] luckyTurn = { 6, 23, 40, 55, 69, 83 };
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };
            int[] pause = { 9, 27, 60, 93 };
            int[] timeTurn = { 20, 25, 45, 63, 72, 88, 90 };
            DateToMaps(luckyTurn, MapType.LuckyTurn);
            DateToMaps(landMine, MapType.LandMine);
            DateToMaps(pause, MapType.Pause);
            DateToMaps(timeTurn, MapType.TimeTurn);

        }
        public static void DateToMaps(int[] dates,MapType type)
        {
            int len = dates.Length;
            for (int i = 0; i < len; i++)
            {
                int index = dates[i];
                Maps[index] = (int)type;
            }
        }
        public static void ShowMap()
        {
            Console.Clear();
            Console.WriteLine("{0}的士兵用A表示,{1}的士兵用B表示。", CurrentPlayer[0].Name,CurrentPlayer[1].Name);
            Console.WriteLine("图例:  幸运转盘:◎  地雷:★    暂停:▲    时空隧道:卐");
            Maps[FormerStatus[0].Pos] = FormerStatus[0].Type;
            Maps[FormerStatus[1].Pos] = FormerStatus[1].Type;
            ShowPlayer();
            Console.ForegroundColor = ConsoleColor.Yellow;
            for(int i = 0; i < 13; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    if (i==0)
                    {
                        Console.Write(MapToChar(Maps[j]));
                    }
                    else if ((i==1||i==2||i==3||i==4||i==5)&&(j==29))
                    {
                        Console.Write(MapToChar(Maps[29+i]));
                    }
                    else if (i==6)
                    {
                        Console.Write(MapToChar(Maps[29-j+35]));
                    }
                    else if((i == 7 || i == 8 || i == 9 || i == 10 || i == 11) && (j == 0))
                    {
                        Console.Write(MapToChar(Maps[i+58]));
                    }
                    else if (i==12)
                    {
                        Console.Write(MapToChar(Maps[j+70]));
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
    }
        
        public static string MapToChar(int typeCode)
        {
            string returnChar="";
            switch (typeCode)
            {
                case 0:
                    returnChar="□";
                    break;
                case 1:
                    returnChar= "◎";
                    break;
                case 2:
                    returnChar="★";
                    break;
                case 3:
                    returnChar="▲";
                    break;
                case 4:
                    returnChar="卐";
                    break;
                case 5:
                    returnChar = "A ";
                    break;
                case 6:
                    returnChar = "B ";
                    break;
                case 7:
                    returnChar = "<>";
                    break;
                default:
                    break;
            }
            return returnChar;
        }
        public static void ShowPlayer()
        {
            FormerStatus[0].Pos= CurrentPlayer[0].Pos;
            FormerStatus[0].Type = Maps[CurrentPlayer[0].Pos];
            FormerStatus[1].Pos = CurrentPlayer[1].Pos;
            FormerStatus[1].Type = Maps[CurrentPlayer[1].Pos];
            if (CurrentPlayer[0].Pos == CurrentPlayer[1].Pos)
            {
                Maps[CurrentPlayer[0].Pos] = 7;
            }
            else
            {
                Maps[CurrentPlayer[0].Pos] = 5;
                Maps[CurrentPlayer[1].Pos] = 6;
            }
        }
        public static void PlayGame(int playerId)
        {
            Console.WriteLine("玩家{0}开始掷骰子。",CurrentPlayer[playerId].Name);
            Console.ReadKey();
            Random rnd = new Random();
            int rndNumber = rnd.Next(1, 7);
            Console.WriteLine("玩家{0}掷了{1}点。",CurrentPlayer[playerId].Name,rndNumber);
            CurrentPlayer[playerId].Pos += rndNumber;
            CurrentPlayer[playerId].Pos = CheckPosition(CurrentPlayer[playerId].Pos);
            if (CurrentPlayer[playerId].Pos == CurrentPlayer[1 - playerId].Pos)
            {
                Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{1}退6格，真是个悲剧。",CurrentPlayer[playerId].Name,CurrentPlayer[1-playerId].Name, CurrentPlayer[1 - playerId].Name);
                CurrentPlayer[1 - playerId].Pos -= 6;
                CurrentPlayer[1 - playerId].Pos=CheckPosition(CurrentPlayer[1 - playerId].Pos);
                JudgeMap(1-playerId);
            }
            else
            {
                JudgeMap(playerId);
            }
            ShowMap();

        }
        public static int CheckPosition(int pos)
        {
            if (pos < 0)
            {
                return 0;
            }
            else if (pos > 99)
            {
                return 99;
            }
            else
            {
                return pos;
            }
        }
        public static void JudgeMap(int id)
        {
            switch (Maps[CurrentPlayer[id].Pos])
            {
                case 0:
                    Console.WriteLine("玩家{0}踩到了方块，安全。",CurrentPlayer[id].Name);
                    Console.ReadKey(true);
                    break;
                case 1:
                    Console.Write("恭喜玩家{0}获得了辛运大转盘。输入 1--前进6格  2--与其他玩家交换位置，请输入您的选择：", CurrentPlayer[id].Name);
                    string choice = Console.ReadLine();
                    while (true){
                        if (choice == "1")
                        {
                            Console.WriteLine("玩家{0}选择前进6格。", CurrentPlayer[id].Name);
                            Console.ReadKey(true);
                            CurrentPlayer[id].Pos += 6;
                            CurrentPlayer[id].Pos = CheckPosition(CurrentPlayer[id].Pos);
                            Console.ReadKey(true);
                            break;
                        }
                        else if (choice == "2")
                        {
                            Console.WriteLine("玩家{0}将与玩家{1}交换位置。", CurrentPlayer[id].Name, CurrentPlayer[1 - id].Name);
                            int tmp = CurrentPlayer[id].Pos;
                            CurrentPlayer[id].Pos = CurrentPlayer[1 - id].Pos;
                            CurrentPlayer[1 - id].Pos = tmp;
                            Console.ReadKey(true);
                            break;
                        }
                        else
                        {
                            Console.Write("输入错误，1--前进6格  2--与其他玩家交换位置，请输入您的选择：");
                            choice = Console.ReadLine();
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("玩家{0}踩到了地雷，后退6格，真是喜大普奔。",CurrentPlayer[id].Name);
                    CurrentPlayer[id].Pos -= 6;
                    CurrentPlayer[id].Pos = CheckPosition(CurrentPlayer[id].Pos);
                    Console.ReadKey(true);
                    break;
                case 3:
                    Console.WriteLine("玩家{0}踩到了暂停，他得休息一回合了。",CurrentPlayer[id].Name);
                    CurrentPlayer[id].Flag = true;
                    Console.ReadKey(true);
                    break;
                case 4:
                    Console.WriteLine("玩家{0}遇到了时空隧道，前进10格，真是走了狗屎运。",CurrentPlayer[id].Name);
                    CurrentPlayer[id].Pos += 10;
                    CurrentPlayer[id].Pos = CheckPosition(CurrentPlayer[id].Pos);
                    Console.ReadKey(true);
                    break;
            }
            if ((Maps[CurrentPlayer[id].Pos]==1|| Maps[CurrentPlayer[id].Pos]==2|| Maps[CurrentPlayer[id].Pos]==3|| Maps[CurrentPlayer[id].Pos]==4)&&(CurrentPlayer[id].Flag==false))
            {
                JudgeMap(id);
            }
        }
        public static int CheckWin()
        {
            if (CurrentPlayer[0].Pos >= 99)
            {
                return 0;
            }
            else if (CurrentPlayer[1].Pos >= 99)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }
}
