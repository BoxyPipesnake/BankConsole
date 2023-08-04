namespace BankConsole;

public class Client : User, IPerson
{

    public Client(){}


    public char TaxRegime { get; set; }

    public Client(int ID, string Name, string Email, decimal Balance, char TaxRegime) : base(ID, Name, Email, Balance)
    {
        this.TaxRegime = TaxRegime;
        SetBalance(Balance);
    }

    public override void SetBalance(decimal amount)
    {
        base.SetBalance(amount);

        if(TaxRegime.Equals('M'))
            Balance += (amount * 0.02m);

    }

    public override string showData()
    {
        return base.showData() + $", Regimen Fiscal: {this.TaxRegime}";
    }

    public string GetName()
    {
        throw new NotImplementedException();
    }

    public string GetCountry()
    {
        throw new NotImplementedException();
    }
}