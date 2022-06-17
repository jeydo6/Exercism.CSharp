using System;

internal static class Appointment
{
    public static DateTime Schedule(string appointmentDateDescription) => DateTime.Parse(appointmentDateDescription);

    public static bool HasPassed(DateTime appointmentDate) => appointmentDate < DateTime.Now;

    public static bool IsAfternoonAppointment(DateTime appointmentDate) =>
        appointmentDate.TimeOfDay >= new TimeSpan(12, 0, 0) && appointmentDate.TimeOfDay < new TimeSpan(18, 0, 0);

    public static string Description(DateTime appointmentDate) => $"You have an appointment on {appointmentDate}.";

    public static DateTime AnniversaryDate() => new DateTime(DateTime.Today.Year, 9, 15);
}
