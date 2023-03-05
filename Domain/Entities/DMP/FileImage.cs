﻿using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class FileImage
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Films Films { get; set; }
    public string? Path { get; set; }
    public DateTime CreatedDate { get; set; }
    public Account CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Account ModifiedBy { get; set; }
}