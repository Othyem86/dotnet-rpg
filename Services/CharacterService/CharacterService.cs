namespace dotnet_rpg.Services.CharacterService;

public class CharacterService : ICharacterService
{
    static List<Character> characters = new ()
    {
        new Character() { Id = 0, Name = "Frodo"},
        new Character() { Id = 1, Name = "Sam"},
    };

    public async Task<List<Character> AddCharacter(Character newCharacter)
    {
        characters.Add(newCharacter);
        return characters;
    }

    public async Task<List<Character>> GetAllCharacters()
    {
        return characters;
    }

    public async Task<Character> GetCharacterById(int id)
    {
        var character = characters.FirstOrDefault(c => c.Id == id);

        return character ?? throw new Exception("Character not found");
    }
}