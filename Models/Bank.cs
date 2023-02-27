namespace BankWeb.Models
{
    public class Bank
    {
        public Bank()
        {

        }
        public Bank(string name, double highlyLiquidAssets, double involvedFunds, double demandObligations, double assetsFor30Days, double commitmentsFor30Days, double capital, double assetsMore1Year, double commitmentsMore1Year, double issuedBills, double delinquentLoans, double loanDebt, double riskAssets)

        {
            Name = name;
            HighlyLiquidAssets = highlyLiquidAssets;
            InvolvedFunds = involvedFunds;
            DemandObligations = demandObligations;
            AssetsFor30Days = assetsFor30Days;
            CommitmentsFor30Days = commitmentsFor30Days;
            Capital = capital;
            AssetsMore1Year = assetsMore1Year;
            CommitmentsMore1Year = commitmentsMore1Year;
            IssuedBills = issuedBills;
            DelinquentLoans = delinquentLoans;
            LoanDebt = loanDebt;
            RiskAssets = riskAssets;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double HighlyLiquidAssets { get; set; } // Высоколиквидные активы
        public double InvolvedFunds { get; set; } // Привлеченные средства
        public double DemandObligations { get; set; } // Обязательства до востребования
        public double AssetsFor30Days { get; set; } // Активы до 30 дней
        public double CommitmentsFor30Days { get; set; } // Обязательства до 30 дней
        public double Capital { get; set; } // Капитал
        public double AssetsMore1Year { get; set; } // Активы свыше года
        public double CommitmentsMore1Year { get; set; } // Обязательства свыше года
        public double IssuedBills { get; set; } // Выпущенные векселя
        public double DelinquentLoans { get; set; } // Просроченные кредиты
        public double LoanDebt { get; set; } // Ссудная задолженность
        public double RiskAssets { get; set; } // Активы с учетом риска
    }
}
