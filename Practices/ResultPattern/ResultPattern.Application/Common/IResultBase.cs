namespace ResultPattern.Application.Common
{
    public interface IResultBase
    {
        bool Success { get; }
        string? Error { get; }
        string[]? Errors { get; }
        int StatusCode { get; }

    }
}
