using Microsoft.Extensions.Configuration;

namespace FolderCleanupService;

public class FolderCleanupService : IFolderCleanupService
{
    private readonly IEnumerable<string> _directoryPath;
    private readonly int _keepRecentFoldersCount;
    private readonly ILogger _logger;

    public FolderCleanupService(ILogger logger, IConfiguration config)
    {
        _logger = logger;
        _directoryPath = config.GetSection("FolderCleanup:DirectoryPaths").Get<List<string>>() ?? new List<string>();
        _keepRecentFoldersCount = int.TryParse(config["FolderCleanup:KeepRecentFoldersCount"], out _keepRecentFoldersCount) ?
            _keepRecentFoldersCount : 5;
    }

    public async Task KeepRecentFoldersAsync()
    {
        try
        {
            foreach (var folder in _directoryPath)
            {
                var directoryInfo = new DirectoryInfo(folder);
                if (!directoryInfo.Exists)
                {
                    _logger.Log($"❌ O diretório {directoryInfo.FullName} especificado não existe.");
                    continue;
                }

                var folders = directoryInfo.GetDirectories()
                    .OrderByDescending(d => d.CreationTimeUtc)
                    .ToList();

                if (folders.Count > _keepRecentFoldersCount)
                {
                    var foldersToDelete = folders.Skip(_keepRecentFoldersCount);

                    _logger.Log($"🚮 Deletando pastas do diretório: {directoryInfo.FullName}");

                    foreach (var subFolder in foldersToDelete)
                    {
                        _logger.Log($"↠ Deletando a pasta: {subFolder.Name}");
                        await Task.Run(() => subFolder.Delete(true));
                    }
                }

                _logger.Log($"🧹 Limpeza de pastas do diretório foi concluída.");
            }

            _logger.Log("✅ Done: Limpeza de pastas de de TODOS os diretórios foi concluída. As pastas mais recentes foram mantidas.");
        }
        catch (Exception ex)
        {
            _logger.Log($"🚨 Ocorreu um erro ao tentar excluir as pastas: {ex.Message}");
        }
    }
}
