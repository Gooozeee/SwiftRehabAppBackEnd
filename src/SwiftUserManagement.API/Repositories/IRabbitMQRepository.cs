using SwiftUserManagement.API.Entities;

namespace SwiftUserManagement.API.Repositories
{
    public interface IRabbitMQRepository
    {

        bool EmitGameAnalysis(GameResults gameResults);
        Task<bool> EmitVideoAnalysis(IFormFile video);

        string ReceiveVideoAnalysis();
    }
}
