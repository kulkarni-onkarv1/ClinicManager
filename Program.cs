// See https://aka.ms/new-console-template for more information
using CS_DoctorVolumeMiniProj;
using CS_DoctorVolumeMiniProj.DataAccess;
using CS_DoctorVolumeMiniProj.Models2;
using CS_DoctorVolumeMiniProj.DataAccess.NewFolder; 
using System.Text.Json;
using System.Text.RegularExpressions;

class Program
{
    static int choice = 0;
    static string PatientName=String.Empty;
    static string DoctorRegNo = "MD429P";
    static string PatientAddress=String.Empty;
    static string PatientIdcardNumber=String.Empty;
    static int IdChoice = 0;
    static DateTime? NextAppointmentDate;
    static string HDL=String.Empty;
    static string LDL=String.Empty;
    static decimal Weight;
    static decimal FixedFees = 500;
    static decimal VaryFees = 425;
    static decimal FeesAppliedToPatient;
    static string Diease=String.Empty;
    static string Discription=String.Empty;
    static string Medicine=String.Empty;
    static string Date=String.Empty;
    static bool IsNotValidPatientName = true;
    static bool IsNotValidPatientWeight = true;
    //static int PatientID = 0;

    public static void Main()
    {
        Console.WriteLine("Greetings!Welcome On The Board!");
        do
        {
            try
            {
                Console.WriteLine("\nEnter 1 To Add New Out Door Patient\nEnter 2 To Get Patient Registered Information By Patient ID\nEnter 3 To Get Visited Patient Dieases Record" +
                    "\nEnter 4 To Enter Registered Patient Diagonisis\nEnter 5 To Note Down Prescribed Medicines For The Patient\nEnter 6 To See Medicines Prescribed For Treated Patients" +
                    "\nEnter 7 To Note Down Refered Lab Tests For Patient\nEnter 8 To See Refered Lab Tests For Patient\nEnter 9 To Search Patient Registered Information By Name" +
                    "\nEnter 10 To Get DateWise Daily Collection Report\nEnter 11 To Clear The Screen" +
                    "\nEnter 12 To Quit");
                choice = int.Parse(Console.ReadLine());

                PatientDataManagement patientDataManagement = new PatientDataManagement();
                PatientDieasesRecord patientDieasesRecord= new PatientDieasesRecord();
                PrintDetails printDetails= new PrintDetails();
                LaboratoryRecords laboratoryRecords= new LaboratoryRecords();
                DailyCollectionReport dailyCollectionReport= new DailyCollectionReport();
                if (choice == 1)
                {
                    Console.WriteLine("Enter Patient Name");
                    string strRegexx4 = "^[a-zA-Z\\s]+$";
                    String validationForName=String.Empty;
                    do {
                        IsNotValidPatientName = true;
                        validationForName = Console.ReadLine();
                        Regex re = new Regex(strRegexx4);
                        if (re.IsMatch(validationForName))
                        {
                            IsNotValidPatientName = false;
                            PatientName = validationForName;
                        }
                        else
                        {
                            Console.WriteLine("Patient Name Cannot Have Number and special Characters");
                            Console.WriteLine("Enter Patient Name Again");
                        }
                    }while (IsNotValidPatientName);
                    
                    Console.WriteLine("Enter Patient Address(At Least Village/Town/City");
                    PatientAddress = Console.ReadLine();
                    Console.WriteLine("Enter Patient's ID Card Number:");
                    PatientIdcardNumber = Console.ReadLine();
                    Console.WriteLine("Creating new Record");
                    var ODpatientNew = new OutDoorPatient()
                    {
                        DoctorRegNo = DoctorRegNo,
                        PatientName = PatientName,
                        PatientAddress = PatientAddress,
                        PatientIdcardNumber = PatientIdcardNumber,
                    };
                    Console.WriteLine("Passing");
                    var result = patientDataManagement.CreateAsync(ODpatientNew).Result;
                    // Console.WriteLine(result.PatientId);

                    if (result == null)
                    {
                        Console.WriteLine("Insertion Failed");
                    }
                    else
                    {
                        var patientAutoIncrID = result.PatientId;
                        Console.WriteLine($"Patient's Auto Generated ID: {patientAutoIncrID}. Note This Down For Further Insertions");
                    }
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Note Here Above Patients Diagonisis");
                    Console.WriteLine("\nEnter Patient's ID Whose Diagonisis You Want To Note Down:");
                    choice = int.Parse(Console.ReadLine());
                    var resultantPatientID = patientDataManagement.GetByIdAsync(choice).Result;
                    if (resultantPatientID == null)
                    {
                        Console.WriteLine("No Patient Found For PatientID:{0}", choice);
                        Console.WriteLine("If You Are Not Able To Find PatientID, Then Search Patient By Its Name From 2nd Menu To Find PatientID");
                    }
                    else
                    {
                        Console.WriteLine("Enter Patient's Recognised Diease");
                        Diease = Console.ReadLine();
                        Console.WriteLine("Enter Recognised Diease Description if Any.Else Enter To Continue");
                        Discription = Console.ReadLine();
                        Console.WriteLine("Enter Yes To Have A Next Appointment Date To Patient, If Any.Else Enter Any Key To Proceed Further");
                        string appointMentDateChoice = Console.ReadLine().ToLower();
                        if (appointMentDateChoice == "yes")
                        {
                            Console.WriteLine("Enter Date As DD-MM-YYYY Format. Ex. 01-03-2000 For 1st March 2000");
                            NextAppointmentDate = Convert.ToDateTime(Console.ReadLine());
                        }
                        else
                        {
                            NextAppointmentDate = null;
                        }
                        Console.WriteLine("Enter Patient's Measured HDL");
                        HDL = Console.ReadLine();
                        Console.WriteLine("Enter Patient's Measured LDL");
                        LDL = Console.ReadLine();
                        do
                        {
                            IsNotValidPatientWeight = true;
                            Console.WriteLine("Enter Patient's Measured Weight In KiloGram");
                            Weight = decimal.Parse(Console.ReadLine());
                            if(Weight <= 0)
                            {
                                Console.WriteLine("Oh Oh! Weight Cannot Be Zero Or Less Than Zero");
                            }
                            else
                            {
                                IsNotValidPatientWeight = false;
                            }
                        } while (IsNotValidPatientWeight);
                        Console.WriteLine("Has Patient Come Second Time In The Interval Of Past 15 Days From Last Treated Day?");
                        Console.WriteLine("If Yes Then Enter Yes.Else Enter Enter Any Key To Proceed Further");
                        String VisitCheck=Console.ReadLine().ToLower();
                        if(VisitCheck == "yes")
                        {
                            Console.WriteLine("Re-Visit Fees Is Being Applied To Patient");
                            FeesAppliedToPatient = VaryFees;
                        }
                        else
                        {
                            Console.WriteLine("First Visit Fees Is Being Applied To Patient");
                            FeesAppliedToPatient = FixedFees; 
                        }
                        var patientMedicalDiagonisis = new PatientDiease()
                        {
                            Diease = Diease,
                            Description = Discription,
                            NextAppointmentDate = NextAppointmentDate,
                            Hdl = HDL,
                            Ldl = LDL,
                            Weight = Weight,
                            Fees=FeesAppliedToPatient,
                            PatientId = resultantPatientID.PatientId,
                        };
                        var patientDiesess = patientDieasesRecord.patientDieasesRecordInsertionAsync(patientMedicalDiagonisis);
                        if (patientDiesess.Result != null)
                        {
                            Console.WriteLine("Record Inserted Successfully");                           
                        }
                        else
                        {
                            Console.WriteLine("Insertion Failed");
                        }
                    }

                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter Patient ID Whose Personal Information You Want!");
                    IdChoice = int.Parse(Console.ReadLine());
                    var result = printDetails.FetchingRecordsAsync(IdChoice).Result;
                    if (result == 0)
                    {
                        Console.WriteLine($"No Record Found For Patient ID:{IdChoice}");
                    }
                }
                else if (choice == 9)
                {
                    Console.WriteLine("Enter Patient Name");
                    String PName = Console.ReadLine();
                    var result = printDetails.FetchingRecordsByPatientNameAsync(PName).Result;
                    if (result == 0)
                    {
                        Console.WriteLine($"No Record Found For Patient Name:{PName}");
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Enter Patient ID Mentioned  Whose Record You Want!");
                    IdChoice = int.Parse(Console.ReadLine());
                    var resultantPatientID = patientDataManagement.GetByIdAsync(IdChoice).Result;
                    if (resultantPatientID == null)
                    {
                        Console.WriteLine("No Patient Found For PatientID:{0}", choice);
                        Console.WriteLine("If You Are Not Able To Find PatientID, Then Search Patient By Its Name From 2nd Menu To Find PatientID");
                    }
                    else
                    {
                        var result = patientDataManagement.GetPatientDieasesDiagonisis(IdChoice).Result;
                        if (result == 0)
                        {
                            Console.WriteLine("No Diease Diagonisis Record Exists For Patient: {0}", resultantPatientID.PatientId);
                        }
                    }
                }
                else if (choice == 5)
                {
                    String choice = String.Empty;
                    int IdChoiCe = 0;
                    // Console.WriteLine("Enter Medicine Name");                                
                    Console.WriteLine("\nEnter Patient's ID For Whom You Want To Note Down Prescribed Medicines:");
                    IdChoiCe = int.Parse(Console.ReadLine());
                    var resultantPatientID = patientDataManagement.GetByIdAsync(IdChoiCe).Result;
                    if (resultantPatientID == null)
                    {
                        Console.WriteLine("No Patient Found For PatientID:{0}", choice);
                        Console.WriteLine("If You Are Not Able To Find PatientID, Then Search Patient By Its Name From 2nd Menu To Find PatientID");
                    }
                    else
                    {
                        do
                        {
                            Console.WriteLine("Enter Medicine Name:");
                            Medicine = Console.ReadLine();
                            var PatientMedicines = new PatientPrescribedMedicine()
                            {
                                MedicineName = Medicine,
                                PatientId = resultantPatientID.PatientId,
                            };
                            //string medicalTests = Console.ReadLine();
                            //LaboratoryRecords.PatientRefLabTests(result.PatientId, result.DoctorRegNo);
                            var patientMedines = patientDieasesRecord.patientMedicinessRecordInsertionAsync(PatientMedicines);
                            if (PatientMedicines == null)
                            {
                                Console.WriteLine("Insertion Failed");
                            }
                            else
                            {
                                Console.WriteLine($"Medicine Noted Down For PatientID:{resultantPatientID.PatientId} Successfully");
                                Console.WriteLine("More Medicines To Add? Enter Yes Else Press Enter To Proceed Further");
                                choice = Console.ReadLine().ToLower();
                            }
                        } while (choice == "yes");
                    }

                }
                else if (choice == 6)
                {
                    int IdChoiCe = 0;
                    Console.WriteLine("\nEnter Patient's ID For Whose Prescribed Medicines You Want To See:");
                    IdChoiCe = int.Parse(Console.ReadLine());
                    var resultantPatientID = patientDataManagement.GetByIdAsync(IdChoiCe).Result;
                    if (resultantPatientID == null)
                    {
                        Console.WriteLine("No Patient Found For PatientID:{0}", IdChoiCe);
                        Console.WriteLine("If You Are Not Able To Find PatientID, Then Search Patient By Its Name From 2nd Menu To Find PatientID");
                    }
                    else
                    {
                        var result = patientDataManagement.GetPatientPrescribedMedicinesDiary(resultantPatientID.PatientId);
                        if (result == 0)
                        {
                            Console.WriteLine("No Medicines Had Been Prescribed For PatientID: {0}", resultantPatientID.PatientId);
                        }
                    }
                }
                else if (choice == 7)
                {

                    int IdChoiCe = 0;
                    Console.WriteLine("\nEnter Patient's ID For Whom You Want To Note Down Refered Lab Tests");
                    IdChoiCe = int.Parse(Console.ReadLine());
                    var resultantPatientID = patientDataManagement.GetByIdAsync(IdChoiCe).Result;
                    if (resultantPatientID == null)
                    {
                        Console.WriteLine("No Patient Found For PatientID:{0}", choice);
                        Console.WriteLine("If You Are Not Able To Find PatientID, Then Search Patient By Its Name From 2nd Menu To Find PatientID");
                    }
                    else
                    {
                        LaboratoryRecords.PatientRefLabTests(resultantPatientID.PatientId, DoctorRegNo);
                    }
                }
                else if (choice == 8)
                {
                    int IdChoiCe = 0;
                    Console.WriteLine("\nEnter Patient's ID Whose Prescribed Lab Tetsts You Want To See:");
                    IdChoiCe = int.Parse(Console.ReadLine());
                    var resultantPatientID = patientDataManagement.GetByIdAsync(IdChoiCe).Result;
                    if (resultantPatientID == null)
                    {
                        Console.WriteLine("No Patient Found For PatientID:{0}", IdChoiCe);
                        Console.WriteLine("If You Are Not Able To Find PatientID, Then Search Patient By Its Name From 2nd Menu To Find PatientID");
                    }
                    else
                    {
                        var result = laboratoryRecords.GetPatientReferedLabtestsAsync(resultantPatientID.PatientId).Result;
                        if (result == 0)
                        {
                            Console.WriteLine("No Refered Lab Tests For PatientID :{0}", resultantPatientID.PatientId);
                        }
                    }
                }
                else if (choice == 10)
                {
                    Console.WriteLine("Enter The Date Of Which You Want Entire Collection Of That Day.");
                    Console.WriteLine("Enter The Date In Format Of YYYY-MM-DD. For Ex. For 1st March 2022, Enter 2022-03-01");
                    Date=Console.ReadLine();
                   var result= dailyCollectionReport.GetDateWiseDailyCollectionReport(Date).Result;
                    if(result == 0)
                    {
                        Console.WriteLine("Either Date Was Not Entered Mentioned Format Or No Entries Had Been Recorded For Specified Date:");
                        Console.WriteLine("Please Check Once!");
                    }
                }
                else if(choice == 11)
                {
                    Console.Clear();
                }
                else if (choice == 12)
                {
                    Console.WriteLine("Have A Nice Time!!! See You Soon On The Board!!!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid Choice!");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }while (true);
    }
}
