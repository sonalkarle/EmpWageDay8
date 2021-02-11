using System;
using System.Collections.Generic;

namespace EmpWageDay8
{
    class Company
    {
        public string companyName;
        public int empWagePerHr = 20;
        public int fullTimePerDay = 8;
        public int partTimePerDay = 4;
        public int MAX_WORKING_HRS = 100;
        public int MAX_WORKING_DAYS = 20;
        internal float TotalWage;

        public Company(String companyName, int empWagePerHr, int fullTimePerDay, int partTimePerDay, int MAX_WORKING_HRS, int MAX_WORKING_DAYS)
        {
            this.companyName = companyName;
            this.empWagePerHr = empWagePerHr;
            this.fullTimePerDay = fullTimePerDay;
            this.partTimePerDay = partTimePerDay;
            this.MAX_WORKING_HRS = MAX_WORKING_HRS;
            this.MAX_WORKING_DAYS = MAX_WORKING_DAYS;
            
        }
    }

    class EmployeeWageComputation
    {
        private const int IS_FULL_TIME = 1;
        private const int IS_PART_TIME = 2;
        private const int IS_ABSENT = 0;
        float EmpDailyWage = 0;
        private float TotalWage = 0;
        private Dictionary<String, Company> Companies = new Dictionary<String, Company>();

        private void AddCompany(String companyName, int empWagePerHr, int fullTimePerDay, int partTimePerDay, int MAX_WORKING_HRS, int MAX_WORKING_DAYS)
        {
            Company company = new Company(companyName.ToLower(), empWagePerHr, fullTimePerDay, partTimePerDay, MAX_WORKING_HRS, MAX_WORKING_DAYS);
            Companies.Add(companyName.ToLower(), company);
        }

        private int IsEmployeePresent()
        {
            return new Random().Next(0, 3);
        }

        public void CalculateEmpWage(string CompanyName)
        {

            int DayNumber = 1;
            int EmpWorkinghHrs = 0;
            int TotalWorkingHrs = 0;
            try
            {
                if (!Companies.ContainsKey(CompanyName.ToLower()))
                    throw new ArgumentNullException("company don't exist");
                Companies.TryGetValue(CompanyName.ToLower(), out Company company);

                while (DayNumber <= company.MAX_WORKING_DAYS && TotalWorkingHrs <= company.MAX_WORKING_HRS)
                {
                    switch (IsEmployeePresent())
                    {
                        case IS_ABSENT:
                            EmpWorkinghHrs = 0;
                            break;
                        case IS_PART_TIME:
                            EmpWorkinghHrs = company.partTimePerDay;
                            break;
                        case IS_FULL_TIME:
                            EmpWorkinghHrs = company.fullTimePerDay;
                            break;
                    }
                    EmpDailyWage = EmpWorkinghHrs * company.empWagePerHr;
                    TotalWage += EmpDailyWage;
                    DayNumber++;
                    TotalWorkingHrs += EmpWorkinghHrs;
                }
                company.TotalWage = TotalWage;
                Console.WriteLine("\nCompany name: " + CompanyName);
                Console.WriteLine("Total working days :" + (DayNumber - 1) + "\nTotal working hours :" + TotalWorkingHrs + "\nTotal employee wage :" + company.TotalWage);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Wage Computation");

            EmployeeWageComputation employeeWageComputation = new EmployeeWageComputation();
            employeeWageComputation.AddCompany("TATA", 20, 8, 4, 100, 20);
            employeeWageComputation.AddCompany("Mahindra", 30, 8, 4, 100, 20);
            employeeWageComputation.CalculateEmpWage("tata");
            employeeWageComputation.CalculateEmpWage("Mahindra");
        }
    }
}