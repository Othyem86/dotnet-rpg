namespace dotnet_rpg.Services.CharacterService;

public class CharacterService : ICharacterService
{    
    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    readonly IMapper _mapper;
    readonly DataContext _context;

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var character = _mapper.Map<Character>(newCharacter);
        
        _context.Characters.Add(character);

        await _context.SaveChangesAsync();

        return new ServiceResponse<List<GetCharacterDto>> 
        { 
            Data = await _context.Characters
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToListAsync() 
        };
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        var response = new ServiceResponse<List<GetCharacterDto>>();
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        
        if (character is null)
        {
            response.Success = false;
            response.Message = $"Character with Id '{id}' not found.";
            return response;
        }

        _context.Characters.Remove(character);

        await _context.SaveChangesAsync();

        response.Data = await _context.Characters
            .Select(c => _mapper.Map<GetCharacterDto>(c))
            .ToListAsync();

        return response;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var characters = await _context.Characters.ToListAsync();

        return new ServiceResponse<List<GetCharacterDto>>
        {
            Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList() 
        };
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var response = new ServiceResponse<GetCharacterDto>();
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        
        if (character is null)
        {
            response.Success = false;
            response.Message = $"Character with Id '{id}' not found.";
            return response;
        }

        response.Data = _mapper.Map<GetCharacterDto>(character);

        return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        var response = new ServiceResponse<GetCharacterDto>();
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
        
        if (character is null)
        {
            response.Success = false;
            response.Message = $"Character with Id '{updatedCharacter.Id}' not found.";
            return response;
        }

        character.Name = updatedCharacter.Name;
        character.HitPoints = updatedCharacter.HitPoints;
        character.Strength = updatedCharacter.Strength;
        character.Defense = updatedCharacter.Defense;
        character.Intelligence = updatedCharacter.Intelligence;
        character.Class = updatedCharacter.Class;

        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetCharacterDto>(character);

        return response;
    }
}