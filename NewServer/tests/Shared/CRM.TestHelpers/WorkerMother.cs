using System;
using CRM.HR.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;

namespace CRM.TestHelpers;

public static class WorkerMother
{
    public static readonly UserId DefaultUserId = new(Guid.NewGuid());
    public static readonly Name DefaultFirstName = Name.Create("Alex").Value;
    public static readonly Name DefaultLastName = Name.Create("Petrov").Value;
    public static readonly Money DefaultHourlyRate = Money.Create(25m).Value;
    public static readonly PhoneNumber DefaultPhone = PhoneNumber.Create("+375291234567").Value;
    public static readonly Email DefaultEmail = Email.Create("alex@service.com").Value;

    public static Worker CreateDefault(
        WorkerId? id = null,
        UserId? userId = null,
        Name? firstName = null,
        Name? lastName = null,
        Money? hourlyRate = null,
        PhoneNumber? phone = null,
        Email? email = null)
    {
        return Worker.Create(
            id ?? new WorkerId(Guid.NewGuid()),
            userId ?? DefaultUserId,
            firstName ?? DefaultFirstName,
            lastName ?? DefaultLastName,
            hourlyRate ?? DefaultHourlyRate,
            phone ?? DefaultPhone,
            email ?? DefaultEmail).Value;
    }
}
