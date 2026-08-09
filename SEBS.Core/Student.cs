using System;

namespace SEBS.Core
{
    public class Student
    {
        public string StudentId { get; }
        public string Name { get; }
        public string Email { get; }

        public Student(string studentId, string name, string email)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Student name cannot be empty.");

            StudentId = studentId;
            Name = name;
            Email = email;
        }
    }
}