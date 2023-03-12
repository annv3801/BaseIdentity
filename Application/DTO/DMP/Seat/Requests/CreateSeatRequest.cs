﻿namespace Application.DTO.DMP.Seat.Requests;

public class CreateSeatRequest
{
    public string Name { get; set; }
    public Guid RoomId { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
}