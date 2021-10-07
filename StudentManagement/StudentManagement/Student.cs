using System;

namespace StudentManagement
{
    class Student
    {
        private int _id;
        private string _fullName, _address, _email, _phone, _gender, _dOB, _note;
        private float _mark;

        public Student(int studentId, string studentName, string studentAddress, string studentEmail, string studentPhone, string studentGender, string studentDOB, string studentNote, float studentMark)
        {
            _id = studentId;
            _fullName = studentName;
            _address = studentAddress;
            _email = studentEmail;
            _phone = studentPhone;
            _gender = studentGender;
            _dOB = studentDOB;
            _note = studentNote;
            _mark = studentMark;
        }

        public Student() { }

        public int Id { get => _id; set => _id = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string Address { get => _address; set => _address = value; }
        public string Email { get => _email; set => _email = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Gender { get => _gender; set => _gender = value; }
        public string DOB { get => _dOB; set => _dOB = value; }
        public string Note { get => _note; set => _note = value; }
        public float Mark { get => _mark; set => _mark = value; }

        public void Show() => Console.WriteLine("{0,-5}{1,15}{2,30}{3,30}{4,25}{5,15}{6,20}{7,50}{8,15}\n", _id, _fullName, _address, _email, _phone, _gender, _dOB, _note, _mark);
    }
}
