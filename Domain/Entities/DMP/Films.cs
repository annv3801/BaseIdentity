﻿using Domain.Common;

namespace Domain.Entities.DMP;

public class Films : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortenUrl { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string? Description { get; set; }
    public string? Director { get; set; }
    public string? Actor { get; set; }
    public string? Genre { get; set; }
    public string? Premiere { get; set; }
    public string? Duration { get; set; }
    public string? Language { get; set; }
    public string? Rated { get; set; }
}