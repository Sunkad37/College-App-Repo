using Microsoft.AspNetCore.Mvc;
using Web_Api_Colleg_App.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Web_Api_Colleg_App.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController : Controller
{
    //Api - GetAll
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<Student>> GetStudents()
    {
        var students = CollegeAppRepository.students.Select(s => new StudentDto
        {
            StudentId = s.StudentId,
            StudentName = s.StudentName,
            Address = s.Address,
            Emial = s.Emial
        });
        if (students is null)
            return NotFound();
        return Ok(students);
    }

    // Api - GetbyId
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}", Name = "GetStudentById")]
    public ActionResult<StudentDto> GetStudentById(int id)
    {
        if (id == 0)
            return BadRequest();
        var student = CollegeAppRepository.students.Where(s => s.StudentId == id).FirstOrDefault();

        if (student is null)
            return NotFound();

        StudentDto studentDto = new()
        {
            StudentId = student.StudentId,
            StudentName = student.StudentName,
            Address = student.Address,
            Emial = student.Emial
        };
        return Ok(studentDto);
    }

    //Api - GetbyName
    [HttpGet]
    [Route("{name:alpha}", Name = "GetStudentByName")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Student> GetStudentByName(string name)
    {
        if (name is null)
            return BadRequest();
        var student = CollegeAppRepository.students.Where(s => s.StudentName == name)
            .FirstOrDefault();
        if (student is null)
            return NotFound();
        StudentDto studentDto = new()
        {
            StudentId = student.StudentId,
            StudentName = student.StudentName,
            Address = student.Address,
            Emial = student.Emial
        };
            
        return Ok(studentDto);
    }

    //Api - Delete
    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> DeleteStudent(int id)
    {
        if (id <= 0)
            return BadRequest();
        var student = CollegeAppRepository.students.Where(s => s.StudentId == id)
            .FirstOrDefault();
        if (student is null)
            return NotFound();
        CollegeAppRepository.students.Remove(student);
        return Ok(true);
    }

    //Api - Create
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("Create")]
    public ActionResult<Student> CreateStudent([FromBody] StudentDto studentDto)
    {
        if (studentDto is null)
            return BadRequest();

        if (studentDto.StudentId <= 0)
            return BadRequest();

        int nextStudentId = CollegeAppRepository.students.Any()
            ? CollegeAppRepository.students.Max(s => s.StudentId) + 1 : 1;

       
        var newStudent = new Student()
        {
            StudentId = nextStudentId,
            StudentName = studentDto.StudentName,
            Address = studentDto.Address,
            Emial = studentDto.Emial
        };
        CollegeAppRepository.students.Add(newStudent);

        return CreatedAtRoute("GetStudentById", new { id = newStudent.StudentId}, newStudent);
    }

    [HttpPut]
    [Route("UpdateStudent")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<StudentDto> UpdateStudent([FromBody] StudentDto model)
    {
        if (model.StudentId <= 0 || model is null)
            return BadRequest();

        var existingStudent = CollegeAppRepository.students.Where(s => s.StudentId == model.StudentId)
            .FirstOrDefault();

        if (existingStudent is null)
            return NotFound();

        // Update the existing student entity
        existingStudent.StudentName = model.StudentName;
        existingStudent.Address = model.Address;
        existingStudent.Emial = model.Emial;

        // Optionally return the updated DTO
        var updatedDto = new StudentDto
        {
            StudentId = existingStudent.StudentId,
            StudentName = existingStudent.StudentName,
            Address = existingStudent.Address,
            Emial = existingStudent.Emial
        };

        return Ok(updatedDto);
    }

    [HttpPatch]
    [Route("{id:int}", Name = "UpdatePartial")]
    [Consumes("application/json-patch+json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult PatchStudent(int id, [FromBody] JsonPatchDocument<Student> patchDoc)
    {
        if (patchDoc == null)
            return BadRequest();

        var student = CollegeAppRepository.students.FirstOrDefault(s => s.StudentId == id);
        if (student == null)
            return NotFound();

        patchDoc.ApplyTo(student, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(student);
    }
}

