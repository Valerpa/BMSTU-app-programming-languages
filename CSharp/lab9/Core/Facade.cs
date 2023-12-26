using DatabaseContext;
using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class Facade
{
    private UniversityRepository _repository;
    private Dictionary<string, Func<Task>> _operations;
    
    private const string FinishProcessCode = "18";
    public Facade(UniversityRepository repository)
    {
        _repository = repository;
        
        _operations = new Dictionary<string, Func<Task>>
        {
            { "1", AddStudent },
            { "2", UpdateStudent },
            { "3", DeleteStudent },
            { "4", AddCurator },
            { "5", UpdateCurator },
            { "6", DeleteCurator },
            { "7", AddGroup },
            { "8", UpdateGroup },
            { "9", DeleteGroup },
            { "10", MoveStudentToOtherGroup },
            { "11", OutputStudentsByGroupId },
            { "12", OutputStudents},
            { "13", OutputCurators },
            { "14", OutputGroups },
            { "15", GetNumberOfStudentsInGroupById },
            { "16", OutputCuratorNameByStudentId },
            { "17", OutputAverageAgeOfStudentsInGroupByCuratorId },
            { FinishProcessCode, Finish }
        };
    }
    
    public async Task Run()
    {
        
        DisplayMenu();
        while (true)
        {
            string? userInput = Console.ReadLine();

            if (_operations.TryGetValue(userInput, out Func<Task> operation))
            {
                try
                {
                    await operation();
                    if (userInput == FinishProcessCode)
                    {
                        break;
                    }
                }
                catch (MyExceptions ex)
                {
                    Console.WriteLine(ex.Message); 
                }
            }
            else
            {
                Console.WriteLine("Incorrect option! (must be integer 1-17).");
            }
            Console.WriteLine("\nChoose next option:");
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("Choose option:");
        Console.WriteLine("1. Add student");
        Console.WriteLine("2. Update student");
        Console.WriteLine("3. Delete student");
        Console.WriteLine("4. Add curator");
        Console.WriteLine("5. Update curator");
        Console.WriteLine("6. Delete curator");
        Console.WriteLine("7. Add group");
        Console.WriteLine("8. Update group");
        Console.WriteLine("9. Delete group");
        Console.WriteLine("10. Move student with given ID to other group");
        Console.WriteLine("--------------------");
        Console.WriteLine("Entities output:");
        Console.WriteLine("11. Output students by group ID");
        Console.WriteLine("12. Output all students");
        Console.WriteLine("13. Output curators");
        Console.WriteLine("14. Output groups");
        Console.WriteLine("--------------------");
        Console.WriteLine("Lab tasks:");
        Console.WriteLine("15. Output number of students in the group with given ID");
        Console.WriteLine("16. Output name of curator of group in which the student with given ID is a member");
        Console.WriteLine("17. Output average age of  students in group supervised by curator with given ID");
        Console.WriteLine("--------------------");
        Console.WriteLine("18. Finish program");
    }
    
    private async Task AddStudent() 
    {
        await OutputGroups();
        var groupId = ReadValidInteger("group's ID");
    
        var group = await _repository.GetGroupByIdAsync(groupId);
        if (group == null)
        {
            throw new NonExistentEntityException("Group");
        }

        var name = ReadValidName("student");
        var age = ReadValidInteger("age");
        var student = new Student { Name = name, GroupId = groupId, Age = age };
        try
        {
            await _repository.AddStudentAsync(student);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }
    
        Console.WriteLine("Student successfully added.");
        
    }


    private async Task UpdateStudent()
    {
        await OutputStudents();
        var studentId = ReadValidInteger("student's ID");
        
        var student = await _repository.GetStudentByIdAsync(studentId);
        
        if (student == null)
        {
            throw new NonExistentEntityException("Student");
        }

        Console.WriteLine($"Current student's name {student.Name}");
        var newName = ReadValidName("new student");

        Console.WriteLine($"Current student's age {student.Age}");
        var newAge = ReadValidInteger("student's new age");
        student.Name = newName;
        student.Age = newAge;
        try
        {
            await _repository.UpdateStudentAsync(student);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }

        Console.WriteLine("Student successfully updated.");
    }

    private async Task DeleteStudent()
    {
        await OutputStudents();
        var studentId = ReadValidInteger("student's ID");

        bool isDeleted;
        try
        {
            isDeleted = await _repository.DeleteStudentAsync(studentId);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }
        
        if (!isDeleted)
        {
            throw new NonExistentEntityException("Student");
        }
        
        Console.WriteLine("Student successfully deleted.");

    }

    private async Task AddCurator()
    {
        await OutputGroups();
        var groupId = ReadValidInteger("group's ID");

        var group = await _repository.GetGroupByIdAsync(groupId);
        if (group == null)
        {
            throw new NonExistentEntityException("Group");
        }
        var hasCurator = await _repository.CheckCuratorExisting(groupId);
        if (hasCurator)
        {
            throw new AlreadyExistException();
        }
        
        var sender = "curator";
        var name = ReadValidName(sender);
        var email = ReadValidEmail(sender);
        
        Curator curator = new Curator { Name = name, GroupId = groupId, Email = email };
        try
        {
            await _repository.AddCuratorAsync(curator);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }

        Console.WriteLine("Curator successfully added.");
    }
    private async Task UpdateCurator()
    {
        await OutputCurators();
        var curatorId = ReadValidInteger("curator's ID");
        
        var curator = await _repository.GetCuratorByIdAsync(curatorId);
        
        if (curator == null)
        {
            throw new NonExistentEntityException("Curator");
        }

        Console.WriteLine($"Current curator's name: {curator.Name}");
        Console.WriteLine("Enter new curator's name:");
        var sender = "new curator";
        var newName = ReadValidName(sender);

        Console.WriteLine($"Current curator's email: {curator.Email}");
        
        var newEmail = ReadValidEmail(sender);
        
        curator.Name = newName;
        curator.Email = newEmail;
        try
        {
            await _repository.UpdateCuratorAsync(curator);
        }

        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }

        Console.WriteLine("Curator successfully updated."); 
    }

    private async Task DeleteCurator()
    {
        await OutputCurators();
        var curatorId = ReadValidInteger("curator's ID");

        bool isDeleted;
        try
        {
            isDeleted = await _repository.DeleteCuratorAsync(curatorId);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }

        Console.WriteLine(isDeleted ? "Curator successfully deleted." : "No curators with this ID.");
    }

    private async Task AddGroup()
    {
        var name = ReadValidName("group");
        var creationDate = DateTimeOffset.UtcNow;
        Group group = new Group { Name = name, CreationDate = creationDate };
        try
        {
            await _repository.AddGroupAsync(group);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }

        Console.WriteLine("Group successfully added!");
    }

    private async Task UpdateGroup()
    {
        await OutputGroups();
        var groupId = ReadValidInteger("group's ID");
        
        var group = await _repository.GetGroupByIdAsync(groupId);
        if (group == null)
        {
            throw new NonExistentEntityException("Group");
        }

        Console.WriteLine($"Current group's name: {group.Name}");
        var newName = ReadValidName("new group");
        
        group.Name = newName;
        try
        {
            await _repository.UpdateGroupAsync(group);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }

        Console.WriteLine("Group successfully updated.");
    }

    private async Task DeleteGroup()
    {
        await OutputGroups();
        var groupId = ReadValidInteger("group's ID");

        bool isDeleted;
        try
        {
            isDeleted = await _repository.DeleteGroupAsync(groupId);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }
        Console.WriteLine(isDeleted ? "Group successfully deleted." : "No groups with this ID.");
    }

    private async Task MoveStudentToOtherGroup()
    {
        await OutputStudents();
        var studentId = ReadValidInteger("student's ID");
        var student = await _repository.GetStudentByIdAsync(studentId);
        if (student == null)
        {
            throw new NonExistentEntityException("Student");
        }

        await OutputGroups();
        var newGroupId = ReadValidInteger("new group's ID");
        var newGroup = await _repository.GetGroupByIdAsync(newGroupId);
        if (newGroup == null)
        {
            throw new NonExistentEntityException("Group");
        }

        student.GroupId = newGroupId;
        try
        {
            await _repository.UpdateStudentAsync(student);
        }
        catch (DbUpdateException)
        {
            throw new DatabaseUpdateException();
        }

        Console.WriteLine($"Student successfully moved to group {newGroupId}.");
    }
    private async Task GetNumberOfStudentsInGroupById()
    {
        await OutputGroups();
        var groupId = ReadValidInteger("group's ID");
        var group = await _repository.GetGroupByIdAsync(groupId);
        if (group == null)
        {
            throw new NonExistentEntityException("Group");
        }
        var count = await _repository.GetStudentCountInGroupAsync(groupId);
        Console.WriteLine($"{count} students in group with ID {groupId}");
    }

    private async Task OutputCuratorNameByStudentId()
    {
        await OutputStudents();
        var studentId = ReadValidInteger("student's ID");

        var student = await _repository.GetStudentByIdAsync(studentId);
        if (student == null)
        {
            throw new NonExistentEntityException("Student");
        }
        
        var name = await _repository.GetCuratorNameByStudentIdAsync(studentId);
        if (name == null)
        {
            throw new NonExistentEntityException("Curator");
        }
        Console.WriteLine($"Curator's name of student with ID {studentId} is {name}");
    }

    private async Task OutputAverageAgeOfStudentsInGroupByCuratorId()
    {
        await OutputCurators();
        var curatorId = ReadValidInteger("curator's ID");
        var curator = await _repository.GetCuratorByIdAsync(curatorId);
        if (curator == null)
        {
            throw new NonExistentEntityException("Curator");
        }
        var averageAge = await _repository.GetAverageAgeOfStudentsByCuratorIdAsync(curatorId);
        Console.WriteLine(averageAge > 0 ? 
            $"Average age of students in group supervised by curator ID {curatorId} = {averageAge}"
            : "No students in group supervised by this curator!");
    }

    private async Task OutputStudents() 
    {
        var students = await _repository.GetAllStudents();
        if (students.Count == 0)
        {
            throw new NonExistentEntityException("Students");
        }
        Console.WriteLine("All students:");
        foreach (var student in students)
        {
            Console.Write($"ID: {student.Id}, ");
            Console.Write($"Name: {student.Name}, ");
            Console.Write($"Age: {student.Age}, " );
            Console.WriteLine($"Group ID: {student.GroupId}");
        }
    }
    private async Task OutputStudentsByGroupId()
    {
        await OutputGroups();
        var groupId = ReadValidInteger("group's ID");
        var students = await _repository.GetAllStudentsByGroupId(groupId);
        if (students.Count == 0)
        {
            throw new NonExistentEntityException("Group");
        }
        Console.WriteLine("All students:");
        foreach (var student in students)
        {
            Console.Write($"ID: {student.Id}, ");
            Console.Write($"Name: {student.Name}, ");
            Console.WriteLine($"Age: {student.Age}");
        }
    }
    
    private async Task OutputCurators()
    {
        var curators = await _repository.GetAllCurators();
        if (curators.Count == 0)
        {
            throw new NonExistentEntityException("Curators");
        }
        Console.WriteLine("All curators:");
        
        foreach (var curator in curators)
        {
            Console.Write($"ID: {curator.Id}, ");
            Console.Write($"Name: {curator.Name}, ");
            Console.Write($"Email: {curator.Email}, ");
            Console.WriteLine($"Group ID: {curator.GroupId}");
        }
    }
    
    private async Task OutputGroups()
    {
        var groups = await _repository.GetAllGroups();
        if (groups.Count == 0)
        {
            throw new NonExistentEntityException("Groups");
        }
        Console.WriteLine("All groups:");
        foreach (var group in groups)
        {
            Console.Write($"ID: {group.Id}, ");
            Console.WriteLine($"Name: {group.Name}");
        }
    }
    private Task Finish()
    {
        Console.WriteLine("Program finished.");
        return Task.CompletedTask;
    }
    

    private int ReadValidInteger(string parameter)
    {
        int value = 0;
        while (value <= 0)
        {
            Console.WriteLine($"Enter {parameter}:");
            if (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                try
                {
                    throw new IncorrectInputException(parameter);
                }
                catch (IncorrectInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        return value;
    }

    private string ReadValidName(string sender)
    {
        string? name = string.Empty;
        while (string.IsNullOrEmpty(name))
        {
            Console.WriteLine($"Enter {sender}'s name:");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                try
                {
                    throw new IncorrectInputException("name");
                }
                catch (IncorrectInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        return name;
    }

    private string ReadValidEmail(string sender)
    {
        string? email = string.Empty;
        while (string.IsNullOrEmpty(email) || !email.Contains('@') || !email.Contains('.'))
        {
            Console.WriteLine($"Enter {sender}'s email:");
            email = Console.ReadLine();
            if (string.IsNullOrEmpty(email) || !email.Contains('@') || !email.Contains('.'))
            {
                try
                {
                    throw new IncorrectEmailException();
                }
                catch (IncorrectEmailException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        return email;
    }
}