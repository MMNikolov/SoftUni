using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private LoanRepository loans;
        private BankRepository banks;

        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }

        public string AddBank(string bankTypeName, string name)
        {
            try
            {
                Bank bank = CreateBankByTypeName(bankTypeName, name);
                banks.AddModel(bank);
                return $"{bankTypeName} is successfully added.";
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
        }

        public string AddClient(string bankName, string clientTypeName, string clientName,
            string id, double income)
        {
            throw new NotImplementedException();
        }

        public string AddLoan(string loanTypeName)
        {
            try
            {
                Loan loan = CreateLoanByTypeName(loanTypeName);
                loans.AddModel(loan);
                return $"{loanTypeName} is successfully added.";
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
        }

        public string FinalCalculation(string bankName)
        {
            throw new NotImplementedException();
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            return "ad";
        }

        public string Statistics()
        {
            throw new NotImplementedException();
        }

        private Bank CreateBankByTypeName(string bankTypeName, string name)
        {
            if (bankTypeName == "BranchBank")
            {
                return new BranchBank(name);
            }
            else if (bankTypeName == "CentralBank")
            {
                return new CentralBank(name);
            }
            else
            {
                throw new ArgumentException("Invalid bank type.");
            }
        }

        private Loan CreateLoanByTypeName(string loanTypeName)
        {
            
            if (loanTypeName == "StudentLoan")
            {
                return new StudentLoan();
            }
            else if (loanTypeName == "MortgageLoan")
            {
                return new MortgageLoan();
            }
            else
            {
                throw new ArgumentException("Invalid loan type.");
            }
        }

        
    }
}
