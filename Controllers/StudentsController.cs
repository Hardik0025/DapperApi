using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperApi.Models;
using DapperApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        StudentRepository studentRepository = new StudentRepository();
        [HttpGet]
        public async Task<IEnumerable<StudentInfo>> GetStudents()
        {
            return await studentRepository.GetStudents();
        }
        [HttpPost]
        public async Task<bool> CreateStudent(StudentInfo student)
        {
            return await studentRepository.CreateStudent(student);
        }
        [HttpGet("GetById/{id}")]
        public async Task<StudentInfo> GetStudentById(int id)
        {
            return await studentRepository.GetStudentById(id);
        }
        [HttpPut]
        [Route("updateStudent")]
        public async Task<bool> UpdateStudent(StudentInfo updatedStudent)
        {
            return await studentRepository.UpdateStudent(updatedStudent);
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteStudent(int id)
        {
            return await studentRepository.DeleteStudent(id);
        }
    }
}
