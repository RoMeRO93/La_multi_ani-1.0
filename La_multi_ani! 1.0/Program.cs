using System;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Bine ati venit! Va rugam sa introduceti data dumneavoastra de nastere:");

        DateTime birthDate;
        while (true)
        {
            try
            {
                birthDate = ReadBirthDateFromConsole();
                break;
            }
            catch (FormatException)
            {
                Console.WriteLine("Data introdusa nu este intr-un format valid. Incercati din nou.");
            }
            catch (FutureBirthDateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("A aparut o eroare: " + ex.Message);
            }
        }

        DateTime currentDate = DateTime.Now;
        int age = CalculateAge(birthDate, currentDate);

        Console.WriteLine("Ati implinit " + age + " de ani!");

        if (age == 30)
        {
            Console.WriteLine("La multi ani!");
        }
        else if (age < 30)
        {
            int yearsLeft = 30 - age;
            Console.WriteLine("Mai aveti de asteptat " + yearsLeft + " ani!");
        }
        else
        {
            Console.WriteLine("Ati trecut de 30 de ani!");
        }
    }

    private static DateTime ReadBirthDateFromConsole()
    {
        Console.Write("Introduceti data de nastere (format: dd/MM/yyyy sau MM/dd/yyyy): ");
        string input = Console.ReadLine();

        string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy" };
        DateTime birthDate;

        if (DateTime.TryParseExact(input, formats, null, System.Globalization.DateTimeStyles.None, out birthDate))
        {
            if (birthDate > DateTime.Now)
            {
                throw new FutureBirthDateException("Data de nastere trebuie să fie anterioara datei curente.");
            }
            return birthDate;
        }
        else
        {
            throw new FormatException();
        }
    }

    private static int CalculateAge(DateTime birthDate, DateTime currentDate)
    {
        int age = currentDate.Year - birthDate.Year;

        if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
        {
            age--;
        }

        return age;
    }
}

internal class FutureBirthDateException : Exception
{
    public FutureBirthDateException(string message) : base(message)
    {
    }
}
