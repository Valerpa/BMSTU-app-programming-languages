using Microsoft.EntityFrameworkCore;
using DatabaseModels;
namespace DatabaseContext;

public class UniversityRepository : IUniversityRepository
{
    private UniversityContext _context;

    public UniversityRepository(UniversityContext context)
    {
        _context = context;
    }

    public async Task<int> AddStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        try
        { 
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new DbUpdateException();
        }
        return student.Id;
    }

    public async Task<int> AddGroupAsync(Group group)
    {
        await _context.Groups.AddAsync(group);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new DbUpdateException();
        }

        return group.Id;
    }
    

    public async Task<int> AddCuratorAsync(Curator curator)
    {
        
        await _context.Curators.AddAsync(curator);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new DbUpdateException();
        }
        return curator.Id;
    }

    public async Task<bool> DeleteStudentAsync(int studentId)
    {
        var student = await _context.Students.FindAsync(studentId);
        bool flag = false;
        if (student != null)
        {
            _context.Students.Remove(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
            flag = true;
        }

        return flag;
    }

    public async Task<bool> DeleteGroupAsync(int groupId)
    {
        var group = await _context.Groups.FindAsync(groupId);
        bool flag = false;
        if (group != null)
        {
            var curator = await _context.Curators.FirstOrDefaultAsync(c => c.GroupId == groupId);
            if (curator != null)
            {
                _context.Curators.Remove(curator);
            }
            var students = await GetAllStudentsByGroupId(groupId);
            
            if (students.Count != 0)
            {
                _context.Students.RemoveRange(students);
            }
            
            _context.Groups.Remove(group);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }

            flag = true;
        }

        return flag;
    }

    public async Task<bool> DeleteCuratorAsync(int curatorId)
    {
        var curator = await _context.Curators.FindAsync(curatorId);
        bool flag = false;
        if (curator != null)
        {
            _context.Curators.Remove(curator);
            try
            {
                await _context.SaveChangesAsync();
                flag = true;
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }
        return flag;
    }

    public async Task<Student> UpdateStudentAsync(Student updatedStudent)
    {
        var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == updatedStudent.Id);

        if (existingStudent != null)
        {
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.GroupId = updatedStudent.GroupId;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }

        return existingStudent;
    }
    
    public async Task<Curator> UpdateCuratorAsync(Curator updatedCurator)
    {
        var existingCurator = await _context.Curators.FirstOrDefaultAsync(c => c.Id == updatedCurator.Id);

        if (existingCurator != null)
        {
            existingCurator.Name = updatedCurator.Name;
            existingCurator.Email = updatedCurator.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }

        return existingCurator;
    }
    
    public async Task<Group> UpdateGroupAsync(Group newGroup)
    {
        var existingGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == newGroup.Id);

        if (existingGroup != null)
        {
            existingGroup.Name = newGroup.Name;
            existingGroup.CreationDate = newGroup.CreationDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }
        }

        return existingGroup;
    }

    public async Task<Student?> GetStudentByIdAsync(int studentId)
    {
        return await _context.Students.FirstOrDefaultAsync(x => x.Id == studentId);
    }
    
    public async Task<Curator?> GetCuratorByIdAsync(int curatorId)
    {
        return await _context.Curators.FirstOrDefaultAsync(x => x.Id == curatorId);
    }
    
    public async Task<Group?> GetGroupByIdAsync(int groupId)
    {
        return await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
    }

    public async Task<List<Student>> GetAllStudents()
    {
        return await _context.Students.ToListAsync();
    }
    
    public async Task<List<Student>> GetAllStudentsByGroupId(int groupId)
    {
        return await _context.Students
            .Where(x => x.GroupId == groupId)
            .ToListAsync();
    }

    public async Task<List<Curator>> GetAllCurators()
    {
        return await _context.Curators.ToListAsync();
    }
    
    public async Task<List<Group>> GetAllGroups()
    {
        return await _context.Groups.ToListAsync();
    }
    
    public async Task<int> GetStudentCountInGroupAsync(int groupId)
    {
       return await _context.Students.CountAsync(s => s.GroupId == groupId);
    }
    
    public async Task<string?> GetCuratorNameByStudentIdAsync(int studentId)
    {
        var student = await _context.Students.Include(s => s.Group)
            .ThenInclude(g => g.Curator)
            .FirstOrDefaultAsync(s => s.Id == studentId);
        var curator = student?.Group.Curator;
        return curator?.Name;
    }

    public async Task<double> GetAverageAgeOfStudentsByCuratorIdAsync(int curatorId)
    {
        double result = 0;
        var students = await _context.Students
            .Where(s => s.Group.Curator.Id == curatorId)
            .ToListAsync();
        if (students.Count != 0)
        {
            result = students.Average(s => (double)s.Age);
        }
        return result;
    }

    public async Task<bool> CheckCuratorExisting(int groupId)
    {
        var group = await _context.Groups
            .Include(g => g.Curator)
            .FirstOrDefaultAsync(g => g.Id == groupId);

        return group?.Curator != null;
    }
}