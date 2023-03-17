/// <summary>
/// Class used to hold user information
/// </summary>
[System.Serializable]
public class User
{
    private string username;
    private string password;
    /// <summary>
    /// 1: só consulta (consult e NoPaNoSe) e movimentação
    /// 2: somente consulta (consult e NoPaNoSe [sem movimentação]), exportar csv, verificar os movimentos
    /// 3: consultar (consult e NoPaNoSe), adicionar e mover
    /// 5: tudo menos atualizar
    /// 10: tudo e mais um pouco
    /// </summary>
    private int accessLevel;

    public User(string username, int accessLevel)
    {
        this.username = username;
        this.accessLevel = accessLevel;
    }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public User(string username, string password, int accessLevel)
    {
        this.username = username;
        this.password = password;
        this.accessLevel = accessLevel;
    }

    public string GetUsername()
    {
        return username;
    }

    public string GetPassword()
    {
        return password;
    }

    public int GetAccessLevel()
    {
        return accessLevel;
    }
}
