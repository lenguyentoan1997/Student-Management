using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    class Student
    {
        private int id;
        private string fullName, address, email, phone, gender, dOB, note;
        float mark;

        public Student(int studentId, string studentName, string studentAddress, string studentEmail, string studentPhone, string studentGender, string studentDOB, string studentNote, float studentMark)
        {
            id = studentId;
            fullName = studentName;
            address = studentAddress;
            email = studentEmail;
            phone = studentPhone;
            gender = studentGender;
            dOB = studentDOB;
            note = studentNote;
            mark = studentMark;
        }

        public Student() { }

        public int Id { get => id; set => id = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Gender { get => gender; set => gender = value; }
        public string DOB { get => dOB; set => dOB = value; }
        public string Note { get => note; set => note = value; }
        public float Mark { get => mark; set => mark = value; }

        public void Show() => Console.WriteLine("{0,-5}{1,15}{2,30}{3,30}{4,25}{5,15}{6,20}{7,50}{8,15}\n", id, fullName, address, email, phone, gender, dOB, note, mark);
    }
}
