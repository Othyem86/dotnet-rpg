namespace dotnet_rpg.Services.CharacterService;

public class CharacterService : ICharacterService
{
    static List<Character> characters = new ()
    {
        new Character() { Id = 0, Name = "Frodo"},
        new Character() { Id = 1, Name = "Sam"},
    };

    public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
    {
        characters.Add(newCharacter);
        return new ServiceResponse<List<Character>> { Data = characters };
    }

    public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
    {
        return new ServiceResponse<List<Character>> { Data = characters };
    }

    public async Task<ServiceResponse<Character>> GetCharacterById(int id)
    {
        var character = characters.FirstOrDefault(c => c.Id == id);
        return new ServiceResponse<Character> { Data = character };;
    }
}