using System.Globalization;

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace CsvHelperTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. CSV 파일에 쓰기
            List<Employee> datas = new List<Employee>
            {
                new Employee { Name = "강,감찬", Email = "kang@naver.com", PhoneNumber = "010-1111-2222", Age = 23 },
                new Employee { Name = "이순\"신", Email = "lee@daum.com", PhoneNumber = "010-3333-5555", Age = 47 },
                new Employee { Name = "홍길동", Email = "honggil@gmail.com\r\n", PhoneNumber = "010-6666-7777", Age = 36 }
            };

            using (var streamWriter = new StreamWriter("data.csv"))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(datas);
                }
            }

            // 2. CSV 파일 읽기
            using (var streamReader = new StreamReader("data.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    List<Employee> records = csvReader.GetRecords<Employee>().ToList();
                }
            }

            // 3. CSV 파일에 추가
            List<Employee> addDatas = new List<Employee>
            {
                new Employee { Name = "세종대왕", Email = "sejoing@gmail.com", PhoneNumber = "010-8888-9999", Age = 51 }
            };

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            csvConfiguration.HasHeaderRecord = false;

            using (var fileStream = File.Open("data.csv", FileMode.Append))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, csvConfiguration))
                    {
                        csvWriter.WriteRecords(addDatas);
                    }
                }
            }
        }

        public class Employee
        {
            [Name("이름")]
            public string? Name { get; set; }
            [Name("이메일")]
            public string? Email { get; set; }
            [Name("전화번호")]
            public string? PhoneNumber { get; set; }
            [Name("나이")]
            public int Age { get; set; }
        }
    }
}