namespace dotnet_rpg.Services.CharacterService;

public class CharacterService : ICharacterService
{
    static List<Character> characters = new ()
    {
        new Character() { Id = 0, Name = "Frodo"},
        new Character() { Id = 1, Name = "Sam"},
    };

    readonly IMapper _mapper;

    public CharacterService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var character = _mapper.Map<Character>(newCharacter);
        character.Id = characters.Max(c => c.Id) + 1;
        
        characters.Add(character);

        return new ServiceResponse<List<GetCharacterDto>> 
        { 
            Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList() 
        };
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        return new ServiceResponse<List<GetCharacterDto>>
        {
            Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList() 
        };
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var character = characters.FirstOrDefault(c => c.Id == id);
        return new ServiceResponse<GetCharacterDto> 
        {
            Data = _mapper.Map<GetCharacterDto>(character)
        };
    }
}