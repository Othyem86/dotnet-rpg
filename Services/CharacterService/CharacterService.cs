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

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        var response = new ServiceResponse<List<GetCharacterDto>>();

        try
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            
            if (character is null)
                throw new Exception($"Character with Id '{id}' not found.");

            characters.Remove(character);

            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
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
        var response = new ServiceResponse<GetCharacterDto>();

        try
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            
            if (character is null)
                throw new Exception($"Character with Id '{id}' not found.");

            response.Data = _mapper.Map<GetCharacterDto>(character);

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var response = new ServiceResponse<GetCharacterDto>();

        try 
        {
            var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
            
            if (character is null)
                throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");

            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;

            response.Data = _mapper.Map<GetCharacterDto>(character);
        }
        catch  (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}