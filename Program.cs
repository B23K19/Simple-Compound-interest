using System;
using System.Globalization;

class Program
{
    public static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-ZA");

        Console.WriteLine("__________ A Simple Compound Interest Calculator __________\n");

        Console.Write("Enter the initial principal amount: ");
        double principal = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the interest rate value: ");
        double rate = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("What is the offered interest rate:");
        Console.WriteLine("1. Annual");
        Console.WriteLine("2. Semi-Annual");
        Console.WriteLine("3. Quarterly");
        Console.WriteLine("4. Monthly");
        Console.Write("Select an option (1-4): ");
        int interestChoice = Convert.ToInt32(Console.ReadLine());

        switch (interestChoice)
        {
            case 1: 
                break;
            case 2: rate *= 2;
                break;
            case 3: rate *= 4;
                break;
            case 4: rate *= 12;
                break;
            default:
                Console.WriteLine("Enter correct interest rate type.");
                return;
        }


        Console.WriteLine("Will you be making regular contributions?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No (One-time investment only)");
        Console.Write("Select an option (1 or 2): ");
        int contributeChoice = Convert.ToInt32(Console.ReadLine());

        double contribution = 0;
        int injectPerYear = 1;
        bool makeContributions = false;

        if (contributeChoice == 1)
        {
            makeContributions = true;

            Console.Write("Enter your contribution amount: ");
            contribution = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("How often do you contribute?");
            Console.WriteLine("1. Monthly");
            Console.WriteLine("2. Quarterly");
            Console.WriteLine("3. Annually");
            Console.Write("Select an option (1 - 3): ");
            int contributionCountChoice = Convert.ToInt32(Console.ReadLine());

            switch (contributionCountChoice)
            {
                case 1:
                    injectPerYear = 12;
                    break;
                case 2:
                    injectPerYear = 4;
                    break;
                case 3:
                    injectPerYear = 1;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
                    return;
            }
           
        }

        Console.Write("Enter the number of years: ");
        int years = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("How frequently is the interest compounded?");
        Console.WriteLine("1. Daily");
        Console.WriteLine("2. Monthly");
        Console.WriteLine("3. Quarterly");
        Console.WriteLine("4. Semi-Annually");
        Console.WriteLine("5. Annually");
        Console.Write("Select an option (1-5): ");
        int compoundChoice = Convert.ToInt32(Console.ReadLine());
        int compoundPerYear;
                switch (compoundChoice)
                {
                    case 1:
                        compoundPerYear = 365;
                        break;
                    case 2:
                        compoundPerYear = 12;
                        break;
                    case 3:
                        compoundPerYear = 4;
                        break;
                    case 4:
                        compoundPerYear = 2;
                        break;
                    case 5:
                        compoundPerYear = 1;
                        break;
                    default:
                        Console.WriteLine("Error:please select the option provided");
                        return;
                }

        Console.Write("Enter the average annual inflation rate in percentages(%): ");
        double inflationRate = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter tax on interest earnings in percentages(%): ");
        double taxRate = Convert.ToDouble(Console.ReadLine());

        double periodRate = rate / 100 / compoundPerYear;
        double inflationPerPeriod = inflationRate / 100 / compoundPerYear;
        double taxFraction = taxRate / 100;
        double balance = principal;
        double totalContributions = 0;
        double totalInterestEarned = 0;
        double inflationAdjustedBalance = 0;

        int totalPeriods = years * compoundPerYear;
        int contributionPeriodCounter = 0;
        int contributionInterval = compoundPerYear / injectPerYear;

        for (int i = 1; i <= totalPeriods; i++)
        {
            double interestEarned = balance * periodRate;
            double tax = interestEarned * taxFraction;
            double netInterest = interestEarned - tax;

            balance += netInterest;
            totalInterestEarned += netInterest;

            contributionPeriodCounter++;
            if (makeContributions && contributionPeriodCounter == contributionInterval)
            {
                balance += contribution;
                totalContributions += contribution;
                contributionPeriodCounter = 0;
            }
            inflationAdjustedBalance = balance / Math.Pow(1 + inflationPerPeriod, i);
        }

        Console.WriteLine("\n____________ Final Report ___________\n");
        Console.WriteLine($"Total Years: {years}");
        Console.WriteLine($"Total Contributions: {totalContributions:C2}");
        Console.WriteLine($"Total Interest Earned after tax: {totalInterestEarned:C2}");
        Console.WriteLine($"Final Balance: {balance:C2}");
        Console.WriteLine($"Inflation-Adjusted Balance: {inflationAdjustedBalance:C2} \n");
    }
}
