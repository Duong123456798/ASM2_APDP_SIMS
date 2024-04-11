using Microsoft.AspNetCore.Mvc;
using SIMS_IT0602.Controllers;
using SIMS_IT0602.Models;
using System.Text.Json;

namespace SIMS_TEST;

public class Testing
{
    [Fact]
    public void Test_LoadTeachersFromFile_Success()
    {
        // Arrange
        TeacherController sut = new TeacherController();

        // Assuming you have teacher.json file in the same directory with test file
        string filePath = "teacher.json";

        // Act
        List<Teacher> result = sut.LoadTeacherFromFile(filePath);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(4, result.Count); // Kiểm tra xem số lượng giáo viên được tải có đúng không
        Assert.Contains(result, t => t.Name == "Do Quoc Binh"); // Kiểm tra xem một giáo viên cụ thể có trong danh sách không
    }
    [Fact]
    public void Test_LoadClassFromFile_Success()
    {
        ClassController sut = new ClassController();

       
        string filePath = "class.json";

        // Act
        List<Class> result = sut.LoadClassFromFile(filePath);
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("IT0602", result[0].Name);
        Assert.Equal("Technology Information", result[0].Major);
        Assert.Equal("Dinh Van Dong", result[0].Lecturer);
    }
    [Fact]
    public void Test_LoadCoursesFromFile_Success()
    {

        CourseController sut = new CourseController();

        string filePath = "course.json";

        // Act
        List<Course> result = sut.LoadCourseFromFile(filePath);
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Programing", result[0].Name);
        Assert.Equal("IT0602", result[0].Class);
        Assert.Equal("Technology Information", result[0].Major);
    }
   
}
public class TestingLoadStudent
{
    [Theory]
    [InlineData("student.json",1)] // Dữ liệu đầu vào: tên tệp và số lượng sinh viên mong đợi
    public void Test_LoadStudentsFromJsonFile_Success(string filePath, int expectedCount)
    {
        // Arrange
        var test = new TestingFunction();

        // Act
        var students = test.LoadStudentsFromJsonFile(filePath);

        // Assert
        Assert.NotNull(students);
        Assert.NotEmpty(students);
        Assert.Equal(expectedCount, students.Count);
    }

}
public class TestingFunction
{
    [Fact]
    public void TestNewStudent_ValidInput_RedirectToAction()
    {
        // Arrange
        var controller = new StudentController();
        var newStudent = new Student { Id = 5, Name = "New Student", Address = "New Address", Major = "New Major" };

        // Act
        var result = controller.NewStudent(newStudent) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ManageStudent", result.ActionName);

        // Check if the new student is added to the list
        var students = LoadStudentsFromJsonFile("student.json");
        var addedStudent = students.Find(s => s.Id == 5);
        Assert.NotNull(addedStudent);
        Assert.Equal("New Student", addedStudent.Name);
        Assert.Equal("New Address", addedStudent.Address);
        Assert.Equal("New Major", addedStudent.Major);
    }

    [Fact]
    public void TestNewStudent_InvalidInput_ReturnsViewWithError()
    {
        // Arrange
        var controller = new StudentController();
        var newStudent = new Student { Id = 0, Name = "", Address = "", Major = "" };

        // Act
        var result = controller.NewStudent(newStudent) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NewStudent", result.ViewName);
        Assert.True(result.ViewData.ModelState.ErrorCount > 0);
    }

    public List<Student> LoadStudentsFromJsonFile(string filePath)
    {
        var jsonString = System.IO.File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Student>>(jsonString);
    }

}





