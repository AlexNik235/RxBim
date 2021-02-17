namespace PikTools.CommandExample
{
    using System;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Command.Api;
    using Shared;
    using Shared.FmHelpers.Abstractions;
    using Shared.RevitExtensions.Abstractions;
    using Shared.Ui.Abstractions;

    /// <summary>
    /// �������� ������� �������� ������ ������� ������ �� ���������� ������� � <see cref="IScopedElementsCollector"/>
    /// </summary>
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Cmd7 : PikToolsCommand
    {
        /// <summary>
        /// ������ �������� �������
        /// </summary>
        /// <param name="doc">Revit document</param>
        /// <param name="notificationService">������ �����������</param>
        /// <param name="familyManagerService">������ �� ������ � ������ �����������</param>
        public PluginResult ExecuteCommand(
            Document doc,
            INotificationService notificationService,
            IFamilyManagerService familyManagerService)
        {
            var a = familyManagerService.GetFamiliesByFunctionalType(doc, "����");

            return PluginResult.Succeeded;
        }
    }
}