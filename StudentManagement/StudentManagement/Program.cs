using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace StudentManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.ReadFromFile();
            program.RunProgram();
        }

        private List<Student> _listStudent;

        private Program() => _listStudent = new List<Student>();

        private void MenuProgram()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("| 1.Add Student                                 |");
            Console.WriteLine("| 2.Edit Infomation Student                     |");
            Console.WriteLine("| 3.Search Student                              |");
            Console.WriteLine("| 4.Display Student                             |");
            Console.WriteLine("| 5.Delete Student                              |");
            Console.WriteLine("| 6.Sort Student                                |");
            Console.WriteLine("| 7.End Program And Save File Students.dat      |");
            Console.WriteLine("-------------------------------------------------");
            Console.Write(" Please choose:");
        }

        /*
         *Function to execute 
         */
        private void ChooseFunction(int chooseFunction)
        {
            switch (chooseFunction)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    UpdateInforStudentById();
                    break;
                case 3:
                    SearchStudentByIdOrName();
                    break;
                case 4:
                    DisplayStudent();
                    break;
                case 5:
                    DeleteStudentById();
                    break;
                case 6:
                    SortStudent();
                    break;
                case 7:
                    Console.WriteLine("Program Exit");
                    break;
                default:
                    Console.WriteLine("This function is not available,please choose again");
                    break;
            }
        }

        /*
         * Client choose function 
         */
        private void RunProgram()
        {
            while (true)
            {
                MenuProgram();

                try
                {
                    int chooseFunction = int.Parse(Console.ReadLine());
                    ChooseFunction(chooseFunction);
                    if (chooseFunction == 7)
                    {
                        WriteToFile();
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Please Enter Number");
                }
                Console.WriteLine();
            }
        }

        /*
         * Regular expression
         */
        private bool IsMatchRegex(string content, string type)
        {
            bool result = false;
            switch (type)
            {
                case "name":
                    var regexName = new Regex(@"^([a-zA-Z]+\s{1}[a-zA-Z]{1,}|[a-zA-Z]+\s{1}[a-zA-Z]{2,}\s{1}[a-zA-Z]{1,})$");
                    result = regexName.IsMatch(content) ? true : false;
                    break;

                case "phone":
                    var regexPhone = new Regex(@"^([0-9]{10})+$");
                    result = regexPhone.IsMatch(content) ? true : false;
                    break;

                case "address":
                    var regexAddress = new Regex(@"^([a-zA-Z0-9\/]+\s{1}[a-zA-Z0-9]{1,}|[a-zA-Z0-9]+\s{1}[a-zA-Z0-9]{3,}\s{1}[a-zA-Z0-9]{1,}){1,}$");
                    result = regexAddress.IsMatch(content) ? true : false;
                    break;

                case "gender":
                    string gender = content.ToLower();
                    result = (gender == "male" || gender == "female") ? true : false;
                    break;
            }

            return result;
        }

        /*
         * Check Student ID exists
         */
        private bool IsCheckId(int id) => _listStudent.Any(student => student.Id == id) ? true : false;

        /*
         * Add Student
         */
        private void AddStudent()
        {
            int countStudent = 0;

            while (true)
            {
                Console.WriteLine("Leave Id Blank And Press Enter Back To Menu");

                Console.Write("Student ID: ");
                try
                {
                    string studentName, studentAddress, studentEmail, studentPhone, studentGender, studentDOB;
                    float studentMark = 0;
                    string addStudentId = Console.ReadLine();

                    if (!addStudentId.Equals(""))
                    {
                        int studentId = int.Parse(addStudentId);
                        if (!IsCheckId(studentId))
                        {
                            //Student Name
                            while (true)
                            {
                                Console.Write("Student Name: ");
                                studentName = Console.ReadLine().Trim();
                                if (IsMatchRegex(studentName, "name"))
                                    break;
                                Console.WriteLine("Plesae Enter Alphabet(Ex:Le Nguyen Toan)");
                            }

                            //Student Address
                            while (true)
                            {
                                Console.Write("Student Address: ");
                                studentAddress = Console.ReadLine().Trim();
                                if (IsMatchRegex(studentAddress, "address"))
                                    break;
                                Console.WriteLine("Address Format: 123 aaaa Or aaa aaa");
                            }

                            //Student Email
                            while (true)
                            {
                                try
                                {
                                    Console.Write("Student Email: ");
                                    studentEmail = Console.ReadLine().Trim();
                                    //Delete White Space in Email
                                    studentEmail = Regex.Replace(studentEmail, @"\s{1,}", "");
                                    MailAddress mail = new MailAddress(studentEmail);
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Incorrect email format");
                                }
                            }

                            //Student Phone                  
                            while (true)
                            {
                                Console.Write("Student Phone: ");
                                studentPhone = Console.ReadLine().Trim();
                                if (IsMatchRegex(studentPhone, "phone"))
                                    break;
                                Console.WriteLine("Plase enter numbers and limit 10 numbers");
                            }

                            //Student Gender                      
                            while (true)
                            {
                                Console.Write("Student Gender(Male/Female): ");
                                studentGender = Console.ReadLine().Trim();
                                if (IsMatchRegex(studentGender, "gender"))
                                    break;
                                Console.WriteLine("Gender Format: Male/Female");
                            }

                            //Student date of birth day                      
                            while (true)
                            {
                                DateTime date;
                                Console.Write("Date of birth day(dd/MM/yyyy): ");
                                studentDOB = Console.ReadLine().Trim();
                                //Format DOB
                                if (DateTime.TryParseExact(studentDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                    break;
                                Console.WriteLine("DOB Format: dd/MM/yyyy");
                            }

                            //Student Note
                            Console.Write("Note: ");
                            string studentNote = Console.ReadLine().Trim();
                            //Delete WhiteSpace > 2 in Note
                            studentNote = Regex.Replace(studentNote, @"\s{2,}", " ");
                            //check if the client has entered the note
                            if (studentNote.Equals(""))
                            {
                                studentNote = "Null";
                            }

                            while (true)
                            {
                                try
                                {
                                    Console.Write("Mark: ");
                                    studentMark = float.Parse(Console.ReadLine());
                                    if (studentMark > 0) { break; }
                                }
                                catch
                                {
                                    Console.WriteLine("Please Enter Number Mark");
                                }
                            }

                            Student student = new Student(studentId, studentName, studentAddress, studentEmail, studentPhone, studentGender, studentDOB, studentNote, studentMark);
                            _listStudent.Add(student);

                            countStudent++;
                        }
                        else
                        {
                            Console.WriteLine("Student ID cannot be duplicated");
                        }
                    }
                    else
                    {
                        if (countStudent >= 1)
                            break;
                        Console.WriteLine("Please enter at least one student");
                    }
                }
                catch
                {
                    Console.WriteLine("Please Enter Number Student ID");
                }

            }
        }


        /*
         * Formart string Display Header
         */
        private void DisplayHeaderStudent() => Console.WriteLine("\n{0,-5}{1,15}{2,30}{3,30}{4,25}{5,15}{6,20}{7,50}{8,15}\n", "ID", "Full Name", "Address", "Email", "Phone", "Gender", "DOB", "Note", "Mark");

        /*
         * Show All Student
         */
        private void DisplayStudent()
        {
            DisplayHeaderStudent();
            _listStudent.ForEach(student => student.Show());
        }

        /*
         * Update Student By Id
         */
        private void UpdateInforStudentById()
        {
            while (true)
            {
                Console.WriteLine("Leave Id Blank And Press Enter Will Exit");
                Console.Write("Please Enter Student ID: ");
                //check Id equals("")
                string id = Console.ReadLine();
                if (!id.Equals(""))
                {
                    //try catch execption format number studentID
                    try
                    {
                        int studentId = int.Parse(id);
                        //Check if student id exists
                        var studentInformation = _listStudent.FirstOrDefault(student => student.Id == studentId);
                        if (studentInformation != null)
                        {
                            //Update Student Name
                            while (true)
                            {
                                Console.Write("Edit Student Name: ");
                                string studentName = Console.ReadLine().Trim();
                                if (!studentName.Equals(""))
                                {
                                    if (IsMatchRegex(studentName, "name"))
                                    {
                                        studentInformation.FullName = studentName;
                                        break;
                                    }
                                    else { Console.WriteLine("Plesae Enter Alphabet(Ex:Le Nguyen Toan)"); }
                                }
                                else { break; }
                            }

                            //Update Student Address
                            while (true)
                            {
                                Console.Write("Edit Student Address: ");
                                string studentAddress = Console.ReadLine().Trim();
                                if (!studentAddress.Equals(""))
                                {
                                    if (IsMatchRegex(studentAddress, "address"))
                                    {
                                        studentInformation.Address = studentAddress;
                                        break;
                                    }
                                    else { Console.WriteLine("Please do not enter special characters"); }

                                }
                                else { break; }
                            }

                            //Update Student Email
                            while (true)
                            {
                                Console.Write("Edit Student Email: ");
                                string studentEmail = Console.ReadLine().Trim();
                                if (!studentEmail.Equals(""))
                                {
                                    try
                                    {
                                        MailAddress mail = new MailAddress(studentEmail);
                                        studentInformation.Email = Convert.ToString(mail);
                                        break;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Incorrect email format");
                                    }
                                }
                                else { break; }
                            }

                            //Update Student Phone
                            while (true)
                            {
                                Console.Write("Edit Student Phone: ");
                                string studentPhone = Console.ReadLine().Trim();
                                if (!studentPhone.Equals(""))
                                {
                                    if (IsMatchRegex(studentPhone, "phone"))
                                    {
                                        studentInformation.Phone = studentPhone;
                                        break;
                                    }
                                    else { Console.WriteLine("Plase enter numbers and limit 10 numbers"); }
                                }
                                else { break; }
                            }

                            //Update Student Gender
                            while (true)
                            {
                                Console.Write("Edit Student Gender: ");
                                string studentGender = Console.ReadLine().Trim();
                                if (!studentGender.Equals(""))
                                {
                                    if (IsMatchRegex(studentGender, "gender"))
                                    {
                                        studentInformation.Gender = studentGender;
                                    }
                                    else { Console.WriteLine("Gender Format: Male/Female"); }
                                }
                                else { break; }
                            }

                            //Update Student DOB
                            while (true)
                            {
                                Console.Write("Edit Student DOB(dd/MM/yyyy): ");
                                string studentDOB = Console.ReadLine().Trim();
                                if (!studentDOB.Equals(""))
                                {
                                    DateTime date;
                                    //Format DOB
                                    if (DateTime.TryParseExact(studentDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                    {
                                        studentInformation.DOB = studentDOB;
                                        break;
                                    }
                                    else { Console.WriteLine("DOB Format: dd/MM/yyyy"); }
                                }
                                else { break; }
                            }

                            //Update Student Note
                            Console.Write("Edit Student Note: ");
                            string studentNote = Console.ReadLine().Trim();
                            if (!studentNote.Equals(""))
                            {
                                studentInformation.Note = studentNote;
                            }

                            //Update Student Mark
                            while (true)
                            {
                                Console.WriteLine("Edit Student Mark: ");
                                string mark = Console.ReadLine();
                                if (!mark.Equals(""))
                                {
                                    int studentMark = int.Parse(mark);
                                    if (studentMark > 0)
                                    {
                                        studentInformation.Mark = studentMark;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter Number Mark > 0");
                                    }

                                }
                                else { break; }
                            }
                        }
                        else { Console.WriteLine("Student ID:{0} not found", studentId); }
                    }
                    catch
                    {
                        Console.WriteLine("Please Enter Number");
                    }
                }
                else { break; }
            }
        }

        /*
         * Search Student
         */
        private void SearchStudentByIdOrName()
        {
            while (true)
            {
                Console.WriteLine("Leave Search Blank And Press Enter Will Back To Menu");
                Console.Write("Please select ID or Name To search for students(Name/Id?): ");
                string typeSearch = Console.ReadLine();
                if (!typeSearch.Equals(""))
                {
                    if (typeSearch.ToLower().Equals("name") || typeSearch.ToLower().Equals("id"))
                    {
                        if (typeSearch.ToLower().Equals("name"))
                        {
                            Console.Write("Please Enter Student Name: ");
                            string studentName = Console.ReadLine();
                            var studentInforListByName = _listStudent.FindAll(student => student.FullName == studentName);
                            if (studentInforListByName.Count != 0)
                            {
                                DisplayHeaderStudent();

                                studentInforListByName.ForEach(student => student.Show());
                            }
                            else { Console.WriteLine("This Name: {0} could not be found", studentName); }
                        }
                        else
                        {
                            Console.Write("Please Enter Student ID: ");
                            int studentId = int.Parse(Console.ReadLine());
                            //check Student By ID
                            var studentInforListById = _listStudent.First(student => student.Id == studentId);

                            if (studentInforListById != null)
                            {
                                DisplayHeaderStudent();

                                studentInforListById.Show();
                            }
                            else { Console.WriteLine("This Student ID: {0} could not be found", studentId); }
                        }
                    }
                    else { Console.WriteLine("Search Format(Name/Id)"); }
                }
                else { break; }
            }
        }


        /*
         * Delete Student By Student ID
         */
        private void DeleteStudentById()
        {
            while (true)
            {
                Console.WriteLine("Leave Search Blank And Press Enter Will Back To Menu");
                Console.Write("Please Enter Student ID: ");
                string id = Console.ReadLine();
                if (!id.Equals(""))
                {
                    int studentId = int.Parse(id);
                    //Find Student By ID
                    try
                    {
                        var resultInforStudent = _listStudent.First(student => student.Id == studentId);
                        while (true)
                        {
                            DisplayHeaderStudent();

                            //Show student when found by id
                            resultInforStudent.Show();

                            Console.Write("Do you want to delete this student?(Yes/No): ");
                            string chooseYesNo = Console.ReadLine().ToLower().Trim();

                            if (chooseYesNo.Equals("yes"))
                            {
                                //Delete Student
                                _listStudent.Remove(resultInforStudent);
                                Console.WriteLine("Deleted Successfully Student ID:{0}", studentId);
                                break;
                            }
                            else if (chooseYesNo.Equals("no"))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong Format Please Enter(Yes/No)");
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Not Found Student ID {0}", studentId);
                    }
                }
                else { break; }
            }
        }

        /*
         * Selection Function Sort Student By ID,Mark,Name
         */
        private void SortStudent()
        {
            while (true)
            {
                Console.WriteLine("Leave Blank And Press Enter Back To Menu");
                Console.Write("Sort Student(ID/Mark/Name)?: ");
                string typeSort = Console.ReadLine();
                if (!typeSort.Equals(""))
                {
                    switch (typeSort.ToLower())
                    {
                        case "id":
                            SortStudentById();
                            break;

                        case "mark":
                            SortStudentByMark();
                            break;

                        case "name":
                            SortStudentByName();
                            break;

                        default:
                            Console.WriteLine("Input format for sort student(ID/Mark/Name)");
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        /*
         * Sort Student By ID
         */
        private void SortStudentById()
        {
            while (true)
            {
                Console.WriteLine("Leave Blank Press Enter Go Back");
                Console.Write("Sort Student By ID(asc/desc)?: ");
                string ascOrDesc = Console.ReadLine().ToLower();
                if (!ascOrDesc.Equals(""))
                {
                    if (ascOrDesc == "asc" || ascOrDesc == "desc")
                    {
                        if (ascOrDesc == "asc")
                        {
                            SortStudentByIdAsc();
                        }
                        else
                        {
                            SortStudentByIdDesc();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input format for sort student by id(asc/desc)");
                    }
                }
                else
                {
                    break;
                }
            }
        }
        //Sort Student By Mark Descending
        private void SortStudentByIdDesc()
        {
            DisplayHeaderStudent();
            //Y < X 
            _listStudent.Sort((studentX, studentY) => studentY.Id.CompareTo(studentX.Id));
            _listStudent.ForEach(student => student.Show());
        }
        //Sort Student By Mark Ascending
        private void SortStudentByIdAsc()
        {
            DisplayHeaderStudent();
            //X < Y
            _listStudent.Sort((studentX, studentY) => studentX.Id.CompareTo(studentY.Id));
            _listStudent.ForEach(student => student.Show());
        }

        /*
         * Sort Student By Mark
         */
        private void SortStudentByMark()
        {
            while (true)
            {
                Console.WriteLine("Leave Blank Press Enter Go Back");
                Console.Write("Sort Student By Mark(asc/desc)?: ");
                string ascOrDesc = Console.ReadLine().ToLower();
                if (!ascOrDesc.Equals(""))
                {
                    if (ascOrDesc == "asc" || ascOrDesc == "desc")
                    {
                        if (ascOrDesc == "asc")
                        {
                            SortStudentByMarkAsc();
                        }
                        else
                        {
                            SortStudentByMarkDesc();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input format for sort student by mark(asc/desc)");
                    }
                }
                else
                {
                    break;
                }
            }
        }
        //Sort Student By Mark Descending
        private void SortStudentByMarkDesc()
        {
            DisplayHeaderStudent();
            //Y < X 
            _listStudent.Sort((studentX, studentY) => studentY.Mark.CompareTo(studentX.Mark));
            _listStudent.ForEach(student => student.Show());
        }
        //Sort Student By Mark Ascending
        private void SortStudentByMarkAsc()
        {
            DisplayHeaderStudent();
            //X < Y
            _listStudent.Sort((studentX, studentY) => studentX.Mark.CompareTo(studentY.Mark));
            _listStudent.ForEach(student => student.Show());
        }

        /*
         * Sort Student By Name
         */
        private void SortStudentByName()
        {
            while (true)
            {
                Console.WriteLine("Leave Blank Press Enter Go Back");
                Console.Write("Sort Student By Name A-Z Or Z-A (asc/desc)?: ");
                string ascOrDesc = Console.ReadLine().ToLower();
                if (!ascOrDesc.Equals(""))
                {
                    if (ascOrDesc == "asc" || ascOrDesc == "desc")
                    {
                        if (ascOrDesc == "asc")
                        {
                            SortStudentByNameAsc();
                        }
                        else
                        {
                            SortStudentByNameDesc();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input format for sort student by name(asc/desc)");
                    }
                }
                else
                {
                    break;
                }
            }
        }
        //Sort Student By Name Ascending
        private void SortStudentByNameAsc()
        {
            DisplayHeaderStudent();
            //X < Y
            _listStudent.Sort((studentX, studentY) => studentX.FullName.CompareTo(studentY.FullName));
            _listStudent.ForEach(student => student.Show());
        }
        //Sort Student By Name Descending
        private void SortStudentByNameDesc()
        {
            DisplayHeaderStudent();
            //Y < X
            _listStudent.Sort((studentX, studentY) => studentY.FullName.CompareTo(studentX.FullName));
            _listStudent.ForEach(student => student.Show());
        }

        /*
         * Working With File
         * Full Path File Students.dat
         */
        private string FileStudents()
        {
            //Path working project
            string workingDirectory = Environment.CurrentDirectory;
            //Shorten the folder to the same level as the project folder
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //Full Path Students.dat
            string pathStudentFile = $"{projectDirectory}\\Students.dat";

            return pathStudentFile;
        }
        //Save File StudentManagement/StudentManagement/Students.dat
        private void WriteToFile()
        {
            try
            {
                //Full Path File Students.dat
                string fileStudent = FileStudents();

                string headerStudent = string.Format("{0,-5}{1,15}{2,30}{3,30}{4,25}{5,15}{6,20}{7,50}{8,15}\n", "ID", "Full Name", "Address", "Email", "Phone", "Gender", "DOB", "Note", "Mark");
                //Write Header Student in File
                File.WriteAllText(fileStudent, headerStudent);

                //Write Student Information
                _listStudent.ForEach(student =>
                File.AppendAllText(fileStudent, string.Format("\n{0,-5}{1,15}{2,30}{3,30}{4,25}{5,15}{6,20}{7,50}{8,15}\n", student.Id, student.FullName, student.Address, student.Email, student.Phone, student.Gender, student.DOB, student.Note, student.Mark)));
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exeception: " + exception);
            }
        }
        //Read File StudentManagement/StudentManagement/Students.dat
        private void ReadFromFile()
        {
            try
            {
                //Full Path File Students.dat
                string fileStudent = FileStudents();

                //Check if file exists
                if (File.Exists(fileStudent))
                {
                    //Read from file Students.dat
                    string[] fileContent = File.ReadAllLines(fileStudent);

                    for (int i = 1; i <= fileContent.Length; i++)
                    {
                        //Get the contents even lines
                        if (i % 2 == 0)
                        {
                            string contentEachLine = fileContent[i];
                            // separate fields in line
                            var inforStudent = Regex.Split(contentEachLine, @"\s{3,}");

                            Student student = new Student(int.Parse(inforStudent[0]), inforStudent[1], inforStudent[2], inforStudent[3], inforStudent[4], inforStudent[5], inforStudent[6], inforStudent[7], float.Parse(inforStudent[8]));
                            _listStudent.Add(student);
                        }
                    }
                }
                else
                {
                    //Create File and Close
                    File.Create(fileStudent).Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exeception: " + exception);
            }
        }


    }
}
