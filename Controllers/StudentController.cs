using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCrudOperationWithReactjs.Context;
using StudentCrudOperationWithReactjs.Model;

namespace StudentCrudOperationWithReactjs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDbContext _studentDbContext;
        public StudentController(StudentDbContext db)
        {
            _studentDbContext = db;
        }
        [HttpGet]
        [Route("GetStudent")]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _studentDbContext.Student.ToListAsync();
        }
        [HttpGet]
        [Route("GetStudentById/{id}")]
        public async Task<Student> GetStudentById(int id)
        {
            return await _studentDbContext.Student.FindAsync(id);
        }

        [HttpPost]
        [Route("AddStudent")]
        public async Task<Student> AddStudent([FromForm] Student objStudent, IFormFile picture)
         {
            if (picture != null)
            {
                // Generate a unique filename for the picture
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);

                // Specify the directory path where the pictures will be saved
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Combine the directory path and the filename to get the full file path
                var filePath = Path.Combine(directoryPath, fileName);

                // Save the picture file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }
                // Assign the URL to the objStudent.StudentPictureUrl property
                //objStudent.StudentPictureUrl = "https://localhost:7129/uploads/" + fileName;
                objStudent.StudentPictureUrl = fileName;
                // Assign the URL or file path to the objStudent.StudentPictureUrl property
                //objStudent.StudentPictureUrl = filePath; // Example: Assigning the file path
                                                         //objStudent.StudentPictureUrl = "https://example.com/uploads/" + fileName; // Example: Assigning the URL
            }
            _studentDbContext.Student.Add(objStudent);
            await _studentDbContext.SaveChangesAsync();
            return objStudent;
        }
        //[HttpPatch]
        //[Route("UpdateStudent/{id}")]
        //public async Task<Student> UpdateStudent(Student objStudent)
        //{
        //    _studentDbContext.Entry(objStudent).State = EntityState.Modified;
        //    await _studentDbContext.SaveChangesAsync();
        //    return objStudent;
        //}
        [HttpPatch]
        [Route("UpdateStudent/{id}")]
        public async Task<Student> UpdateStudent(int id, [FromForm] Student objStudent, IFormFile picture)
        {
            var existingStudent = await _studentDbContext.Student.FindAsync(id);

            if (existingStudent == null)
            {
                // Student with the specified ID was not found
                throw new ArgumentException("Invalid student ID.");
            }

            // Update the student properties
            existingStudent.stname = objStudent.stname;
            existingStudent.course = objStudent.course;

            // Update the picture if provided
            if (picture != null)
            {
                // Generate a unique filename for the new picture
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);

                // Specify the directory path where the pictures will be saved
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Combine the directory path and the filename to get the full file path
                var filePath = Path.Combine(directoryPath, fileName);

                // Save the new picture file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }

                // Assign the URL or file path to the objStudent.StudentPictureUrl property
                existingStudent.StudentPictureUrl = fileName; // Example: Assigning the file path
                                                              //existingStudent.studentPictureUrl = "https://example.com/uploads/" + fileName; // Example: Assigning the URL
            }

            // Update the student in the database
            await _studentDbContext.SaveChangesAsync();

            return existingStudent;
        }





        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public bool DeleteStudent(int id)
        {
            bool a = false;
            var student = _studentDbContext.Student.Find(id);
            if (student != null)
            {
                a = true;
                _studentDbContext.Entry(student).State = EntityState.Deleted;
                _studentDbContext.SaveChanges();
            }
            else
            {
                a = false;
            }
            return a;
        }

    }
}
