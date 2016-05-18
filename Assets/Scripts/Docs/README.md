Documentando o código                         {#mainpage}
============

A partir de agora todos os scritps a serem criados na MuvucaGames terão que seguir algumas regras para poder manter a organização e facilitar a manutenção futura do código.

No link abaixo temos descrições detalhadas de como comentar o código fonte utilizando as tags XML muito utilizadas em C#: https://msdn.microsoft.com/en-us/library/5ast78ax.aspx

O Doxygen suporta uma infinidade de tipos de arquivos de documentação, arquivos .dox que utiliza TAGS do Doxygen e o arquivo .md com o já conhecido MARKDOWN.

Ao escrever uma classe, métodos, variáveis sempre que colocar '///' antes do nome dos componentes, o Mono irá automaticamente adicionar os comentários em um formato que o Doxygen (ferramenta para gerar a documentação) conseguirá interpretar quando estiver gerando a documentação.

Podemos utilizar os comentários em português ou inglês, isso ainda não foi definido.

#### Veja o exemplo abaixo de uma classe e seus atributos não comentados:

\code{.unparsed}
public static class GameLevels
{
    #region Properties
    private const string EDITOR_LEVELS_FILE_DIRECTORY = "Assets/";
    private const string BUILD_LEVELS_FILE_DIRECTORY = "";
    private const string LEVEL_FILE_NAME = "GameLevels.ini";
    
    ...
        
    public static string[] Levels
    {
        get
        {
            ...
            
        }
    }
    
    ...
    
    private static string[] ReadLevelsFile(string directory)
    {
        string path = Path.Combine(directory, LEVEL_FILE_NAME);

        ...

        return levelNames.ToArray();
    }
    ...
    #endregion
}
\endcode

#### Agora a mesma classe com os comentários:

\code{.unparsed}
/// <summary>
/// Game levels.
/// Stripped from here:
/// http://answers.unity3d.com/questions/33263/how-to-get-names-of-all-available-levels.html#
/// </summary>    
public static class GameLevels
{
    #region Properties
    /// <summary>
    /// Editor levels directory
    /// </summary>    
    private const string EDITOR_LEVELS_FILE_DIRECTORY = "Assets/";
    
    /// <summary>
    /// Build leves directory
    /// </summary>    
    private const string BUILD_LEVELS_FILE_DIRECTORY = "";
    
    /// <summary>
    /// Game level file name
    /// </summary>
    private const string LEVEL_FILE_NAME = "GameLevels.ini";
    
    ...
    
    /// <summary>
    /// Gets the levels.
    /// </summary>
    /// <value>The levels.</value>        
    public static string[] Levels
    {
        get
        {
            ...
            
        }
    }
    
    ...
    
    /// <summary>
    /// Reads the levels file from the provided directory.
    /// </summary>
    /// <param name="directory">The directory that contains the levels file.</param>
    /// <returns>The discovered levels.</returns>
    private static string[] ReadLevelsFile(string directory)
    {
        string path = Path.Combine(directory, LEVEL_FILE_NAME);

        ...

        return levelNames.ToArray();
    }
    ...
    #endregion
}
\endcode