namespace CRMSystem.Core.DTOs;

public record FileItem
(
    Stream Content, 
    string FileName, 
    string ContentType
);
