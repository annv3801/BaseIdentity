﻿using Domain.Common;
using Domain.Entities.Identity;
using Domain.Enums;

namespace Domain.Entities.DMP;

public class Seat : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid ScheduleId { get; set; }
    public FilmSchedule Schedule { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public DMPStatus Status { get; set; }
}