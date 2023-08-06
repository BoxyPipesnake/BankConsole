using BankConsole;
using System.Text.RegularExpressions;

if (args.Length == 0)
    EmailService.SendEmail();
else
    ShowMenu();

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("Selecciona una opcion:");
    Console.WriteLine("1 - Crear un usuario nuevo.");
    Console.WriteLine("2 - Eliminar un usuario existente.");
    Console.WriteLine("3 - Salir.");

    int option = 0;
    do
    {
        string input = Console.ReadLine();

        if (!int.TryParse(input, out option))
            Console.WriteLine("Debes Ingresar un numero (1, 2 o 3).");
        else if (option > 3)
            Console.WriteLine("Debes Ingresar un numero (1, 2 o 3).");

    }
    while (option == 0 || option > 3);

    switch (option)
    {
        case 1:
            CreateUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }

}

void CreateUser()
{
    Console.Clear();
    Console.WriteLine("Ingrese la informacion del usuario:");

    int ID;
    Console.Write("ID: ");
    do
    {
        string input = Console.ReadLine();

        if(!int.TryParse(input, out ID) || ID <= 0)
            Console.Write("No es un ID valido, intentalo nuevamente: ");
        else if(Storage.IsUserIDTaken(ID))
            Console.Write($"El ID {ID} ya ha sido tomado, elige otro: ");
        
    } while (ID <= 0 || Storage.IsUserIDTaken(ID));


    Console.Write("Nombre: ");
    string name = Console.ReadLine();



    string email;
    do
    {
        Console.Write("Email: ");
        email = Console.ReadLine();

        if (!validateEmail(email))
        {
            Console.WriteLine("El formato del correo electrónico no es valido. Ej: goku@gmail.com");
        }
        else
            break;

    } while (true);


    decimal balance;

    do
    {
        Console.Write("Saldo: ");
        string input = Console.ReadLine();

        if (decimal.TryParse(input, out balance) && balance >= 0)
        {
            break;
        }
        else
        {
            Console.WriteLine("El saldo debe ser un número positivo.");
        }
    } while (true);


    char userType;

    do
    {
        Console.Write("Escribe 'c' si el usuario es Cliente, 'e' si es Empleado: ");
        userType = Console.ReadKey().KeyChar;

        if (userType != 'c' && userType != 'e')
        {
            Console.WriteLine("\n******Intentalo nuevamente.******");
        }
    } while (userType != 'c' && userType != 'e');


    User newUser;

    if (userType.Equals('c'))
    {
        Console.Write("\n\nRegimen fiscal: ");
        char taxRegime = char.Parse(Console.ReadLine());

        newUser = new Client(ID, name, email, balance, taxRegime);
    }
    else
    {
        Console.Write("\n\nDepartamento: ");
        string department = Console.ReadLine();

        newUser = new Employee(ID, name, email, balance, department);
    }

    Storage.AddUser(newUser);

    Console.WriteLine("Usuario creado.");
    Thread.Sleep(2000);
    ShowMenu();
}

void DeleteUser()
{
    Console.Clear();

    int ID;
    bool idValid = false;

    do
    {
        Console.Write("Ingresa el ID del usuario a eliminar: ");
        if (int.TryParse(Console.ReadLine(), out ID) && ID > 0)
        {
            if (!Storage.IsUserIDTaken(ID))
            {
                Console.WriteLine("El ID no existe. Intentalo nuevamente");
            }
            else
            {
                idValid = true;
            }
        }
        else
        {
            Console.WriteLine("No es un ID valido, intentalo nuevamente");
        }
    } while (!idValid);

    string result = Storage.DeleteUser(ID);

    if (result.Equals("Success"))
    {
        Console.Write("Usuario eliminado.");
        Thread.Sleep(2000);
        ShowMenu();
    }

}


// Validations

bool validateEmail(string email)
{

    string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z]+\.[a-zA-Z]{2,}$";
    return Regex.IsMatch(email, pattern);
}