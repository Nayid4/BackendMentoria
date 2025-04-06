namespace Mentoria.Services.Emails.Dtos
{
    public record EmailDto(
        string From,
        string To,
        string Subject,
        string Body
    );
}
