using CRMSystemMobile.Extensions;
using Shared.Contracts.Absence;

namespace CRMSystemMobile.Selectors;

public class ScheduleTemplateSelector : DataTemplateSelector
{
    public ScheduleTemplateSelector()
    {
    }

    public DataTemplate? ShiftTemplate { get; set; }
    public DataTemplate? AbsenceTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        return item switch
        {
            ScheduleUiModel => ShiftTemplate,
            AbsenceResponse => AbsenceTemplate,
            _ => null
        };
    }
}