using MagicOnion;
using System.Data;

namespace grpc_test_interface
{
    /// <summary>
    /// ServerとclientのAPI定義
    /// </summary>
    public interface IMyFirstService : IService<IMyFirstService>
    {
        UnaryResult<int> SumAsync(int x, int y);

        DataTable GetFittingTable();
    }
}
