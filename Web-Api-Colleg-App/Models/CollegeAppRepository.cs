namespace Web_Api_Colleg_App.Models;

public static class CollegeAppRepository
{
    public static List<Student> students = new()
    {
        new Student()
        {
            StudentId = 1,
            StudentName = "Sharan",
            Address = "Koopal, KA",
            Emial = "Sharan@gmail.com"
        },
        new Student()
        {
            StudentId = 2,
            StudentName = "Shrikant",
            Address = "Athani, KA",
            Emial = "Shrikant@gmail.com"
        }
    };
}

