namespace Web.Models;

public record UploadModel(IFormFile File);
public record ResultModel(string Name, long Size, string MimeType, MalwareStatus MalwareStatus, string[] Signals);

public record ApiResultModel(Guid Id, MalwareStatus Status, string[] Signals);
public enum MalwareStatus { Unknown, Clean, Threat }
