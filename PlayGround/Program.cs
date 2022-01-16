using System;

using System.Threading;


namespace PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            /*<summery>
            Very basic casino style game made by Ithamar Baron.
            btw in lines <26> and <28-48> im doing the same thing in 2 diffrent ways.
            here are some statistics:
            Blue  x2 (40%) | Green x3(30%),Yellow x5(8%),Red x10 (2%),Gray x0 20% */
            ConsoleColor[] colors = {ConsoleColor.Blue,ConsoleColor.Green,ConsoleColor.Yellow,ConsoleColor.Red,ConsoleColor.DarkGray};
            ConsoleColor[] allColors = { ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.DarkYellow, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.DarkGreen, ConsoleColor.Yellow };
            bool notAllowed = true;
            int wallet = 0;
            bool keepGoing = true;
            CasinoText(allColors);
            while (keepGoing)
            {
                //taking wallet info and cheacking it
                wallet = IsAllowedForWallet(wallet);

                //entering bet
                int bet = 0;
            FalseBet:
                try
                {
                    Console.Write("Enter your betting amount: ");
                    bet = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Error("Number required");
                    goto FalseBet;
                }
                catch (OverflowException)
                {
                    Error("dont even try");
                    goto FalseBet;
                }

                //making sure bet isnt causing bugs
                bet = IsAllowedForBet(notAllowed, bet, wallet);

                //displaying colors
                DisplayColors(colors);
                displayCR(colors);

                //letting the user to choose a color
                string colorChoice;
                Console.Write("\nEnter the color you want to bet on: ");
                colorChoice = Console.ReadLine();

                //making sure color choice is allowed
                colorChoice = IsAllowedForColor(notAllowed, colorChoice);

                //taking off bet money
                wallet -= bet;
                //displaying data about the bet
                BetData(bet, wallet);
                //calculating
                int earnings = Roll(colorChoice, bet, colors);
                wallet += earnings;

                //displaying data about the game
                EarningsData(earnings, wallet);

                //asking to keep going
                keepGoing = DoWeKeepGoing(keepGoing);
            }




        }

        public static int Roll(string colorChose,int bet, ConsoleColor[] colors)
        {
            colorChose = colorChose.ToLower();

            int winnings = 0;
            Random rnd = new Random();
            byte num = (byte)rnd.Next(1,101);
            RollingIn(10);
            ColorRullet(colors);
            if (num <=2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Rolled RED!!");
                Console.ResetColor();
                Console.WriteLine("You choose: " + colorChose);
                if ((colorChose == "red"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("IT IS RED U WON THE FUCKING JACKPOT HOLY SHIT");
                    winnings = bet * 10;
                }
                Console.ResetColor();
            }
            else if(num <= 10)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Rolled YELLOW!");
                Console.ResetColor();
                Console.WriteLine("You choose: " + colorChose);
                if ((colorChose == "yellow"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("IT IS YELLOW YOU WON");
                    winnings = bet * 5;

                }
                Console.ResetColor();
            }
            else if(num <= 40)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Rolled GREEN!");
                Console.ResetColor();
                Console.WriteLine("You choose: " + colorChose);
                if ((colorChose == "green"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("IT IS GREEN YOU WON");
                    winnings = bet * 3;

                }
                Console.ResetColor();
            }
            else if (num <= 80)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Rolled BLUE!");
                Console.ResetColor();
                Console.WriteLine("You choose: " + colorChose);
                if ((colorChose == "blue"))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("IT IS BLUE YOU WON");
                    winnings = bet * 2;
                }
                Console.ResetColor();
            }
            else if(num > 80)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Rolled GRAY (hehe u lose)");
                winnings = 0;
                Console.ResetColor();

            }
            else
            {
                Error("how tf did u not win or lose");
            }
            return winnings;
        }

        static void Error(string errCode)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=====================<!>=====================");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" sorry something went wrong please try again");
            Console.WriteLine(" Error Code: "+errCode);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=====================<!>=====================");
            Console.ResetColor();
        }

        static void DisplayColors(ConsoleColor[] colors)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                Console.ForegroundColor = colors[i];
                Console.Write(" "+colors[i]+" ");
            }
            Console.ResetColor();
        }

        static void displayCR(ConsoleColor[] colors)
        {
            //<Blue  x2 (40%) | Green x3(30%),Yellow x5(8%),Red x10 (2%)>
            //blue
            Console.WriteLine();
            Console.ForegroundColor = colors[0];
            Console.Write("x2|40% ");
            //green
            Console.ForegroundColor = colors[1];
            Console.Write("x3|30% ");
            //yellow
            Console.ForegroundColor = colors[2];
            Console.Write("x5|8% ");
            //red
            Console.ForegroundColor = colors[3];
            Console.Write("x10|2% ");
            Console.ForegroundColor = colors[4];
            Console.Write("x0|20% ");

            Console.ResetColor();

        }

        static string IsAllowedForColor(bool isAllowed,string cc)
        {
            cc.ToLower();
            string errMsg;
            while (isAllowed)
            {
                if (cc != "red" && cc != "blue" && cc != "green" && cc != "yellow")
                {
                    if (cc=="darkgray"||cc=="dark gray")
                    {
                        errMsg = "Dark Gray isn't a color you can choose its you're losing chances(x0)";
                    }
                    else
                    {
                        errMsg = "Please pick from the colors above!";
                    }
                    Error(errMsg);
                    isAllowed = true;
                    Console.Write("\nEnter the color you want to bet on: ");
                    cc = Console.ReadLine();
                }
                else
                {
                    isAllowed = false;
                }
            }
            return cc;
        }

        static int IsAllowedForBet(bool isAllowed,int ba,int inWallet)
        {
        L1:
            while (isAllowed)
            {
                if (ba == 0)
                {
                    
                    try
                    {
                        Error("Whats the point of betting 0?");
                        Console.Write("\nEnter your betting amount: ");
                        ba = int.Parse(Console.ReadLine());
                        isAllowed = true;
                    }
                    catch (FormatException)
                    {
                        Error("Expecting a number");
                        goto L1;
                    }
                    catch (OverflowException)
                    {
                        Error("Dont even try >:|");
                        goto L1;
                    }


                }
                else if (ba < 0)
                {
                    try
                    {
                        Error("Cant bet a negative number");
                        isAllowed = true;
                        Console.Write("\nEnter your betting amount: ");
                        ba = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Error("Expecting a number");
                        goto L1;
                    }
                    catch (OverflowException)
                    {
                        Error("Dont even try >:|");
                        goto L1;
                    }

                }
                else if (ba > inWallet)
                {
                    try
                    {
                        Error("You cant bet more the you have!");
                        isAllowed = true;
                        Console.Write("\nEnter your betting amount: ");
                        ba = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Error("Expecting a number");
                        goto L1;
                    }
                    catch (OverflowException)
                    {
                        Error("Dont even try >:|");
                        goto L1;
                    }

                }
                else
                {
                    isAllowed = false;
                }

            }
            return ba;
        }
        static int IsAllowedForWallet(int w)
        {
        
            bool isntAllowed = true;
            while (isntAllowed)
            {
                L1:
                try
                {
                    Console.Write("\nFill your wallet [currently " + w + "]: ");
                    w += int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Error("Expecting a number");
                    goto L1;
                }
                catch (OverflowException)
                {
                    Error("Dont even try >:|");
                    goto L1;
                }
                L3:
                if (w<0)
                {
                    w = 0;
                    Error("Must be a positive number");
                    goto L1;
                }
                else
                {
                    isntAllowed = false;
                }

            L2:
                if (w == 0)
                {
                    Error("Whats the point of depositing 0?");
                    isntAllowed = true;
                    try
                    {
                        Console.Write("\nFill your wallet [currently " + w + "]: ");
                        w += int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Error("Expecting a number");
                        goto L2;
                    }
                    catch (OverflowException)
                    {
                        Error("Dont even try >:|");
                        goto L2;
                    }
                    if (w <0)
                    {
                        goto L3;
                    }
                }
                else
                {
                    isntAllowed = false;
                }
            }
            return w;
        }
        static void ColorRullet(ConsoleColor[] colors)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < colors.Length; i++)
                {

                    Console.ForegroundColor = colors[i];
                    Console.WriteLine("Rolling!!");
                    Thread.Sleep(500);
                    Console.Clear();
                }
                Console.ResetColor();
            }
        }

        static void RollingIn(byte sc)
        {
            while (sc !=0)
            {
                Console.WriteLine("Rolling In "+sc+"!");
                Thread.Sleep(1000);
                sc--;
            }
        }
        static void BetData(int bet, int wallet)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("==================================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" you placed a bet of ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + bet + "$] ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("You now have ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + wallet + "$] ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("In your wallet");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("==================================================================================");
            Console.ResetColor();
        }

        static void EarningsData(int earnings, int wallet)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==================================================================================");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" You Earned ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + earnings + "$] ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("From this round you now have ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + wallet + "$] ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("In your wallet");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==================================================================================");
            Console.ResetColor();
        }
        static bool DoWeKeepGoing(bool keepGoing)
        {
            bool goodAnswer = false;
            string answer = "";
            while (!goodAnswer)
            {
                Console.Write("wanna keep going?:");
                answer = Console.ReadLine();
                if (answer.ToLower() == "yes" || answer.ToLower() == "yep")
                {
                    keepGoing = true;
                    goodAnswer = true;
                    Thread.Sleep(100);
                    Console.Clear();
                    
                }
                else if (answer.ToLower() == "no" || answer.ToLower() == "nope")
                {
                    keepGoing = false;
                    goodAnswer = true;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("ok cya later");
                    Console.ResetColor();
                }
                else
                {
                    goodAnswer = false;
                    Error("umm.. a YES/NO qestion dumbass");
                    
                }
            }
            return keepGoing;
        }

        static void CasinoText(ConsoleColor[] colors)
        {

            for (int i = 0; i < colors.Length; i++)
            {
                Console.ForegroundColor = colors[i];
                Console.WriteLine("      \n      ░█████╗░░█████╗░░██████╗██╗███╗░░██╗░█████╗░\n      ██╔══██╗██╔══██╗██╔════╝██║████╗░██║██╔══██╗\n      ██║░░╚═╝███████║╚█████╗░██║██╔██╗██║██║░░██║\n      ██║░░██╗██╔══██║░╚═══██╗██║██║╚████║██║░░██║\n      ╚█████╔╝██║░░██║██████╔╝██║██║░╚███║╚█████╔╝\n      ░╚════╝░╚═╝░░╚═╝╚═════╝░╚═╝╚═╝░░╚══╝░╚════╝░");
                Thread.Sleep(150);
                Console.Clear();
            }
            Console.ResetColor();
        }



    }
}



//u read u gay  ̿̿ ̿̿ ̿̿ ̿'̿'\̵͇̿̿\з= ( ▀ ͜͞ʖ▀) =ε/̵͇̿̿/’̿’̿ ̿ ̿̿ ̿̿ ̿̿

//special thx to: sahar and omer