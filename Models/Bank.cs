namespace BankWeb.Models
{
    public class Bank
    {
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
