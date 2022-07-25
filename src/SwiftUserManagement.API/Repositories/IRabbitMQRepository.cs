using SwiftUserManagement.API.Entities;

namespace SwiftUserManagement.API.Repositories
{
    public interface IRabbitMQRepository
    {

        void EmitGameAnalysis(GameResults gameResults);
    }
}
