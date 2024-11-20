using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProgressManager
{

    public ObservableValue<int> Coins { get; set; }
    public ObservableValue<int> Hints { get; set; }
    
    public Task<GameSave> GetLevelsProgress();
    public void SetLevelsProgress(string word, LevelData level);
    public Task SaveLevelsProgress();

}
