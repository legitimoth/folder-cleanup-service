# ⚙️ Serviço de Limpeza de Diretórios para Runners

![Folder Cleanup Service](./imgs/foldercleanupservice.jpg)

## 📝 Descrição

Este serviço .NET realiza a limpeza automática de diretórios especificados, mantendo apenas o número configurado de subdiretórios mais recentes em cada um. É ideal para uso em ambientes de integração contínua, onde há necessidade de remover builds antigos e economizar espaço em disco.

## 🛠️ Configuração

### 📄 Arquivo de Configuração

Configure o serviço no arquivo `appsettings.json`:

- `DirectoryPaths`: Lista de caminhos de diretórios a serem monitorados.
- `KeepRecentFoldersCount`: Número de subdiretórios mais recentes a manter.

Exemplo de `appsettings.json`:

```json
{
  "FolderCleanup": {
    "DirectoryPaths": [
      "C:\\path\\to\\directory1",
      "C:\\path\\to\\directory2"
    ],
    "KeepRecentFoldersCount": 10
  }
}
```

## 📦 Build do Projeto

- Instale o .NET SDK correspondente à sua versão de desenvolvimento nesse caso foi `.net 7.0`.
- Abra o prompt de comando e navegue até a raiz do projeto.
- Execute `dotnet build -c Release` para compilar o projeto.
- Copie os arquivo gerados de dentro da pasta `bin/Release/net7.0` para dentro da pasta que desejar no servidor.

## ⏱️ Agendamento do Serviço

### 🧑🏻‍💻 Via Terminal
- Abra o prompt de comando como administrador.
- Crie uma tarefa agendada com:
```shell
schtasks /create /tn "FolderCleanupTask" /tr "C:\path\to\your\service\FolderCleanupService.exe" /sc daily /st 00:00.
```

### 🖥️ Via Interface Gráfica

- Pressione `Win + R`, digite `taskschd.msc` e pressione "Enter".
- Clique em "Criar Tarefa..." no painel de ações.
- Na aba "Geral", insira um "nome" e uma "descrição".
- Em "Disparadores", defina o "horário" e a "frequência" de execução.
- Em "Ações", aponte para o executável do serviço.
- Ajuste as configurações de segurança conforme necessário.

## 📑 Logs

O serviço possui um arquivo de log que fica dentro da pasta raiz. Esse arquivo e reescrito a cada execução.

Arquivo: `./.log`