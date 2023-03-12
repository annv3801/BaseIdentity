﻿using Domain.Common;

namespace Domain.Entities.DMP;

public class Ticket : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ScheduleId { get; set; }
    public FilmSchedule Schedule { get; set; }
    public float? Price { get; set; }
    public int Type { get; set; }
    
}