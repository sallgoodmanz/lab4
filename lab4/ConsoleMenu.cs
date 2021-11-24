using System;
using System.Collections.Generic;
using System.Text;
using BLL;

namespace lab4
{
    public static class ConsoleMenu
    {
        #region servide functions
        private static bool RemoveBankAccount(List<BankAccount> accountList, int accountName)
        {
            foreach (var account in accountList)
            {
                if (accountName == account.AccountName)
                {
                    bankAccounts.Remove(account);
                    return true;
                }
            }
            return false;
        }
        private static bool BankAccountExists(List<BankAccount> accountList, int accountName)
        {
            foreach (var account in accountList)
            {
                if (accountName == account.AccountName)
                {
                    return true;
                }
            }
            return false;
        }
        private static BankAccount GetBankAccountByName(List<BankAccount> accountList, int accountName)
        {
            foreach (var account in accountList)
            {
                if (accountName == account.AccountName)
                {
                    return account;
                }
            }
            throw new Exception("Заданого аккаунту не існує!");
        }
        private static BankAccount CreateBankAccount()
        {
            Console.Write("Введіть номер рахунку: ");
            string inputAccountName = Console.ReadLine(); int accountName = int.Parse(inputAccountName);
            foreach (var account in bankAccounts)
            {
                if (account.AccountName == accountName)
                {
                    throw new Exception("Номер рахунку вже використовується!");
                }
            }
            return new BankAccount(accountName);
        }
        private static void CheckBalance(BankAccount bankAccount)
        {
            Console.Write($@"Баланс рахунку №{bankAccount.AccountName} становить"); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($@" { bankAccount.AccountBalance}"); Console.ResetColor(); Console.WriteLine(" грн");

            Console.Write($@"Баланс депозитного рахунку №{bankAccount.AccountName} становить"); Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($@" { bankAccount.AccountDepositBalance}"); Console.ResetColor(); Console.WriteLine(" грн");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($@"Борг: {bankAccount.GetMoneyToRepay()} грн");
            Console.ResetColor();
            Console.ReadLine();
        }
        private static void OutputAllBankAccount(List<BankAccount> accountList)
        {
            if (accountList.Count != 0)
            {
                foreach (var account in accountList)
                {
                    Console.WriteLine($@"Рахунок номер: { account.AccountName}");
                }
            }
            else Console.WriteLine("Не знайдено жодного банківського рахунку!");
            Console.ReadLine();
        }
        #endregion

        static List<BankAccount> bankAccounts = new List<BankAccount>();

        //Event
        public static void RepayCredit(object sender, OverdraftEvent eventArgs)
        {
            var bankAccount = sender as BankAccount;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            if (bankAccount.GetMoneyToRepay() > 0)
            {
                if (bankAccount.GetMoneyToRepay() > bankAccount.AccountBalance)
                {
                    bankAccount.OverdraftRepay();
                    bankAccount.AccountBalance = 0;
                    Console.WriteLine($"Потрібно сплатити за кредит: {bankAccount.GetMoneyToRepay()}\t Поточний рахунок: {  bankAccount.AccountBalance}");
                }
                if (bankAccount.GetMoneyToRepay() == bankAccount.AccountBalance)
                {
                    bankAccount.OverdraftRepay();
                    bankAccount.AccountBalance = 0;
                    Console.WriteLine($"Кредит був успішно закрит!\t Поточний рахунок: {  bankAccount.AccountBalance}");
                    bankAccount.RepayCredit();
                }
                if (bankAccount.GetMoneyToRepay() < bankAccount.AccountBalance)
                {
                    double temp = Math.Abs(bankAccount.AccountBalance - bankAccount.GetMoneyToRepay());
                    bankAccount.OverdraftRepay();
                    bankAccount.AccountBalance = temp;
                    Console.WriteLine($"Кредит був успішно закрит!\t Поточний рахунок: {  bankAccount.AccountBalance}");
                    bankAccount.RepayCredit();
                }
                Console.ReadKey();
            }
            Console.ResetColor();
        }
        public static void MainMenu()
        {
            try
            {
                while (true)
                {

                    Console.Clear();
                    Console.WriteLine("\t\tMAIN MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Робота з делегатом");
                    Console.WriteLine("2 - Переглянути всі банківські рахунки");
                    Console.WriteLine("3 - Вибрати банківський рахунок для роботи");
                    Console.WriteLine("4 - Створити банківський рахунок");
                    Console.WriteLine("5 - Видалити банківський рахунок");
                    Console.WriteLine("6 - Закінчити роботу");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);

                    switch (_str)
                    {
                        case 1:
                            {
                                Console.Clear();
                                DelegateMenu();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                OutputAllBankAccount(bankAccounts);
                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                Console.Write("Введіть номер аккаунта: ");
                                string _accountName = Console.ReadLine(); int accountName = int.Parse(_accountName);
                                AccountMenu(accountName);
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                bankAccounts.Add(CreateBankAccount());
                                break;
                            }
                        case 5:
                            {
                                Console.Clear();
                                DeleteMenu();
                                break;
                            }
                        case 6:
                            {
                                Console.Clear();
                                Environment.Exit(0);
                                break;
                            }

                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до MAIN MENU");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($@"Exception: {e.Message}");
                Console.Write("\nНатисніть ENTER щоб повернутись до MAIN MENU");
                Console.ReadKey();
                Console.Clear();
                MainMenu();
            }
        }
        #region service menus
        private static void DelegateMenu()
        {
            int[,] array = new int[9, 6]
            {
                { 9,9,9,9,9,9},
                { 8,8,8,8,8,8},
                { 7,7,7,7,7,7},
                { 6,6,6,6,6,6},
                { 5,5,5,5,5,5},
                { 4,4,4,4,4,4},
                { 3,3,3,3,3,3},
                { 2,2,2,2,2,2},
                { 1,1,1,1,1,1},
            };
            Func<int[,], int[]> function;

            bool status = true;
            while (status)
            {
                status = false;

                Console.WriteLine("\t\tDELEGATE");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("1 - Анонімний метод");
                Console.WriteLine("2 - Лямбда вираз");
                Console.WriteLine("-----------------------------------------");


                string str = Console.ReadLine();
                int choice = int.Parse(str);

                switch (choice)
                {
                    case 1:
                        {
                            Console.Clear();

                            function = delegate (int[,] array)
                            {
                                int[] outputArray = new int[array.GetLength(0)];

                                for (int i = 0; i < array.GetLength(0); i++)
                                {
                                    int sum = 0;
                                    for (int j = 0; j < array.GetLength(1); j++)
                                    {
                                        sum += array[i, j];
                                    }
                                    outputArray[i] = sum;
                                }
                                return outputArray;
                            };

                            Console.WriteLine("     Початковий вигляд масиву: ");
                            Console.WriteLine("__________________________________");
                            for (int i = 0; i < array.GetLength(0); i++)
                            {
                                for (int j = 0; j < array.GetLength(1); j++)
                                {
                                    Console.Write(array[i, j] + "  |  ");
                                }
                                Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
                            }

                            Console.ReadKey();

                            Console.WriteLine("\n\tРезультат роботи ");
                            Console.Write("Сума рядків двовимірного масиву: ");
                            Console.WriteLine("\n___________________________________________________________");

                            var result = function?.Invoke(array);
                            for (int i = 0; i < result.GetLength(0); i++)
                            {
                                Console.Write(result[i] + "  |  ");
                            }
                            Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            function = (array) =>
                            {
                                int[] outputArray = new int[array.GetLength(0)];

                                for (int i = 0; i < array.GetLength(0); i++)
                                {
                                    int sum = 0;
                                    for (int j = 0; j < array.GetLength(1); j++)
                                    {
                                        sum += array[i, j];
                                    }
                                    outputArray[i] = sum;
                                }
                                return outputArray;
                            };

                            Console.WriteLine("     Початковий вигляд масиву: ");
                            Console.WriteLine("__________________________________");
                            for (int i = 0; i < array.GetLength(0); i++)
                            {
                                for (int j = 0; j < array.GetLength(1); j++)
                                {
                                    Console.Write(array[i, j] + "  |  ");
                                }
                                Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
                            }

                            Console.ReadKey();

                            Console.WriteLine("\n\tРезультат роботи ");
                            Console.Write("Сума рядків двовимірного масиву: ");
                            Console.WriteLine("\n___________________________________________________________");

                            var result = function?.Invoke(array);
                            for (int i = 0; i < result.GetLength(0); i++)
                            {
                                Console.Write(result[i] + "  |  ");
                            }
                            Console.WriteLine("\n‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
                            Console.ReadKey();
                            break;
                        }

                    default:
                        status = true;
                        break;
                }
            }
        }
        private static void DeleteMenu()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\t\tDELETE MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Видалити всі банківські рахунки");
                    Console.WriteLine("2 - Видалити банківський рахунок");
                    Console.WriteLine("3 - Повернутись до MAIN MENU");
                    Console.WriteLine("4 - Закінчити роботу");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);
                    switch (_str)
                    {
                        case 1:
                            {
                                Console.Clear();
                                bankAccounts = null;
                                Console.WriteLine("Операція пройшла успішно!");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Console.Write("Введіть номер рахунку: ");
                                string _accountName = Console.ReadLine(); int accountName = int.Parse(_accountName);
                                Console.Clear();
                                if (RemoveBankAccount(bankAccounts, accountName))
                                    Console.WriteLine("Операція прошла успішно");
                                else Console.WriteLine("Заданого аккаунут не існує!");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                MainMenu();
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                Environment.Exit(0);
                                break;
                            }
                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до DELETE MENU");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine($@"Exception: {e.Message}");
                    Console.Write("\nНатисніть ENTER щоб повернутись до DELETE MENU");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        private static void AccountMenu(int accountName)
        {
            if (BankAccountExists(bankAccounts, accountName))
            {
                var loggedAccount = GetBankAccountByName(bankAccounts, accountName);
                loggedAccount.Overdraft += RepayCredit;
                WorkMenu(loggedAccount);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Заданого аккаунту не існує!");
                Console.WriteLine("\nНатисніть ENTER щоб повернутись до MAIN MENU");
                Console.ReadKey();
                Console.Clear();
                MainMenu();
            }
        }
        private static void WorkMenu(BankAccount bankAccount)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\t\tWORK MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Перевірити баланс рахунку");
                    Console.WriteLine("2 - Поповнити рахунок");
                    Console.WriteLine("3 - Зняти готівку");
                    Console.WriteLine("4 - Покласти кошти на депозит");
                    Console.WriteLine("5 - Зняти кошти з депозиту");
                    Console.WriteLine("6 - Відкрити кредитний рахунок");
                    Console.WriteLine("7 - Повернутись до головного меню");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);
                    switch (_str)
                    {
                        case 1:
                            {
                                Console.Clear();
                                CheckBalance(bankAccount);
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                Console.Write("Введіть суму, на яку хочете поповнити рахунок: ");
                                string _putMoney = Console.ReadLine(); double putMoney = double.Parse(_putMoney);
                                bankAccount.AddCurrencyToAccount(putMoney);
                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                Console.Write("Введіть суму, яку хочете отримати: ");
                                string _moneyToSpend = Console.ReadLine(); double moneyToSpend = double.Parse(_moneyToSpend);
                                bankAccount.SpendAccountMoney(moneyToSpend);
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                Console.Write("Введіть суму, на яку хочете поповнити депозитний рахунок: ");
                                string _transferedMoney = Console.ReadLine(); double transferedMoney = double.Parse(_transferedMoney);
                                bankAccount.TransferToDeposit(transferedMoney);
                                break;
                            }
                        case 5:
                            {
                                Console.Clear();
                                Console.Write("Введіть суму, яку хочете зняти з депозитного рахунок: ");
                                string _transferedMoney = Console.ReadLine(); double transferedMoney = double.Parse(_transferedMoney);
                                bankAccount.TransferfromDeposit(transferedMoney);
                                break;
                            }
                        case 6:
                            {
                                Console.Clear();
                                Console.Write("Введіть назву нової кредитної картки: ");
                                string creditCard = Console.ReadLine();
                                Console.Write("Введіть суму, яку хочете отримати: ");
                                string _creditMoney = Console.ReadLine(); double creditMoney = double.Parse(_creditMoney);
                                bankAccount.GetCredit(creditCard, creditMoney);
                                break;
                            }
                        case 7:
                            {
                                Console.Clear();
                                MainMenu();
                                break;
                            }

                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до WORK MENU");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine($@"Exception: {e.Message}");
                    Console.Write("\nНатисніть ENTER щоб повернутись до WORK MENU");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        #endregion
    }
}

