namespace CRMSystem.Core.ProjectionModels;

public record FileItem
(
    Stream Content, 
    string FileName, 
    string ContentType
);
