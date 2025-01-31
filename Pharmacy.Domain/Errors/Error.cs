using System.Text;

namespace Pharmacy.Domain.Errors;

public record Error
{
    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }
    
    public string Code { get; } 
    public string Description { get; } 
    public ErrorType Type { get; }

    public string Serialize()
    {
        return $"{nameof(Code)}: {Code}\n{nameof(Description)}: {Description}\n{nameof(Type)}: {Type}";
    }
    
    public static Error Validation(string code, string description) => 
        new Error(code, description, ErrorType.Validation);
    
    public static Error NotFound(string code, string description) =>
        new Error(code, description, ErrorType.NotFound);
    
    public static Error Conflict(string code, string description) =>
        new Error(code, description, ErrorType.Conflict);

    public static Error Failure(string code, string description) =>
        new Error(code, description, ErrorType.Failure);
}