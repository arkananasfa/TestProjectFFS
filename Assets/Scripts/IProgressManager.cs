using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProgressManager
{

    public Task<List<LevelData>> GetLevelsProgress();
    public void SetLevelsProgress(string word, LevelData level);
    public Task SaveLevelsProgress();

}
