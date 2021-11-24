using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BankAccount : IAccount
    {
        public int AccountName { get; set; }
        public double AccountBalance { get; set; }
        public double AccountDepositBalance { get; set; }

        #region bank
        Bank bank = new Bank();
        public double GetMoneyToRepay() { return bank.CreditToRepay; }
        public void RepayCredit() { bank.CreditCard = string.Empty; bank.CreditToRepay = 0; }
        public void OverdraftRepay() { bank.CreditToRepay -= AccountBalance; }
        #endregion

        public BankAccount(int account)
        {
            if (InputProtection.ProtectedIntegers(account, 999999, 6))
            {
                AccountName = account;
                AccountBalance = 0;
                AccountDepositBalance = 0;
            }
            else throw new Exception("Перевірте коректність вводу даних!");
        }

        #region service methods
        public void AddCurrencyToAccount(double currency)
        {
            if (currency <= 0) throw new Exception("Неможлива операція!");
            AccountBalance += currency;
            if (bank.CreditCard != string.Empty)
            {
                Overdraft?.Invoke(this, new OverdraftEvent(bank.CreditToRepay));
            }
        }
        public void SpendAccountMoney(double moneyToSpend)
        {
            if (moneyToSpend <= 0) throw new Exception("Неможлива операція!");
            if (moneyToSpend <= AccountBalance)
            {
                AccountBalance -= moneyToSpend;
            }
            else throw new Exception("Неможлива операція! Сума перевищує баланс акаунта.");
        }
        public void TransferToDeposit(double trasferedMoney)
        {
            if (trasferedMoney <= 0) throw new Exception("Неможлива операція!");
            if (AccountBalance > 0 && trasferedMoney <= AccountBalance)
            {
                AccountBalance -= trasferedMoney;
                AccountDepositBalance += trasferedMoney;
            }
            else throw new Exception("Неможлива операція! Не вистачає коштів на рахунку.");
        }
        public void TransferfromDeposit(double trasferedMoney)
        {
            if (trasferedMoney <= 0) throw new Exception("Неможлива операція!");
            if (AccountDepositBalance > 0 && trasferedMoney <= AccountDepositBalance)
            {
                AccountDepositBalance -= trasferedMoney;
                AccountBalance += trasferedMoney;
            }
            else throw new Exception("Неможлива операція! Сума переводу перевищує баланс.");
        }
        #endregion

        public event EventHandler<OverdraftEvent> Overdraft;
        public bool GetCredit(string creditCard, double creditMoney)
        {
            if (bank.CreditCard == string.Empty)
            {
                bank.CreditCard = creditCard;
                AccountBalance += creditMoney;
                bank.CreditToRepay = creditMoney + creditMoney * 0.11;
                return true;
            }
            else throw new Exception($@"Неможливо оформити кредит! Несплата по іншому кредитному рахунку |{bank.CreditCard}|"); ;
        }
    }
}
