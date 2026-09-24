using System;

namespace Assignment02OOP
{
    // =========================================================================
    // 1. SecurityPrivileges Enum (Flags for Full Permissions support)
    // =========================================================================
    [Flags]
    public enum SecurityPrivileges
    {
        Guest = 1,
        Developer = 2,
        Secretary = 4,
        DBA = 8,
        // Security Officer has full permissions (combination of all privileges)
        SecurityOfficer = Guest | Developer | Secretary | DBA
    }


    // =========================================================================
    // 2. HiringDate Class
    // =========================================================================
    public class HiringDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public HiringDate(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Day:D2}/{Month:D2}/{Year}";
        }
    }


    // =========================================================================
    // 3. Employee Class
    // =========================================================================
    public class Employee
    {
        private char gender;

        public int ID { get; set; }
        public string Name { get; set; }
        public SecurityPrivileges SecurityLevel { get; set; }
        public decimal Salary { get; set; }
        public HiringDate HireDate { get; set; }

        // Restricting Gender to 'M' or 'F' only
        public char Gender
        {
            get { return gender; }
            set
            {
                char upperChar = char.ToUpper(value);
                if (upperChar == 'M' || upperChar == 'F')
                {
                    gender = upperChar;
                }
                else
                {
                    gender = 'M'; // Default value if invalid input is provided
                }
            }
        }

        // Constructors
        public Employee()
        {
            HireDate = new HiringDate(1, 1, 2020);
        }

        public Employee(int id, string name, SecurityPrivileges securityLevel, decimal salary, HiringDate hireDate, char gender)
        {
            ID = id;
            Name = name;
            SecurityLevel = securityLevel;
            Salary = salary;
            HireDate = hireDate;
            Gender = gender;
        }

        // Display Employee Data in String Form using String.Format and Currency Format
        public override string ToString()
        {
            string formattedSalary = string.Format("{0:C}", Salary);
            string genderFull = (Gender == 'M') ? "Male" : "Female";

            return string.Format("ID: {0}, Name: {1}, Gender: {2}, Security Level: {3}, Salary: {4}, Hire Date: {5}",
                ID, Name, genderFull, SecurityLevel, formattedSalary, HireDate);
        }
    }


    // =========================================================================
    // MAIN PROGRAM (EMPLOYEE ARRAY & SAFE INPUT)
    // =========================================================================

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================================================================");
            Console.WriteLine("                   ASSIGNMENT 2 OOP - PART 01                            ");
            Console.WriteLine("=========================================================================");

            // Array of Employees with size 3
            Employee[] EmpArr = new Employee[3];

            // 1. DBA Employee
            EmpArr[0] = new Employee(101, "Mona Ahmed", SecurityPrivileges.DBA, 12000.50m, new HiringDate(15, 3, 2021), 'F');

            // 2. Guest Employee
            EmpArr[1] = new Employee(102, "Ali Hassan", SecurityPrivileges.Guest, 4500.00m, new HiringDate(1, 10, 2023), 'M');

            // 3. Security Officer Employee (Full Permissions)
            EmpArr[2] = new Employee(103, "Ahmed Mohamed", SecurityPrivileges.SecurityOfficer, 20000.00m, new HiringDate(5, 6, 2018), 'M');

            Console.WriteLine("\n--- Displaying Hardcoded Employees List ---");
            foreach (var emp in EmpArr)
            {
                Console.WriteLine(emp.ToString());
            }

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // Safe User Input Demonstration (No Runtime Errors Allowed)
            Console.WriteLine("--- Create New Employee (Safe User Input Test) ---");

            int id = ReadInt("Enter Employee ID: ");
            string name = ReadString("Enter Employee Name: ");
            char gender = ReadGender("Enter Gender (M/F): ");
            decimal salary = ReadDecimal("Enter Salary: ");

            Console.WriteLine("Enter Hiring Date:");
            int day = ReadInt("  Day: ");
            int month = ReadInt("  Month: ");
            int year = ReadInt("  Year: ");
            HiringDate hireDate = new HiringDate(day, month, year);

            SecurityPrivileges privilege = ReadSecurityPrivilege();

            Employee userEmp = new Employee(id, name, privilege, salary, hireDate, gender);

            Console.WriteLine("\n--- Newly Created Employee Details ---");
            Console.WriteLine(userEmp.ToString());

            Console.WriteLine("\n=========================================================================");
            Console.WriteLine("End of Assignment 2 OOP Solution");
            Console.WriteLine("=========================================================================");

            Console.ReadLine(); // Keeps console window open
        }

        // =========================================================================
        // HELPER METHODS FOR SAFE USER INPUT (PREVENTS RUNTIME ERRORS)
        // =========================================================================

        static int ReadInt(string prompt)
        {
            int val;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out val) || val <= 0)
            {
                Console.Write("Invalid input. Please enter a valid positive integer: ");
            }
            return val;
        }

        static decimal ReadDecimal(string prompt)
        {
            decimal val;
            Console.Write(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out val) || val < 0)
            {
                Console.Write("Invalid input. Please enter a valid positive decimal number: ");
            }
            return val;
        }

        static string ReadString(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Name cannot be empty. Please enter a valid name: ");
                input = Console.ReadLine();
            }
            return input;
        }

        static char ReadGender(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input) || (input.Trim().ToUpper() != "M" && input.Trim().ToUpper() != "F"))
            {
                Console.Write("Invalid gender. Please enter 'M' for Male or 'F' for Female: ");
                input = Console.ReadLine();
            }
            return input.Trim().ToUpper()[0];
        }

        static SecurityPrivileges ReadSecurityPrivilege()
        {
            Console.WriteLine("Select Security Privilege:");
            Console.WriteLine("1. Guest");
            Console.WriteLine("2. Developer");
            Console.WriteLine("3. Secretary");
            Console.WriteLine("4. DBA");
            Console.WriteLine("5. Security Officer (Full Permissions)");
            Console.Write("Choice (1-5): ");

            while (true)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": return SecurityPrivileges.Guest;
                    case "2": return SecurityPrivileges.Developer;
                    case "3": return SecurityPrivileges.Secretary;
                    case "4": return SecurityPrivileges.DBA;
                    case "5": return SecurityPrivileges.SecurityOfficer;
                    default:
                        Console.Write("Invalid choice. Please enter a number between 1 and 5: ");
                        break;
                }
            }
        }
    }
}