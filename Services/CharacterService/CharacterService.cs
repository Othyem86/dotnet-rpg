namespace dotnet_rpg.Services.CharacterService;

public class CharacterService : ICharacterService
{
    static List<Character> characters = new () 
    {
        new Character() { Id = 0, Name = "Frodo"},
        new Character() { Id = 1, Name = "Sam"},
    };

    public List<Character> AddCharacter(Character newCharacter)
    {
        characters.Add(newCharacter);
        return characters;
    }

    public List<Character> GetAllCharacters()
    {
        return characters;
    }

    public Character GetCharacterById(int id)
    {
        return characters.FirstOrDefault(c => c.Id == id);
    }
}