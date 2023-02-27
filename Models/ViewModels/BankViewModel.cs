namespace BankWeb.Models.ViewModels
{
    public class BankViewModel
    {
        public BankViewModel(Bank bank) 
        {
            States = new(1<<3);
            double PL1 = bank.HighlyLiquidAssets * 100 / bank.InvolvedFunds;
            double H2 = bank.HighlyLiquidAssets * 100 / bank.DemandObligations;
            double H3 = bank.AssetsFor30Days * 100 / bank.CommitmentsFor30Days;
            double H4 = bank.AssetsMore1Year * 100 / (bank.Capital + bank.CommitmentsMore1Year);
            double PL4 = bank.DemandObligations * 100 / bank.InvolvedFunds;
            double PL6 = bank.IssuedBills * 100 / bank.Capital;
            double PAZ = bank.DelinquentLoans * 100 / bank.LoanDebt;
            double H1 = bank.Capital * 100 / bank.RiskAssets;

            States.Add((PL1 >= 30 ? 1 : PL1 >= 20 ? 2 : PL1 >= 10 ? 3 : 4, "ПЛ-1 (" + PL1.ToString() + ")"));
            States.Add((PL4 <= 25 ? 1 : PL4 <= 40 ? 2 : PL4 <= 50 ? 3 : 4, "ПЛ-4 (" + PL4.ToString() + ")"));
            States.Add((PL6 <= 45 ? 1 : PL6 <= 75 ? 2 : PL6 <= 90 ? 3 : 4, "ПЛ-6 (" + PL6.ToString() + ")"));
            States.Add((H1 >= 50 ? 1 : H1 >= 25 ? 2 : H1 >= 15 ? 3 : 4, "Н-1 (" + H1.ToString() + ")"));
            States.Add((H2 >= 70 ? 1 : H2 >= 50 ? 2 : H2 >= 25 ? 3 : 4, "Н-2 (" + H2.ToString() + ")"));
            States.Add((H3 >= 80 ? 1 : H3 >= 70 ? 2 : H3 >= 60 ? 3 : 4, "Н-3 (" + H3.ToString() + ")"));
            States.Add((H4 <= 60 ? 1 : H4 <= 90 ? 2 : H4 <= 110 ? 3 : 4, "Н-4 (" + H4.ToString() + ")"));
            States.Add((PAZ <= 10 ? 1 : PAZ <= 15 ? 2 : PAZ <= 25 ? 3 : 4, "ПАЗ (" + PAZ.ToString() + ")"));
        }
        public List<(int state, string name)> States { get; set; }
    }

    public static class EnumExtenstion
    {
        public static string ToImgFromState(this int state) => state switch
        {
            1 => "perfect.jpg",
            2 => "happy.jpg",
            3 => "normal.jpg",
            4 => "bad.jpg",
            5 => "terrible.jpg",
        };
    }
}
