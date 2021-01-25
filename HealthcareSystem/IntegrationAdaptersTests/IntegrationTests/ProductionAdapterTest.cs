// using Backend.Model.Pharmacies;
// using IntegrationAdapters.Adapters;
// using IntegrationAdapters.Adapters.Production;
// using System;
// using System.IO;
// using System.Net.Http;
// using Xunit;

// namespace IntegrationAdaptersTests.IntegrationTests
// {
//     public class ProductionAdapterTest
//     {
//         private PharmacySystemAdapter_Id1 _adapter;
//         private bool _isaRunning = false;

//         public ProductionAdapterTest()
//         {
//             _adapter = new PharmacySystemAdapter_Id1();
//         }

//         ~ProductionAdapterTest()
//         {
//             try
//             {
//                 Directory.Delete("Resources", true);
//             }
//             catch(Exception e)
//             {
//                 Console.WriteLine(e);
//             }
//         }

//         [Fact]
//         public void DrugAvailibility_Returns_Searched_Drugs_Valid_Endpoint()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8080", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());

//             if (_isaRunning)
//             {
//                 var ret = _adapter.DrugAvailibility("brufen");
//                 Assert.True(ret.Count == 5);
//             }
//         }

//         [Fact]
//         public void DrugAvailibility_Returns_Searched_Drugs_Invalid_Endpoint()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8090", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());

//             if (_isaRunning) 
//             {
//                 var ret = _adapter.DrugAvailibility("brufen");
//                 Assert.True(ret.Count == 0);
//             }
//         }

//         [Fact]
//         public void GetAllDrugs_Valid_Endpoint()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8080", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());

//             if (_isaRunning)
//             {
//                 var ret = _adapter.GetAllDrugs();
//                 Assert.True(ret.Count == 7);
//             }
//         }

//         [Fact]
//         public void GetAllDrugs_Invalid_Endpoint()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8090", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());

//             if (_isaRunning)
//             {
//                 var ret = _adapter.GetAllDrugs();
//                 Assert.True(ret.Count == 0);
//             }
//         }

//         [Fact]
//         public void GetDrugSpecifications_Valid_Specification()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8080", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());
//             System.IO.Directory.CreateDirectory("Resources");

//             if (_isaRunning)
//             {
//                 var ret = _adapter.GetDrugSpecifications(1);
//                 Assert.True(ret && System.IO.File.Exists("Resources/response1.txt"));
//             }
//         }

//         [Fact]
//         public void GetDrugSpecifications_Invalid_Specification()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8080", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());
//             System.IO.Directory.CreateDirectory("Resources");

//             if (_isaRunning)
//             {
//                 var ret = _adapter.GetDrugSpecifications(99);
//                 Assert.False(ret);
//             }
//         }

//         [Fact]
//         public void SendDrugConsumptionReport_Valid_Endpoint()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8080", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());
//             System.IO.Directory.CreateDirectory("Resources");
//             using (StreamWriter sw = File.CreateText("Resources/report.txt"))
//             {
//                 sw.WriteLine("report test");
//             }

//             if (_isaRunning)
//             {
//                 var ret = _adapter.SendDrugConsumptionReport("Resources", "report.txt");
//                 Assert.True(ret);
//             }
//         }

//         [Fact]
//         public void OrderDrugs_Valid_Drug()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8080", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());

//             if (_isaRunning)
//             {
//                 var ret = _adapter.OrderDrugs(2, 1, 1);
//                 Assert.True(ret);
//             }
//         }

//         [Fact]
//         public void OrderDrugs_Invalid_Drug()
//         {
//             SftpConfig sftpConfig = new SftpConfig() { Host = "192.168.1.4", Port = 22, Password = "tester", Username = "password" };
//             PharmacySystemAdapterParameters parameters = new PharmacySystemAdapterParameters() { ApiKey = "api", GrpcHost = "localhost", GrpcPort = 9090, Url = "http://localhost:8080", HospitalName = "HealthcareSystem-ORG4", SftpConfig = sftpConfig };
//             _adapter.Initialize(parameters, new HttpClient());

//             if (_isaRunning)
//             {
//                 var ret = _adapter.OrderDrugs(1, 99, 1);
//                 Assert.False(ret);
//             }
//         }
//     }
// }
