using System;

namespace Domain.Interfaces
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}