using System.ComponentModel.DataAnnotations;

namespace StudentCrudOperationWithReactjs.Model
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string?   stname { get; set; }
        public string? course{ get; set;}
        public string? StudentPictureUrl { get; set; }
    }
    public class StudentVM
    {
        [Key]
        public int id { get; set; }
        public string stname { get; set; }
        public string course { get; set; }
        public string Picture { get; set; }
    }
}
