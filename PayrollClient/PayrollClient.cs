using CsvHelper;
using Microsoft.Extensions.Logging;
using Payroll.API;

namespace Payroll.client

{
    internal class PayrollClient
    {


        internal static void Main(string[] args)
        {

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.PayRollClient", LogLevel.Debug);
                   
            });
            ILogger logger = loggerFactory.CreateLogger<PayrollClient>();


            if (args.Length != 2)
            {
                logger.LogInformation("Please provide the Input CSV FileName and  Output File Path",DateTime.Now);
                Console.WriteLine("Please provide the Input CSV FileName and  Output File Path");
                Console.ReadKey();
                
            }
            else {
                String inputFileName = args[0];
                String outputFilePath = args[1];
                var employeeList = GetEmployee(inputFileName);
                generateMonthlyPaySlip(employeeList,outputFilePath);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        private static IList<EmployeeDTO> GetEmployee(String inputFileName)
        {
            List<EmployeeDTO> employeeList=null;
            try
            {
                Console.WriteLine("Processing the Employee details file " + inputFileName + "for generating payslip");
                using (StreamReader sr = new StreamReader(inputFileName))
                {
                    var csvReader = new CsvReader(sr);
                    csvReader.Configuration.RegisterClassMap<EmployeeDTOMap>();
                    csvReader.Configuration.HasHeaderRecord = false;
                    employeeList = csvReader.GetRecords<EmployeeDTO>().ToList();
                    csvReader.ClearRecordCache();
                    sr.Close();
                    csvReader.Dispose();

                }
            }catch(FileNotFoundException exp)
            {
                Console.Write(exp.Message);
                

            }
            return employeeList;
        }
        private static void generateMonthlyPaySlip(IList<EmployeeDTO> employeeList,String outputFilePath)
        {
            IPayrollAPI PayRollAPI = new PayRollAPI();
           
            IList<SalaryDTO> payslipList = new List<SalaryDTO>();
            String outputFileName = outputFilePath + "\\Monthly_Salary_" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+".csv";

            try
            {
                using (StreamWriter file = new StreamWriter(outputFileName))

                {
                    foreach (var employee in employeeList)
                    {
                        var payslip = PayRollAPI.generatePaySlip(employee, PayType.MONTHLY);
                        payslipList.Add(payslip);


                    }
                    var csvWriter = new CsvWriter(file);
                    csvWriter.Configuration.RegisterClassMap<SalaryDTOMap>();
                    csvWriter.Configuration.HasHeaderRecord = false;
                    csvWriter.WriteRecords(payslipList);
                    csvWriter.Dispose();
                    file.Close();       

                }
            }catch(Exception exp)
            {
                Console.WriteLine("Unexpected error occured while generating the payslip", exp.Message);
            }
            Console.WriteLine("PaySlip got generated Successfully " + outputFileName);
           
        }

      
    }
    }
