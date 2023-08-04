using Newtonsoft.Json;

namespace BankConsole;
public class User
{

    [JsonProperty]
    protected int ID { get; set; }
    [JsonProperty]
    protected string Name { get; set; }
    [JsonProperty]
    protected string Email { get; set; }
    [JsonProperty]
    protected decimal Balance { get; set; }
    [JsonProperty]
    protected DateTime RegisterDate { get; set; }


    public User() {}

    public User(int ID, string Name, string Email, decimal Balance)
    {
        this.ID = ID;
        this.Name = Name;
        this.Email = Email;
        this.RegisterDate = DateTime.Now;

    }

    public int GetID()
    {
        return ID;
    }

    public DateTime GetRegisterDate()
    {
        return RegisterDate;
    }

    public virtual void SetBalance(decimal amount)
    {
        decimal quantity = 0;

        if(amount < 0)
            quantity = 0;
        else    
            quantity = amount;
        
        this.Balance += quantity;
    }

    public virtual string showData()
    {
        return $"ID: {this.ID}, Nombre: {this.Name}, Email: {this.Email}, Balance: {this.Balance}, Register Date {this.RegisterDate.ToShortDateString()}";
    }
    public string showData(string InitialMessage)
    {
        return $"{InitialMessage} -> ID: {this.ID}, Nombre: {this.Name}, Email: {this.Email}, Balance: {this.Balance}, Register Date {this.RegisterDate}.";
    }

}   