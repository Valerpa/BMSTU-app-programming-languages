using DatabaseModels;
namespace DatabaseContext;

public interface IUniversityRepository
{
    Task<int> AddStudentAsync(Student student);
    Task<int> AddGroupAsync(Group group);
    Task<int> AddCuratorAsync(Curator curator);
    Task<bool> DeleteStudentAsync(int studentId);
    Task<bool> DeleteGroupAsync(int groupId);
    Task<bool> DeleteCuratorAsync(int curatorId);
    Task<Student> UpdateStudentAsync(Student updatedStudent);
    Task<Curator> UpdateCuratorAsync(Curator updatedCurator);
    Task<Group> UpdateGroupAsync(Group updatedGroup);
    Task<Student?> GetStudentByIdAsync(int studentId);
    Task<Curator?> GetCuratorByIdAsync(int curatorId);
    Task<Group?> GetGroupByIdAsync(int groupId);
    Task<List<Student>> GetAllStudents();
    Task<List<Student>> GetAllStudentsByGroupId(int groupId);
    Task<List<Group>> GetAllGroups();
    Task<List<Curator>> GetAllCurators();
    Task<int> GetStudentCountInGroupAsync(int groupId);
    Task<string?> GetCuratorNameByStudentIdAsync(int studentId);
    Task<double> GetAverageAgeOfStudentsByCuratorIdAsync(int curatorId);

    Task<bool> CheckCuratorExisting(int groupId);
}